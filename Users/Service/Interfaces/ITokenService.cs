using Service.Configs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interfaces
{
    public interface ITokenService
    {
        TokenModel Token(LoginModel user, SigningConfig signingConfigurations
                                  , TokenConfig tokenConfigurations);
    }
}
