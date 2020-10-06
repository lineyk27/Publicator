using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Publicator.Core.Services;
using Publicator.Infrastructure;
using Publicator.Infrastructure.Models;

namespace Publicator.Core.Domains.User.Commands
{
    class RegisterHandler : IRequestHandler<Register, RegisterResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly ILogger<RegisterHandler> _logger;
        private readonly UserManager<Infrastructure.Models.User> _userManager;
        private readonly SignInManager<Infrastructure.Models.User> _signInManager;
        private readonly IEmailService _emailService;
        public RegisterHandler(
            PublicatorDbContext context, 
            ILogger<RegisterHandler> logger,
            UserManager<Infrastructure.Models.User> userManager,
            SignInManager<Infrastructure.Models.User> signInManager,
            IEmailService emailService
            ) 
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        } 
        public async Task<RegisterResult> Handle(Register request, CancellationToken cancellationToken)
        {
            var userNameExist = await _userManager.FindByNameAsync(request.Nickname);
            var emailExist = await _userManager.FindByEmailAsync(request.Email);

            var result2 =  await _userManager.CreateAsync(new Infrastructure.Models.User()
            {
                JoinDate = DateTime.Now,
                UserName = request.Nickname,
                Email = request.Email,
            }, request.Password);

            var result = new RegisterResult();

            if (userNameExist != null)
            {
                _logger.LogInformation("A try to register account with Username that is already exist");
                result.RegisterResultCode = RegisterResultEnum.NicknameAlreadyExist;
                return result;
            }

            if (emailExist != null)
            {
                _logger.LogInformation("A try to register account on email that is already exist");
                result.RegisterResultCode = RegisterResultEnum.EmailAlreadyExist;
                return result;
            }

            if (result2.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var token = await _userManager
                    .GenerateEmailConfirmationTokenAsync(user);

                _emailService.SendConfirmationEmail(user.Email, token, user.UserName);
                
                _logger.LogInformation("Succesfull registration for user with id: {0}", 0);
                
                result.RegisterResultCode = RegisterResultEnum.Succesfull;
                return result;
            }
            else
            {
                result.RegisterResultCode = RegisterResultEnum.BadCrendentials;
                return result
            }
        }
    }
}
