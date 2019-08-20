using Shared.Common;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public class PersonRelationGroup : BaseModel
    {
        public string PersonId { get; set; }
        public IEnumerable<PersonRelation> Relations { get; set; }
    }
}