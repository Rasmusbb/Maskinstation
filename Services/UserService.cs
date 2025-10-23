using Maskinstation.interfaces;
using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.models;
using Mapster;


namespace Maskinstation.Services
{
    public class UserService : IUser
    {
        private readonly MaskinstationContext _context;
        private readonly Auth _auth;

        public UserService(MaskinstationContext context, Auth auth)
        {
            _context = context;
            _auth = auth;
        }

        string DBNullText = "Database context is not available.";
        public async Task<UserDTOID> CreateAsync(UserDTO UserDTO)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            User User = UserDTO.Adapt<User>();
            _context.Users.Add(User);
            User.Password = _auth.Hash(User.Password, User.UserID.ToString());
            await _context.SaveChangesAsync();
            return User.Adapt<UserDTOID>();
        }

        public async Task<IEnumerable<UserDTOID>> GetAllAsync()
        {
            IEnumerable<UserDTOID> Users = new List<UserDTOID>();
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            Users = _context.Users.Adapt<IEnumerable<UserDTOID>>();
            return Users;

        }

        public async Task<UserDTOID> GetByIDAsync(Guid UserID)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            User User = await _context.Users.FindAsync(UserID);
            return User.Adapt<UserDTOID>();
        }

        public async Task<bool> UpdateAsync(Guid UserID, UserDTO User)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(Guid UserID)
        {

            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            User User = await _context.Users.FindAsync(UserID);
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}

