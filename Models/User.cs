using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Maskinstation.models
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Image ProfilImage { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public ICollection<Image> images { get; set; }
    }
}
