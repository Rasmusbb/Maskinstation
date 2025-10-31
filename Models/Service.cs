using System.ComponentModel.DataAnnotations;

namespace Maskinstation.Models
{
    public class Service
    {
        [Key]
        public Guid ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        ICollection<Machine> Machines { get; set; }
    }
}
