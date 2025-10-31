using Maskinstation.Models;

namespace Maskinstation.DTOs
{
    public class GalleryDTO
    {
        public string Name { get; set; }
    }
    public class GalleryDTOID : GalleryDTO
    {
        public Guid GalleryID { get; set; }
        public ICollection<ImageDTOTags> Images { get; set; }
    }
}
