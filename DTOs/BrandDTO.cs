namespace Maskinstation.DTOs
{
    public class BrandDTO
    {
        public string BrandName { get; set; }
        public string ImageID { get; set; }
    }
    public class BrandDTOID : BrandDTO
    {
        public Guid BrandID { get; set; }
    }
}
