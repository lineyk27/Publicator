using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Publicator.Infrastructure.Models;

namespace Publicator.Core.Services
{
    class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContext;
        public AuthService(IHttpContextAccessor httpAccessor) => _httpContext = httpAccessor;
        public bool IsAuthorized { get => GetCurrentUserId() != null; }

        public Guid? GetCurrentUserId()
        {
            var id = _httpContext.HttpContext?.User?.Claims?
                .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value;

            if(id != null)
            {
                return Guid.Parse(id);
            }
            return null;
        }
    }
}
