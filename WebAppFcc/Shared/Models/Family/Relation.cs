using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppFcc.Shared.Common;
using WebAppFcc.Shared.Enums;

namespace WebAppFcc.Shared.Models
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