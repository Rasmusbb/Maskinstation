

using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface IUser : ICRUD<UserDTO, UserDTOID>
    {
        Task<(MemoryStream Stream, string ContentType)> GetProfilPic(string fileID);
        Task<UserTokens> Login(UserLoingObject UserLogin);
        Task<UserTokens> RefreshToken(RefreshTokenUser Token);

    }
}
