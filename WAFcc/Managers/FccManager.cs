using Microsoft.EntityFrameworkCore;
using WAFcc.Models;
using WAFcc.Interfaces.Managers;
using WAFcc.Data;

namespace WAFcc.Managers
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
                .Include(e => e.MainPhoto)
                    .ThenInclude(e => e.FileContent)
                .Include(e => e.Files)
                    .ThenInclude(e => e.FileContent)
                .Include(e => e.Photos)
                    .ThenInclude(e => e.FileContent)
                .Include(e => e.PreviousNames)
                .FirstOrDefaultAsync() ?? new Person();
        }

        public async Task<int> PersonCount()
        {
            return await _repo.Person.CountAsync();
        }

        public async Task<IEnumerable<Person>> GetPersonList(int skip, int take)
        {
            try
            {
                var dbset = _repo.Person
                    .Skip(skip);
                if (take > 0)
                    dbset = dbset.Take(take);

                return await dbset
                    .Include(e => e.Relations)
                    .Include(e => e.MainPhoto)
                        .ThenInclude(e => e.FileContent)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return new List<Person>();
            }

        }

        public async Task<Person> CreatePerson(Person entity)
        {
            var result = _repo.Person.AddAsync(entity);

            await _repo.SaveChangesAsync();

            if (result.IsCompletedSuccessfully)
                return result.Result.Entity;
            else
                return new Person();
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

        public async Task<PersonPhoto> CreatePersonPhoto(PersonPhoto photo)
        {
            var result = _repo.PersonPhoto.AddAsync(photo);

            await _repo.SaveChangesAsync();

            if (result.IsCompletedSuccessfully)
                return result.Result.Entity;
            else
                return null;
        }

        public async Task<PersonDocument> CreatePersonDocument(PersonDocument document)
        {
            var result = _repo.PersonDocument.AddAsync(document);

            await _repo.SaveChangesAsync();

            if (result.IsCompletedSuccessfully)
                return result.Result.Entity;
            else
                return null;
        }

        public async Task<Relation> GetRelation(Guid id)
        {
            return await _repo.Relation
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<PersonRelation> GetPersonRelation(Guid id)
        {
            return await _repo.PersonRelation
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Relation> SetRelation(Relation entity)
        {
            var result = _repo.Relation.Add(entity);
            await _repo.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<PersonRelation> SetPersonRelation(PersonRelation entity)
        {
            var result = _repo.PersonRelation.Add(entity);
            await _repo.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Relation> DeleteRelation(Guid id)
        {
            Relation deleted = await GetRelation(id);
            var result = _repo.Relation
                .Remove(deleted);

            await _repo.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<PersonRelation> DeletePersonRelation(Guid id)
        {
            PersonRelation deleted = await GetPersonRelation(id);
            var result = _repo.PersonRelation
                .Remove(deleted);

            await _repo.SaveChangesAsync();

            return result.Entity;
        }
    }
}