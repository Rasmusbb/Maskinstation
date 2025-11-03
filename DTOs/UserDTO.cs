

using MongoDB.Driver;

namespace Maskinstation.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }


    }


    public class UserDTOImageID : UserDTO
    {
        public Guid UserID { get; set; }
        public Guid? ImageID { get; set; }
        public bool hasLoggedin { get; set; }
    }


    public class UserLoingObject()
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserDTOIDTokens : UserDTOID
    {
        public string RefreshToken { get; set; }
        public DateTime RefeshTokenExpiryTime { get; set; }

        string? accessToken { get; set; }
    }

    public class UserDTOID : UserDTO
    {
        public Guid UserID { get; set; }
    }

    public class UserDTOPas : UserDTO
    {
        public string Password { get; set; }
    }

    public class RefreshTokenUser
    {
        public Guid UserID { get; set; }
        public string RefreshToken { get; set; }
    }

    public class UserTokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserTokens(string AccessToken, string RefreshToken)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
        }
    }
}
