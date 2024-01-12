using JWT.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT.Services
{
    public class JwtGenerator
    {
        private readonly IConfiguration _builder;

        public JwtGenerator(IConfiguration builder)
        {
            _builder = builder;
        }

        public string GenerateJwtToken(string id, string name)
        {
            List<Claim> claims = new List<Claim>
            {
                new (JwtRegisteredClaimNames.Sid, id),
                new (JwtRegisteredClaimNames.Name, name),
            };

            var token = new JwtSecurityToken(
            issuer: _builder.GetSection("JwtSettings:Issuer").Value,
            audience: _builder.GetSection("JwtSettings:Audience").Value,
            claims: claims,
            expires: DateTime.Now.AddMinutes(1),
            notBefore: DateTime.Now,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_builder.GetSection("JwtSettings:Key").Value)),
                SecurityAlgorithms.HmacSha256)
        );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
