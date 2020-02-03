using System;

namespace Publicator.ApplicationCore.Exceptions
{
    public class AuthentificationException : Exception
    {
        public AuthentificationException(string message) : base(message)
        {
        }
    }
}
