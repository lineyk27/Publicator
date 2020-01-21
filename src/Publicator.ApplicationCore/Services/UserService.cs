using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Publicator.ApplicationCore.Contracts;
using Publicator.ApplicationCore.Exceptions;
using Publicator.Infrastructure.Entities;
using Publicator.Infrastructure.Interfaces;

namespace Publicator.ApplicationCore.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        IEmailService _emailService;
        IHttpContextAccessor _httpContextAccessor;
        IPasswordService _passwordService;
        public UserService(
            IUnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _passwordService = passwordService;
        }
        public bool ConfirmAccountAsync(User user, string token)
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
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByPostAsync(Post post)
        {

            return await _unitOfWork
                .UserRepository
                .GetByIdAsync(post.CreatorUserId);
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
                .GetAsync(x => x.Nickname == username))
                .FirstOrDefault();
            if (user == null)
                throw new ResourceNotFoundException("User not found");

            return user;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            // TODO must be reconsidered
            var username = _httpContextAccessor.HttpContext.User?.Identity.Name;
            if(username != null)
            {
                return await GetByUsernameAsync(username);
            }
            else
            {
                // TODO create custom exception
                throw new Exception("User is not authenticated");
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
            return (await _unitOfWork
                .UserRepository
                .GetAsync(x => x.Email == email))
                .FirstOrDefault();
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
                throw new Exception("Account is not confirmes");
            throw new Exception("Auth error");
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

        public void RegisterAsync(string username, string email, string password)
        {
            var user = GetByUsernameAsync(username);
            if (user != null)
                throw new Exception("User with the username is already exist");
            user = GetByEmailAsync(email);
            if (user != null)
                throw new Exception("User with the email is already exist");
            var id = Guid.NewGuid();
            // TODO add logic for set role and state
            var newuser = new User()
            {
                Id = id,
                JoinDate = DateTime.Now,
                PasswordHash = _passwordService.Encrypt(password),
                Nickname = username,
                Email = email,
                EmailConfirmed = false
            };
            _unitOfWork.UserRepository.Insert(newuser);
            _unitOfWork.Save();
            var emailtext = GetConfirmEmailText(id, id.ToString("D"));
            _emailService.SendEmailAsync(email, "Publicator", emailtext);
        }
    }
}
