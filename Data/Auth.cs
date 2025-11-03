using Maskinstation.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Maskinstation.DTOs;

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

        public string GenerateJwtToken(UserDTOImageID user)
        {
            string value = _configuration.GetSection("Jwt:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            string keytext = key.Key.ToString();
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            Claim[] claims = new Claim[]
            {
                new Claim("UserID", user.UserID.ToString()),
                new Claim("Name", user.Name.ToString()),
                new Claim("Email",user.Email.ToString()),
                new Claim("HasLoggedin",user.hasLoggedin.ToString()),
                new Claim("ProfilPic",user.ImageID.ToString()),
                new Claim(ClaimTypes.Role, user.Role)

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
