using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using RestWithAsp.NetUdemy.Data.Converters;
using RestWithAsp.NetUdemy.Data.VO;
using RestWithAsp.NetUdemy.Model;
using RestWithAsp.NetUdemy.Repository;
using RestWithAsp.NetUdemy.Repository.Generic;
using RestWithAsp.NetUdemy.Security.Configuration;

namespace RestWithAsp.NetUdemy.Business.Implementattions
{
    public class LoginBusinessImplementattion : ILoginBusiness
    {

        private IUserRepository _repository;
        private SigningConfiguration _signingConfiguration;
        private TokenConfiguration _tokenConfiguration;

        public LoginBusinessImplementattion(IUserRepository repository, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }

        public object FindByLogin(User user)
        {
            bool CredentialsIsValid = false;
            if(user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repository.FindByLogin(user.Login);
                CredentialsIsValid = (baseUser != null && user.Login == baseUser.Login && user.AccessKey == baseUser.AccessKey);
            }

            if (CredentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login")
                    ,
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                    }
                    
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);


                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(
            new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to authenticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticaded = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                messsage = "OK"
            };
        }
    }
}
