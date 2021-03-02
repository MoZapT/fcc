using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAppFcc.Shared.Models;
using System.Threading.Tasks;
using System.Linq;
using WebAppFcc.Shared.Interfaces.Managers;
using WebAppFcc.Repository;
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
                .Include(e => e.InviterRelations)
                .Include(e => e.InvitedRelations)
                .Include(e => e.MainPhoto)
                    .ThenInclude(e => e.FileContent)
                .Include(e => e.Files)
                    .ThenInclude(e => e.FileContent)
                .Include(e => e.Photos)
                    .ThenInclude(e => e.FileContent)
                .Include(e => e.PreviousNames)
                .FirstOrDefaultAsync();
        }

        public async Task<int> PersonCount()
        {
            return await _repo.Person.CountAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonList(int skip, int take)
        {
            var dbset = _repo.Person
                .Skip(skip);
            if (take > 0)
                dbset = dbset.Take(take);

            return await dbset
                .Include(e => e.InviterRelations)
                .Include(e => e.InvitedRelations)
                .Include(e => e.MainPhoto)
                    .ThenInclude(e => e.FileContent)
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
            if (entity.NameHasChanged)
            {
                _repo.PersonName.Add(new PersonName()
                {
                    Firstname = entity.Firstname,
                    Lastname = entity.Lastname,
                    Patronym = entity.Patronym,
                    PersonId = entity.Id,
                    DateNameChanged = DateTime.Now,
                });
            }

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