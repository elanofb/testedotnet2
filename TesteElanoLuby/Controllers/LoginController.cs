using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using TesteElano.Models;
using TesteElano.Repository;
using TesteElano.Util;

namespace TesteElano.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {        
        IDesenvolvedorRepository desenvolvedorRepository;
        public LoginController(IDesenvolvedorRepository _dsenvolvedorRepository)
        {
            desenvolvedorRepository = _dsenvolvedorRepository;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[controller]")]
        public object Login(
            [FromBody]Desenvolvedor dev,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            if (dev != null && !String.IsNullOrWhiteSpace(dev.DesenvolvedorId.ToString()))
            {
                var devBase = desenvolvedorRepository.GetDesenvolvedor(dev.DesenvolvedorId);
                
                credenciaisValidas = (devBase != null &&
                    dev.DesenvolvedorId == devBase.DesenvolvedorId);             
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(dev.DesenvolvedorId.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, dev.DesenvolvedorId.ToString())
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "OK"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }
    }
}

