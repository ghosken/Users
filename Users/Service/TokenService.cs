using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.Configs;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Service
{
    public class TokenService : ITokenService
    {
        public TokenModel Token(LoginModel user, SigningConfig signingConfigurations
                               , TokenConfig tokenConfigurations)
        {
            try
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Id.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
                    }
                );

                DateTime CreatedDate = DateTime.Now;
                DateTime ExpiredDate = CreatedDate + TimeSpan.FromMinutes(tokenConfigurations.Minutes);
                IdentityModelEventSource.ShowPII = true;
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = CreatedDate,
                    Expires = ExpiredDate
                });
                var token = handler.WriteToken(securityToken);

                return new TokenModel()
                {
                    Authenticated = true,
                    Created = CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    Expiration = ExpiredDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    AccessToken = token
                };

            }
            catch
            {
                return new TokenModel { Authenticated = false };
            }
        }
    }
}
