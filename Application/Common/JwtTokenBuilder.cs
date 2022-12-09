using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModel.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common
{
    public static class JwtTokenBuilder
    {
        public static string BuildToken(UserViewModel user, IConfiguration configuration,int expireDate)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTBearer:signingKey"]));
            var Credential = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: configuration["JWTBearer:Issuer"],
                audience: configuration["JWTBearer:Audience"], claims: claims, expires: DateTime.Now.AddDays(expireDate),
                signingCredentials: Credential
            );

            var addToken = new JwtSecurityTokenHandler().WriteToken(token);
            return addToken;
        }

    }
}
