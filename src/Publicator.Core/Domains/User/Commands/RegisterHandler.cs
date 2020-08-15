using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Commands
{
    class RegisterHandler : IRequestHandler<Register, RegisterResult>
    {
        private readonly PublicatorDbContext _context;
        public RegisterHandler(PublicatorDbContext context) => _context = context;
        public async Task<RegisterResult> Handle(Register request, CancellationToken cancellationToken)
        {
            var existedUser = (from u in _context.Users
                               where u.Email.Equals(request.Email) || u.Nickname.Equals(request.Nickname)
                               select u
                               ).FirstOrDefault();

            var result = new RegisterResult();

            result.RegisterResultCode = RegisterResultEnum.Succesfull;

            if (existedUser == null)
                return await Task.FromResult(result);

            if (existedUser.Email.Equals(request.Email))
                result.RegisterResultCode = RegisterResultEnum.EmailAlreadyExist;

            if (existedUser.Nickname.Equals(request.Nickname))
                result.RegisterResultCode = RegisterResultEnum.NicknameAlreadyExist;

            // TODO: send mail for confirm on email of user

            return await Task.FromResult(result);
        }
    }
}
