using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInfrastructure.Interfaces.Common
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
        bool IsActive { get; set; }
    }
}
