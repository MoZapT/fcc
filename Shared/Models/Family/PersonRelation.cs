using Shared.Common;
using Shared.Enums;
using System;

namespace Shared.Models
{
    public class PersonRelation : BaseModel
    {
        public string OwnerId { get; set; }
        public string PersonId { get; set; }
        public RelationType RelationTypeId { get; set; }
    }
}