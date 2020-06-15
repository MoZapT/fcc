using Shared.Common;
using Shared.Enums;

namespace Shared.Models
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