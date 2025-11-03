using Maskinstation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{

    public class Machine
    {
        [Key]
        public Guid MachineID { get; set; }
        public string Model { get; set; }
        public string? Description { get; set; }
        [ForeignKey("UserID")]
        public Guid? UserID { get; set; }
        public User? User { get; set; }
         
        [ForeignKey("BrandID")]
        public Guid BrandID { get; set; }
        public Brand Brand { get; set; }
        public ICollection <Tag> Tags { get; set; }

        [ForeignKey("GalleryID")]
        public Guid? GalleryID { get; set; }
        public Gallery Gallery { get; set; }

    }
}
