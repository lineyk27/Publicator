using System;

namespace Publicator.Core.Exceptions
{
    class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message)
        {
        }

        public ResourceNotFoundException() : base("Requested resource not found.")
        {
        }
    }
}
