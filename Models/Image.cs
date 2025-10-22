using System.ComponentModel.DataAnnotations;

namespace Maskinstation.models
{
    public class Image
    {
        [Key]
        public Guid ImageID { get; set; }
        public string ImageURL { get; set; }
    }
}
