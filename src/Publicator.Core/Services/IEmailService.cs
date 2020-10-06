using System;
using System.Collections.Generic;
using System.Text;

namespace Publicator.Core.Services
{
    public interface IEmailService
    {
        void SendConfirmationEmail(string email, string token, string username);
    }
}
