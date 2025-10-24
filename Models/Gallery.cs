

namespace Maskinstation.Models
{
    public class Gallery
    {
        public Guid GalleryID { get; set; }
        public string Name { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
