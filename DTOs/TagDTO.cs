using Maskinstation.models;

namespace Maskinstation.DTOs
{
    public class TagDTO
    {
       public string TagName { get; set; }

    }

    public class TagDTOID {
        public Guid TagID { get; set; }
    }

    public class TagDTOType : TagDTOID
    {
        public TagType TagType {get; set;}
    }
}
