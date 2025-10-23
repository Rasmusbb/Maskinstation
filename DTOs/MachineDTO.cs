
namespace Maskinstation.DTOs
{
    public class MachineDTO
    {
        public string Model { get; set; }
        public string Description { get; set; }
        public Guid BrandID { get; set; }
        public List<TagDTOID>? Tags { get; set; }
    }

    public class MachineDTOID
    {
        public Guid MachineID { get; set; }
        public Guid UserID { get; set; }
        public BrandDTOID Brand {get; set;}
        public List<ImageDTOID> Images { get; set; }

    }
}
