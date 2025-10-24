

using Maskinstation.DTOs;

namespace Maskinstation.interfaces
{
    public interface IUser : ICRUD<UserDTO, UserDTOID>
    {
        Task<UserTokens> Login(UserLoingObject UserLogin);
        Task<UserTokens> RefreshToken(RefreshTokenUser Token);

    }
}
