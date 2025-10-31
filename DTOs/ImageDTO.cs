using Maskinstation.Models;

namespace Maskinstation.DTOs
{

    public class ImageDTOCreation : ImageDTO
    {
        public IFormFile ImageData { get; set; }
        public List<Guid>? Tags { get; set; }
    }

    public class ImageDTO
    {
        public Guid GalleryID { get; set; }
    }

    public class ImageDTOID : ImageDTO
    {
        public Guid ImageID { get; set; }
        public string FileID { get; set; }
    }

    public class ImageDTOTags : ImageDTOID
    {
        public List<TagDTOID> Tags { get; set; }
    }
}
