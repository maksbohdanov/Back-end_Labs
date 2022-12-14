using CostAccounting.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi
{
    public static class TokenGenerator
    {
        public static string GenerateToken(User user, IConfiguration _configuration)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var secret = _configuration["JWT:Secret"];
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
           

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
