using Maskinstation.Models;

namespace Maskinstation.DTOs
{
    public class RoleDTO
    {
        public string RoleName { get; set; }
    }

    public class RoleDTOID : RoleDTO
    {
        public Guid RoleID { get; set; }
    }
}
