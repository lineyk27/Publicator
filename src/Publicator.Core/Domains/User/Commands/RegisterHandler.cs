using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Commands
{
    class RegisterHandler : IRequestHandler<Register, RegisterResult>
    {
        private readonly PublicatorDbContext _context;
        private readonly ILogger<RegisterHandler> _logger;
        public RegisterHandler(PublicatorDbContext context, ILogger<RegisterHandler> logger) 
        {
            _context = context;
            _logger = logger;
        } 
        public async Task<RegisterResult> Handle(Register request, CancellationToken cancellationToken)
        {
            var existedUser = await (from u in _context.Users
                               where u.Email.Equals(request.Email) || u.Nickname.Equals(request.Nickname)
                               select u
                               ).FirstOrDefaultAsync();

            var result = new RegisterResult();

            result.RegisterResultCode = RegisterResultEnum.Succesfull;

            if (existedUser == null)
            {
                // todo: add actually a logic for registration
                _logger.LogInformation("Succesfull registration for user with id: {0}", 0);
                return result;
            }

            if (existedUser.Email.Equals(request.Email))
            {
                _logger.LogInformation("A try to register account on email that is already exist");
                result.RegisterResultCode = RegisterResultEnum.EmailAlreadyExist;
            }
            if (existedUser.Nickname.Equals(request.Nickname))
            {
                _logger.LogInformation("A try to register account with nickname that is already exist");
                result.RegisterResultCode = RegisterResultEnum.NicknameAlreadyExist;
            }
            // TODO: send mail for confirm on email of user

            return result;
        }
    }
}
