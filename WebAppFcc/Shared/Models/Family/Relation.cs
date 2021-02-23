using System.ComponentModel.DataAnnotations.Schema;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Enums;

namespace WebAppFcc.Shared.Models
{
    public class Relation : BaseModel
    {
        public RelationType RelationType { get; set; }
        [NotMapped]
        public Person Inviter { get; set; }
        [NotMapped]
        public Person Invited { get; set; }
    }
}