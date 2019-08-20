using DomainInfrastructure.Common;
using DomainInfrastructure.Entity;
using DomainInfrastructure.Interfaces.Common;
using DomainInfrastructure.Interfaces.Models;
using DomainInfrastructure.Models.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInfrastructure.Models.Family
{
    public class RelatedPersonModel : IRelatedPersonModel
    {
        public int RelatedPersonId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public IEnumerable<string> RelationDisplayNames { get; set; }
        public IEnumerable<IPersonRelationGroupModel> RelationGroups { get; set; }
    }
}
