using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public class ImageTag
    {
        [ForeignKey("ImageID")]
        public Guid? ImageID { get; set; }
        public Image? Image { get; set; }

        [ForeignKey("TagID")]
        public Guid? TagID { get; set; }
        public Tag? Tag { get; set; }
    }
}
