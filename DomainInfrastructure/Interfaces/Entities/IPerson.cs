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
    public interface IPerson : IBaseEntity
    {
        string Name { get; set; }
        string Lastname { get; set; }
        string Patronym { get; set; }
        bool NameModified { get; set; }
        bool BornTimeKnown { get; set; }
        bool IsDead { get; set; }
        DateTime? BornTime { get; set; }
        DateTime? DeadTime { get; set; }
        bool Sex { get; set; }
    }
}
