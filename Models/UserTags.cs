using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public class UserTags
    {
        [ForeignKey("TagID")]
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }    

        [ForeignKey("UserID")]
        public Guid UserID { get; set; }
        public User User { get; set; }  

    }
}
