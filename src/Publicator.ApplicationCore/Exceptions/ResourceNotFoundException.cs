using System;

namespace Publicator.ApplicationCore.Exceptions
{
    class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : base() 
        {
        }
        public ResourceNotFoundException(string message) 
        : base(message)
        {
        }
    }
}
