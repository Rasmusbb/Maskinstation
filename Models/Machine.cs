using Maskinstation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.models
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
        public ICollection <MachineTags> MachineTags { get; set; }
        public ICollection<Image> images { get; set; }  

    }
}
