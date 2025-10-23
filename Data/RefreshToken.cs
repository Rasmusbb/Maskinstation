using System.Security.Cryptography;

namespace BoilerMonitoringAPI.Data
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiryTime { get; set; }
        public RefreshToken()
        {
            Token = GenerateRefeshToken();
            ExpiryTime = DateTime.Now.AddDays(90);
        }

        public RefreshToken(string Token, DateTime ExpiryTime)
        {
            this.Token = Token;
            this.ExpiryTime = ExpiryTime;
        }
        public string GenerateRefeshToken()
        {
            var randomNumber = new byte[32];
            DateTime expiretime = DateTime.Now.AddDays(90);
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }

        public bool IsExpired()
        {

            if (ExpiryTime < DateTime.UtcNow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
