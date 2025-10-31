using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public class Brand
    {
        [Key]
        public Guid BrandID { get; set; }
        public string BrandName { get; set; }

        [ForeignKey("GalleryID")]
        public Guid? GalleryID { get; set; }
        public Gallery Gallery { get; set; }

    }
}
