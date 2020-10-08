using System;

namespace Publicator.Core.Services
{
    public interface IAuthService
    {
        public bool IsAuthorized { get; }
        public Guid? GetCurrentUserId();
    }
}
