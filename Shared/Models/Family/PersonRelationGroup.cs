using Shared.Common;
using Shared.Enums;
using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public class PersonRelationGroup : BaseModel
    {
        public List<PersonRelation> Relations { get; set; }
        public RelationType RelationTypeId { get; set; }
    }
}