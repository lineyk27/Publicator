using System;

namespace Publicator.Core.Exceptions
{
    class FailedAuthenticationException : Exception
    {
        public FailedAuthenticationException() : base("Authentication failed, bad credentials")
        {
        }
    }
}
