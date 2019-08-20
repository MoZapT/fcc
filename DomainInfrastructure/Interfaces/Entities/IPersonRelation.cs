using DomainInfrastructure.Common;
using DomainInfrastructure.Interfaces.Common;
using DomainInfrastructure.Models.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInfrastructure.Interfaces.Models
{
    public interface IPersonRelation : IBaseEntity
    {
        int PersonId { get; set; }
        int PersonRelationGroupId { get; set; }
        int RelationTypeId { get; set; }
    }
}
