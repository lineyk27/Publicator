using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;
using System.Diagnostics;

namespace Publicator.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        IEmailService _emailService;
        IHttpContextAccessor _httpContextAccessor;
        IPasswordService _passwordService;
        IRoleService _roleService;
        IStateService _stateService;
        public UserService(
            IUnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IPasswordService passwordService,
            IRoleService roleService,
            IStateService stateService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _passwordService = passwordService;
            _roleService = roleService;
            _stateService = stateService;
        }
        public bool ConfirmAccount(User user, string token)
        {
            // TODO must be reconsidered
            Guid id;
            var isgood = Guid.TryParse(token, out id);
            if (isgood)
            {
                if(user.Id == id)
                {
                    user.EmailConfirmed = true;
                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Save();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = (await _unitOfWork
                .UserRepository
                .GetAsync(x => x.Id == id))
                .FirstOrDefault();
            if (user == null)
                throw new ResourceNotFoundException("User not found");
            return user;
        }

        public async Task<User> GetByPostAsync(Post post)
        {
            return (await _unitOfWork
                .UserRepository
                .GetAsync(x => x.Id == post.CreatorUserId,includeProperties:"Role"))
                .FirstOrDefault();
        }
        public async Task<IEnumerable<User>> GetByRoleAsync(Role role)
        {
            return (await _unitOfWork
                .RoleRepository
                .GetAsync(x => x.Id == role.Id, includeProperties: "Users"))
                .FirstOrDefault()?
                .Users;
        }

        public async Task<IEnumerable<User>> GetBySearchAsync(string query)
        {
            return await _unitOfWork
                .UserRepository
                .GetAsync(x => x.Nickname.Contains(query));
        }

        public async Task<IEnumerable<User>> GetByStateAsync(State state)
        {
            return (await _unitOfWork
                .StateRepository
                .GetAsync(x => x.Id == state.Id, includeProperties: "Users"))
                .FirstOrDefault()?
                .Users;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = (await _unitOfWork
                .UserRepository
                .GetAsync(x => x.Nickname == username,includeProperties:"Role"))
                .FirstOrDefault();
            if (user == null)
                throw new ResourceNotFoundException("User not found");

            return user;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var username = _httpContextAccessor.HttpContext.User?.Identity.Name;
            if(username != null)
            {
                return await GetByUsernameAsync(username);
            }
            else
            {
                throw new AuthentificationException("User is not authenticated");
            }
        }

        public async Task<User> TryGetCurrentAsync()
        {
            try
            {
                var user = await GetCurrentUserAsync();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetSubscribersAsync(User subscriptionuser)
        {
            return (await _unitOfWork
                .UserSubscriptionRepository
                .GetAsync(x => x.SubscriptionUserId == subscriptionuser.Id, includeProperties: "SubscriberUser"))
                .Select(x => x.SubscriberUser);
        }

        public async Task<IEnumerable<User>> GetSubscriptionsAsync(User subscriberuser)
        {
            return (await _unitOfWork
                .UserSubscriptionRepository
                .GetAsync(x => x.SubscriberUserId == subscriberuser.Id, includeProperties: "SubscriptionUser"))
                .Select(x => x.SubscriberUser);
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            var user = (await _unitOfWork
                .UserRepository
                .GetAsync(x => x.Email == email,includeProperties:"Role"))
                .FirstOrDefault();
            if(user == null)
                throw new ResourceNotFoundException("User not found");
            return user;
        }

        public async Task<User> LoginAsync(string login, string password)
        {
            var user = await GetByUsernameAsync(login) ?? await GetByEmailAsync(login);
            if (user == null)
                throw new ResourceNotFoundException("User not found");
            string passwordhash = _passwordService.Encrypt(password);
            if (_passwordService.IsEqual(passwordhash, user.PasswordHash) &&
                user.EmailConfirmed)
            {
                return user;
            }
            else if (!user.EmailConfirmed)
                throw new AuthentificationException("Account is not confirmed");
            Debug.WriteLine($"{passwordhash} - {user.PasswordHash}");
            throw new Exception("Login or password is not correct");
        }

        public string GetConfirmEmailText(Guid userid, string token)
        {
            // TODO set real confirm link
            string link = $"localhost:8080/confirm?username={userid}&token={token}";
            string cancellink = $"localhost:8080/cancel?username={userid}&token={token}";
            return $"<h3>Hello dear user, please confirm your account<h3/>" +
                "<p>You registered account on publicator.com, and it is email for confirm account<br>" +
                "Click on link below to confirm account:<br>" +
                $"<a href=\"{link}\">{link}<a/><br>" +
                $"If you didn't create account on publicator.com, click below<br>" +
                $"<a href=\"{cancellink}\">{cancellink}<a/><p/>";
        }

        public async Task RegisterAsync(string username, string email, string password)
        {
            User user = new User();
            try
            {
                user = await GetByUsernameAsync(username);
            }
            catch
            {
                try
                {
                    user = await GetByEmailAsync(email);
                }
                catch(ResourceException e)
                {
                    var id = Guid.NewGuid();
                    var role = await _roleService.GetByNameAsync("Simple");
                    var state = await _stateService.GetByNameAsync("Active");
                    var newuser = new User()
                    {
                        Id = id,
                        JoinDate = DateTime.Now,
                        PasswordHash = _passwordService.Encrypt(password),
                        Nickname = username,
                        Email = email,
                        EmailConfirmed = false,
                        RoleId = role.Id,
                        StateId = state.Id
                    };
                    _unitOfWork.UserRepository.Insert(newuser);
                    _unitOfWork.Save();
                    var emailtext = GetConfirmEmailText(id, id.ToString("D"));
                    _emailService.SendEmailAsync(email, "Publicator", emailtext);
                    return;
                }
                throw new ResourceException("User with the email is already exist");
            }
            finally
            {
                throw new ResourceException("User with the username is already exist");
            }
        }

        public async Task<bool> MakeSubscription(User subscription)
        {
            var current = await GetCurrentUserAsync();
            var currsub = (await _unitOfWork
                .UserSubscriptionRepository
                .GetAsync(x => x.SubscriberUserId == current.Id && x.SubscriptionUserId == subscription.Id))
                .FirstOrDefault();
            if(currsub == null)
            {
                _unitOfWork
                    .UserSubscriptionRepository
                    .Insert(new UserSubscription()
                    {
                        SubscriberUserId = current.Id,
                        SubscriptionUserId = subscription.Id
                    });
                _unitOfWork.Save();
                return true;
            }
            else
            {
                _unitOfWork.UserSubscriptionRepository.Delete(currsub);
                _unitOfWork.Save();
                return false;
            }
        }

        public async void ChangeUserPicture(string url)
        {
            var user = await GetCurrentUserAsync();
            user.PictureName = url;
            _unitOfWork.UserRepository.Update(user);
            _unitOfWork.Save();
        }
    }
}
