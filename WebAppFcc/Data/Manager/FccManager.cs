using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAppFcc.Shared.Models;
using System.Threading.Tasks;
using System.Linq;
using WebAppFcc.Shared.Interfaces.Managers;
using WebAppFcc.Repository;
using WebAppFcc.Shared.Enums;
using System;

namespace WebAppFcc.Data.Manager
{
    public class FccManager : IFccManager
    {
        private readonly PersonDbContext _repo;

        public FccManager(PersonDbContext repo)
        {
            _repo = repo;
        }

        public async Task<Person> GetPerson(Guid id)
        {
            return await _repo.Person
                .Where(e => e.Id == id)
                .Include(e => e.Relations)
                .ThenInclude(e => e.Invited)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonList()
        {
            return await _repo.Person
                .Include(e => e.Relations)
                .ThenInclude(e => e.Invited)
                .ToArrayAsync();
        }

        public async Task<Person> CreatePerson(Person entity)
        {
            var result = _repo.Person.AddAsync(entity);

            await _repo.SaveChangesAsync();

            if (result.IsCompletedSuccessfully)
                return result.Result.Entity;
            else
                return null;
        }

        public async Task<Person> UpdatePerson(Person entity)
        {
            var result = _repo.Person.Update(entity);
            
            await _repo.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Person> DeletePerson(Guid id)
        {
            Person deleted = await GetPerson(id);
            var result = _repo.Person
                .Remove(deleted);

            await _repo.SaveChangesAsync();

            return result.Entity;
        }
    }
}