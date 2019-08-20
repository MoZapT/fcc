using DomainInfrastructure.Common;
using DomainInfrastructure.Entity;
using DomainInfrastructure.Interfaces.Common;
using DomainInfrastructure.Models.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInfrastructure.Interfaces.Models
{
    public interface IRelatedPersonModel
    {
        int RelatedPersonId { get; set; }
        string Name { get; set; }
        string Lastname { get; set; }
        string Patronym { get; set; }
        IEnumerable<string> RelationDisplayNames { get; set; }
        IEnumerable<IPersonRelationGroupModel> RelationGroups { get; set; }
    }
}
