using System;

namespace Publicator.ApplicationCore.Exceptions
{
    public class ResourceException : Exception
    {
        public ResourceException(string message) : base(message)
        {
        }
    }
}
