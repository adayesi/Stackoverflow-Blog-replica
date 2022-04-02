using DecaBlog.Models;
using DecaBlog.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DecaBlog.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateJWTToken(User user, IEnumerable<string> userRoles)
        {
            if (string.IsNullOrWhiteSpace(user.PhotoUrl))
                user.PhotoUrl = "";

            //Adding user claims
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("photo", user.PhotoUrl),
                new Claim(ClaimTypes.Email, user.Email),
            };
            foreach (var role in userRoles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            //Set up system security
            var SymmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(SymmetricSecurity, SecurityAlgorithms.HmacSha256)
            };
            //Create token
            var SecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = SecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return SecurityTokenHandler.WriteToken(token);
        }
    }
}
