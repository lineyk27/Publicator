using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Publicator.Infrastructure;

namespace Publicator.Core.Domains.User.Commands
{
    class LogInHandler : IRequestHandler<LogIn, string>
    {
        private readonly PublicatorDbContext _context;
        private readonly JWTSettings jWTSettings;
        public LogInHandler(PublicatorDbContext context)
        {
            _context = context;
        }
        public LogInHandler()
        {

        }
        public Task<string> Handle(LogIn request, CancellationToken cancellationToken)
        {



            return null;
        }
        private string GetAuthToken(Infrastructure.Models.User user)
        {
            throw new NotImplementedException();
        }
    }
}
