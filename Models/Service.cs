using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public enum Unit
    {
        stk,
        HA,
        none
    }

    public class Service
    {
        [Key]
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Unit Unit {get; set;}
        public string Description { get; set; }
        ICollection<Machine> Machines { get; set; }

        [ForeignKey("GalleryID")]
        public Guid GalleryID { get; set;  }
        public Gallery Gallery { get; set; }

    }
}
