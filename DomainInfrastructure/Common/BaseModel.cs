using FamilyControlCenter.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainInfrastructure.Common
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public BaseModel()
        {
            IsActive = true;
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}