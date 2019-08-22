using Shared.Common;
using Shared.Enums;
using System;

namespace Shared.Models
{
    public class PersonRelation : BaseModel
    {
        public Person Member { get; set; }
        public string PersonRelationGroupId { get; set; }
        public string PersonId { get; set; }
    }
}