using DomainInfrastructure.Common;
using DomainInfrastructure.Interfaces.Common;
using DomainInfrastructure.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainInfrastructure.Entity
{
    public class Person : BaseEntity, IPerson
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Patronym { get; set; }
        public bool NameModified { get; set; }
        public bool BornTimeKnown { get; set; }
        public bool IsDead { get; set; }
        public DateTime? BornTime { get; set; }
        public DateTime? DeadTime { get; set; }
        public bool Sex { get; set; }
    }
}