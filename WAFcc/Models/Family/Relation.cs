using WAFcc.Enums;

namespace WAFcc.Models
{
    public class Relation : BaseModel
    {
        public Guid InviterId { get; set; }
        public Person Inviter { get; set; }

        public Guid InvitedId { get; set; }
        public Person Invited { get; set; }

        public RelationType RelationType { get; set; }
    }
}