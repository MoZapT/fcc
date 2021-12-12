namespace WAFcc.Models
{
    public class PersonRelation
    {
        public Guid InviterId { get; set; }
        public Person Inviter { get; set; }

        public Guid InvitedId { get; set; }
        public Person Invited { get; set; }
    }
}