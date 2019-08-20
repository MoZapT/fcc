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
        private ISqlRepository _repo;

        public FccManager()
        {
            //_repo = new SqlRepository();
            _repo = new LocalRepository();
        }

        public async Task<Person> GetPerson(string id)
        {
            return await _repo.ReadPerson(id);
        }
        public async Task<IEnumerable<Person>> GetPersons()
        {
            return await _repo.ReadAllPerson();
        }
        public async Task<string> SetPerson(Person model)
        {
            return await _repo.CreatePerson(model);
        }
        public async Task<bool> UpdatePerson(Person model)
        {
            return await _repo.UpdatePerson(model);
        }
        public async Task<bool> DeletePerson(string id)
        {
            return await _repo.DeletePerson(id);
        }
    }
}