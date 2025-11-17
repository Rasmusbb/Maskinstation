using System.ComponentModel.DataAnnotations;

namespace Maskinstation.Models
{
    public class Role
    {
        [Key]
        public Guid RoleID { get; set; }

        public string RoleName {get; set;}
        public bool deletable { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
