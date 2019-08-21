using Shared.Common;
using Shared.Enums;
using System;

namespace Shared.Models
{
    public class PersonRelation : BaseModel
    {
        public string OwnerId { get; set; }
        public string PersonId { get; set; }
        public string Name { get; set; } //not in DB
        public string Lastname { get; set; } //not in DB
        public string Patronym { get; set; } //not in DB
        public RelationType RelationTypeId { get; set; }
    }
}