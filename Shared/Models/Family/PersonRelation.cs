using Shared.Common;
using System;

namespace Shared.Models
{
    public class PersonRelation : BaseEntity
    {
        public string PersonRelationGroupId { get; set; }
        public string PersonId { get; set; }
        public int RelationTypeId { get; set; }
    }
}