using DomainInfrastructure.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainInfrastructure.Common
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsActive { get; set; }
    }
}