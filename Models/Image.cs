using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public class Image
    {
        [Key]
        public Guid ImageID { get; set; }
        public string FileID { get; set; }
        public DateTime Created { get; set; }
        [ForeignKey("GalleryID")]
        public Guid GalleryID { get; set; }
        public Gallery Gallery { get; set; }
 
        public ICollection<Tag> Tags { get; set; }
    }
}
