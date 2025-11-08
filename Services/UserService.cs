using Maskinstation.Interfaces;
using Maskinstation.Data;
using Maskinstation.DTOs;
using Maskinstation.Models;
using Mapster;
using BoilerMonitoringAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Maskinstation.Services
{
    public class UserService : IUser
    {
        private readonly MaskinstationContext _context;
        private readonly Auth _auth;
        private readonly GridFSService _GridFS;

        public UserService(MaskinstationContext context, Auth auth, GridFSService gridFS)
        {
            _context = context;
            _auth = auth;
            _GridFS = gridFS;
        }

        string DBNullText = "Database context is not available.";
        public async Task<UserDTOID> CreateAsync(UserDTO UserDTO)
        {
            try
            {
                if (_context.Users == null)
                {
                    throw new InvalidOperationException(DBNullText);
                }
                User User = UserDTO.Adapt<User>();
                User.Gallery = new Gallery
                {
                    Name = User.Name + "'s Gallery"
                };
                User.hasLoggedin = false;
                _context.Users.Add(User);
                User.Password = _auth.Hash(User.Password, User.UserID.ToString());
                await _context.SaveChangesAsync();
                return User.Adapt<UserDTOID>();
            }
            catch(DbUpdateException dbEx)
            {
                throw new DbUpdateException("Conflict while saving user. Possibly duplicate data.",dbEx);
            }
        }

        public async Task<string> AddProfilPic(IFormFile Image,Guid UserID)
        {
            string imageData = await _GridFS.UploadImageAsync(Image);

            return imageData;
        }

        
        public async Task<string> ChangePassword(Guid UserID,string NewPassword)
        {
            User User = _context.Users.Find(UserID);
            if(User == null)
            {
                throw new KeyNotFoundException($"User with ID '{UserID}' was not found.");
            }
            User.Password = _auth.Hash(User.Password, User.UserID.ToString());
            await _context.SaveChangesAsync();
            return "Password changed";



        }

        public async Task<UserTokens> Login(UserLoingObject UserLogin)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            UserLogin.Email = UserLogin.Email.ToLower();
            User user = _context.Users.FirstOrDefault(u => u.Email == UserLogin.Email);
            UserDTOImageID UserDTO = user.Adapt<UserDTOImageID>();
            Image profilPic = await _context.Images.Where(i => i.GalleryID == user.GalleryID).Where(i => i.Tags.Any(t => t.TagID == Guid.Parse("D290F1EE-6C54-4B01-90E6-D701748F0851"))).FirstOrDefaultAsync();
            if(profilPic != null)
            {
                UserDTO.ImageID = profilPic.ImageID;
            }
            else
            {
                UserDTO.ImageID = Guid.Empty;
            }
            

            if (user != null)
            {
                string hash = _auth.Hash(UserLogin.Password, user.UserID.ToString());
                if (hash == user.Password)
                {
                    RefreshToken RefreshToken = await CreateRefreshToken(user);
                    return new UserTokens(_auth.GenerateJwtToken(UserDTO), RefreshToken.Token);
                }
            }
            return null;
        }
        public async Task<UserTokens> RefreshToken(RefreshTokenUser Token)
        {
            User user = _context.Users.Find(Token.UserID);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {Token.UserID} was not found.");

            }
            if (_auth.Hash(Token.RefreshToken, user.UserID.ToString()) == user.RefreshToken)
            {
                if (user.RefeshTokenExpiryTime < DateTime.UtcNow)
                {
                    user.RefreshToken = null;
                    user.RefeshTokenExpiryTime = null;
                    await _context.SaveChangesAsync();
                    return null;
                }
                else
                {
                    RefreshToken RefreshToken = await CreateRefreshToken(user);
                    return new UserTokens(_auth.GenerateJwtToken(user.Adapt<UserDTOImageID>()), RefreshToken.Token);
                }
            }
            return null; 
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

        public async Task<UserDTOImageID> GetByIDAsync(Guid UserID)
        {
            if (_context.Users == null)
            {
                throw new InvalidOperationException(DBNullText);
            }
            User user = await _context.Users.FindAsync(UserID);
            Image profilPic = await _context.Images.Where(i => i.GalleryID == user.GalleryID).Where(i => i.Tags.Any(t => t.TagID == Guid.Parse("D290F1EE-6C54-4B01-90E6-D701748F0851"))).FirstOrDefaultAsync();
            UserDTOImageID userDTO = user.Adapt<UserDTOImageID>();
            if(profilPic != null)
            {
                userDTO.ImageID = profilPic.ImageID;
            }
            else
            {
                userDTO.ImageID = Guid.Empty;
            }
                return userDTO;
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

        public async Task<bool> Logout(Guid UserID)
        {
            User user = await _context.Users.FindAsync(UserID);
            if(user == null)
            {
                throw new KeyNotFoundException($"User with ID '{UserID}' was not found.");
            }
            user.RefeshTokenExpiryTime = DateTime.UtcNow;
            return true;
        }
    
        public async Task<RefreshToken> CreateRefreshToken(User user)
        {
            RefreshToken RefeshToken = new RefreshToken();
            user.RefeshTokenExpiryTime = RefeshToken.ExpiryTime;
            user.RefreshToken = _auth.Hash(RefeshToken.Token, user.UserID.ToString());
            await _context.SaveChangesAsync();
            return RefeshToken;
        }
    }

}

