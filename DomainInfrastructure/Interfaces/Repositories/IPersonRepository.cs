using FamilyControlCenter.Interfaces.Models;
using FamilyControlCenter.Models.Family;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainInfrastructure.Interfaces.Repositories
{
    interface IPersonRepository
    {

        #region Person

        IPerson Read(int id);
        IEnumerable<IPerson> ReadAll();
        int Create(IPerson entity);
        bool Update(IPerson entity);
        bool Delete(IPerson entity);

        #endregion

    }
}
