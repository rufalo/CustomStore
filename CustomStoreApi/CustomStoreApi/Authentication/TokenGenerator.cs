using CustomStoreApi.Context.Tabels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomStoreApi.Authentication
{
    public class TokenGenerator
    {
        public readonly IConfiguration _Configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string CreateToken(ApplicationUser user,List<string> roles)
        {
            var claims = new List<Claim>()
            {
                // Unique Id for this Token
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                // The username using the Identity name so it fills out the HttpContext.User.Idenity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName),
                // Security stamp to make token invalid server side
                new Claim("securityStamp",user.SecurityStamp)
            };

            // Adds the Roles of the user to the token
            foreach  (var element in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, element));
            }

            // Create the credentials used to generate the token
            var creditentails = new SigningCredentials(
                // Get the secred key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:SecretKey"])),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _Configuration["Jwt:Issuer"],
                audience: _Configuration["Jwt:Audience"],
                claims: claims.ToArray(),
                signingCredentials: creditentails,
                // Expires in 24 Hours
                expires: DateTime.Now.AddDays(1));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateTokenFin(ApplicationUser user, List<string> roles)
        {
            var claims = new List<Claim>()
            {                
                // Unique Id for this token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                
                // The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                // Security stamp to make token invald server side
                new Claim("SecurityStamp",user.SecurityStamp),

            };

            foreach (var element in roles)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, element));
            }


            // Create the credentials used to generate the token
            var crededentials = new SigningCredentials(
                // Get the secret key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:SecretKey"])),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _Configuration["Jwt:Issuer"],
                audience: _Configuration["Jwt:Audience"],
                claims: claims.ToArray(),
                signingCredentials: crededentials,
                // Exirpes in 24 hours
                expires: DateTime.Now.AddDays(1)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
