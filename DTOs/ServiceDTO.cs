using Maskinstation.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Maskinstation.DTOs
{
    public class ServiceDTO
    {

        public string ServiceName { get; set; }
        public string Description { get; set; }

    }

    public class ServiceDTOID
    {
        public Guid ServiceID { get; set; }
        public Guid GalleryID { get; set; }
        ICollection<Machine> Machines { get; set; }

    }
}
