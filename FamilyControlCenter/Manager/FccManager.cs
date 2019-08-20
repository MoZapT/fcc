using Shared.Interfaces.Common;
using Shared.Interfaces.Repositories;
using DataAccessInfrastructure.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Models;

namespace FamilyControlCenter.Manager
{
    public class FccManager
    {
        private IRepoFactory _repo;

        public FccManager()
        {
#if DEBUG
            _repo = new RepoFactory(typeof(ILocalCashRepository), this.GetType());
#else
            _repo = new RepoFactory(typeof(ISqlRepository), this.GetType());
#endif
        }

        public async Task<Person> GetPerson(string id)
        {
            return await Task.FromResult(_repo.Read<Person>(id));
        }
        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await Task.FromResult(_repo.ReadAll<Person>());
        }
        public async Task<string> SetPerson(Person model)
        {
            return await Task.FromResult(_repo.Create(model));
        }
        public async Task<bool> UpdatePerson(Person model)
        {
            return await Task.FromResult(_repo.Update(model));
        }
        public async Task<bool> DeletePerson(Person model)
        {
            return await Task.FromResult(_repo.Delete(model));
        }
    }
}