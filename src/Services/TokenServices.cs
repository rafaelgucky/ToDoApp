using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Data;
using ToDoApp.Entities.Models;

namespace ToDoApp.Services
{
    public class TokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly Context _context;

        public TokenServices(IConfiguration configuration, Context context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string CreateToken(User user)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            foreach(Role role in user.Roles!)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role.Name));
            }

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretKey")!)),
                SecurityAlgorithms.HmacSha256Signature),
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetSection("JWT").GetValue<int>("Expires")),
                Audience = "https://seusistema.com",
                Issuer = "https://seudominio.com"
            };
            return handler.WriteToken(handler.CreateToken(securityTokenDescriptor));
        }

        public async Task<bool> Logout(string token)
        {
            _context.InvakidTokens.Add(new InvalidToken
            {
                Token = token.Replace("Bearer ", "")
            });
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
