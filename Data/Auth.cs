using Maskinstation.models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Maskinstation.Data
{
    public class Auth
    {
        private readonly IConfiguration _configuration;
        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Hash(string password,string salt)
        {

            StringBuilder builder = new StringBuilder();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
            }
            return builder.ToString();
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new Claim[]
            {
                new Claim("UserID", user.UserID.ToString()),
                new Claim("Name", user.Name.ToString()),
                new Claim("Email",user.Email.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt:Issuer").Value,
                audience: _configuration.GetSection("Jwt:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
