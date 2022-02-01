using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using CheckInspecao.Transport.DTO;
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CheckInspecao.Transport
{
    public interface ITokenTransport
    {
        TokenDTO GetTokenDTO(UsuarioDTO usuarioDTO);
    }

    public class TokenTransport : ITokenTransport
    {
        private JwtConfigurationDTO jwtConfig { get; }

        public TokenTransport(IOptions<JwtConfigurationDTO> configuration)
        {
            jwtConfig = configuration.Value;
        }

        public TokenDTO GetTokenDTO(UsuarioDTO usuarioDTO)
        {
            return BuildToken(usuarioDTO);
        }

        private TokenDTO BuildToken(UsuarioDTO user)
        {
            var key =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));
            var creds =
                new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            // Por eqto só temos um tipo de Claim...
            IList<Claim> claims = new
                List<Claim>
                {
                    new Claim(JwtClaimTypes.Role, user.Role.ToString()),
                    new Claim(JwtClaimTypes.Name, user.Nome),
                    new Claim(JwtClaimTypes.Id, user.Id.ToString())
                };                        

            if (user.Empresas != null)
            {
                foreach (var item in user.Empresas)
                {
                    claims
                        .Add(new Claim("empresas",
                            item.EmpresaId.ToString()));                            
                }
            }

            var token =
                new JwtSecurityToken(jwtConfig.Issuer,
                    jwtConfig.Audience,
                    claims,
                    notBefore: DateTime.Now.AddMinutes(-1),
                    expires: DateTime
                        .Now
                        .AddMinutes(Convert
                            .ToInt32(jwtConfig.DurationInMinutes)),
                    signingCredentials: creds);

            var tokenDto =
                new TokenDTO()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration =
                        DateTime
                            .Now
                            .AddMinutes(Convert
                                .ToInt32(jwtConfig.DurationInMinutes))
                };
            return tokenDto;
        }
    }
}
