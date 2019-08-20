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
    public interface IPersonName : IBaseEntity
    {
        string Name { get; set; }
        string Lastname { get; set; }
        string Patronym { get; set; }
    }
}
