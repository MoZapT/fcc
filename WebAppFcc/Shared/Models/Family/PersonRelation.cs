using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Enums;

namespace WebAppFcc.Shared.Models
{
    public class PersonRelation : BaseModel
    {
        public Person Inviter { get; set; }
        public Person Invited { get; set; }

        public RelationType RelationType { get; set; }
        public string InviterId { get; set; }
        public string InvitedId { get; set; }
    }
}