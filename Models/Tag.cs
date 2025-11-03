using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public bool deletable { get; set; }
        public ICollection<Machine> Machines { get; set; }
        public ICollection<Image> images { get; set; }
        public ICollection<User> User { get; set; }
    }
}
