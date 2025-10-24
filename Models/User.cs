
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
        public string Role { get; set; }

        [ForeignKey("ImageID")]
        public Guid? ImageID { get; set; }
        public Image? Image { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefeshTokenExpiryTime { get; set; }
        public ICollection<UserTags> UserTags { get; set; }
        public ICollection<Image> images { get; set; }
    }
}
