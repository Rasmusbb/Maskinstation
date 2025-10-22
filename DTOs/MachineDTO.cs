using Maskinstation.models;



namespace Maskinstation.DTOs
{
    public class MachineDTO
    {
        public string Model { get; set; }
        public string Description { get; set; }
        public Guid BrandID { get; set; }
        public Brand Brand { get; set; }
        public List<TagDTOID> Tags { get; set; }
    }

    public class MachineDTOID
    {
        public Guid MachineID { get; set; }
        public Guid UserID { get; set; }
        public User User { get; set; }
        public List<Image> images { get; set; }
    }
}
