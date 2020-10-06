using System;
using System.Collections.Generic;
using System.Text;

namespace Publicator.Core.Domains.User.Commands
{
    public class LoginResult
    {
        public LoginResultEnum Result { get; set; }
        public string Token { get; set; }
    }
}
