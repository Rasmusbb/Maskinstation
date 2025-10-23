using Maskinstation.models;

namespace Maskinstation.DTOs
{
    public class ImageDTO
    {
        public string ImageURL { get; set; }
    }
    public class ImageDTOID : ImageDTO
    {
        public Guid ImageID { get; set; }
    }
}
