using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserApi.Constants;
using UserApi.Dtos.Entities;

namespace UserApi.Authentication
{
    public static class JwtTokenGenerator
    {
        public static string GenerateJwtToken(UserLoginDetails user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(JwtConfig.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = "http://fakeIssuer.com",
                Audience = "UserApiApp",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Name, $"{user.FirstName}_{user.LastName}"),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);

        }
    }
}
