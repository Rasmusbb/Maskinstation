
using Maskinstation.Models;

namespace Maskinstation.DTOs
{
    public class MachineDTO
    {
        public string Model { get; set; }
        public string Description { get; set; }
        public Guid BrandID { get; set; }
    }

    public class MachineDTOID : MachineDTO
    {
        public Guid MachineID { get; set; }
        public Guid UserID { get; set; }
        public Guid GalleryID { get; set; }
    }

}
