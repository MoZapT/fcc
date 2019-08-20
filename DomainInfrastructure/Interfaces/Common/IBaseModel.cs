using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInfrastructure.Interfaces.Common
{
    public interface IBaseModel
    {
        int Id { get; set; }
        bool IsActive { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }
    }
}
