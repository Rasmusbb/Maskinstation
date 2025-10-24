using System.ComponentModel.DataAnnotations;

namespace Maskinstation.Models
{
    public enum TagType
    {
        User,
        Machine
    }   
    public class Tag
    {
        [Key]
        public Guid TagID { get; set; }
        public string TagName { get; set; }
        public TagType TagType { get; set; }    
    }
}
