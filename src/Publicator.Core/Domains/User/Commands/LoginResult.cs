﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Publicator.Core.Domains.User.Commands
{
    public class LogInResult
    {
        public LoginResultEnum Result { get; set; }
        public string Token { get; set; }
    }
}