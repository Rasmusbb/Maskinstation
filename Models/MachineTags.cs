using Maskinstation.models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Maskinstation.Models
{
    public class MachineTags
    {
        [ForeignKey("TagID")]
        public Guid TagID { get; set; }
        public Tag Tag { get; set; }

        [ForeignKey("MachineID")]

        public Guid MachineID { get; set; }
        public Machine Machine { get; set; }
    }
}
