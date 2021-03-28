using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pokemanos.Api.Model;
using Pokemanos.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Pokemones.API.Helper
{

    public static class TokenHelper
    {
        public static string GenerateToken(Usuario usuario, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Authentication, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Email, usuario.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static string GetClaims(IIdentity claimsIdentity, string claimName)
        {
            var identity = claimsIdentity as ClaimsIdentity;
            if (identity != null) return identity.FindFirst(claimName)?.Value;
            else return null;
        }
    }
}
