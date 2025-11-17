
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool hasLoggedin { get; set; }

        [ForeignKey("GalleryID")]
        public Guid? GalleryID { get; set; }
        public Gallery Gallery { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefeshTokenExpiryTime { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Role> Roles { get; set; }
        
    }
}
