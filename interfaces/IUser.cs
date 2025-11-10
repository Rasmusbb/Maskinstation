

using Maskinstation.DTOs;

namespace Maskinstation.Interfaces
{
    public interface IUser : ICRUD<UserDTO, UserDTOID>
    {
        Task<UserTokens> Login(UserLoingObject UserLogin);
        Task<bool> Logout(Guid UserID);
        Task<UserTokens> RefreshToken(RefreshTokenUser Token);
        Task<string> ChangePassword(Guid UserID, string NewPassword);
        Task<UserDTOImageID> GetByIDAsync(Guid id);
    }
}
