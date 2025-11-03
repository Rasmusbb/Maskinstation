using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public class Brand
    {
        [Key]
        public Guid BrandID { get; set; }
        public string BrandName { get; set; }

        [ForeignKey("ImageID")]
        public Guid ImageID { get; set; }
        public Image Image { get; set; }
    }
}
