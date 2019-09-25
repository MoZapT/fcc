using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Shared.Interfaces.Repositories;
using Shared.Models;

namespace DataAccessInfrastructure.Repositories
{
    public class SqlRepository : SqlBaseRepository, ISqlRepository
    {
        public SqlRepository()
        {
        }

        #region Person

        public Person ReadPerson(string id)
        {
            var query = @"
                SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
                FROM [Person] AS p
                JOIN (SELECT TOP 1 * FROM [PersonName] 
                WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id
                WHERE p.[Id] = @Id";

            return QueryFoD<Person>(query, new { @Id = id });
        }
        public IEnumerable<Person> ReadAllPerson()
        {
            var query = @"
                SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
                FROM [Person] AS p
                JOIN (SELECT TOP 1 * FROM [PersonName] 
                WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id";

            return Query<Person>(query);
        }
        public IEnumerable<Person> ReadAllPersonByRelationGroupId(string id)
        {
            throw new System.NotImplementedException();
        }
        public string CreatePerson(Person entity)
        {
            #region Q1
            var query = @"
                DECLARE @output table (Id int)

                BEGIN TRAN
                INSERT INTO [Person]
                           ([Sex]
                           ,[BornTimeKnown]
                           ,[IsDead]
                           ,[BornTime]
                           ,[DeadTime]
                           ,[IsActive]
                           ,[DateCreated]
                           ,[DateModified])
	                 OUTPUT INSERTED.Id INTO @output
                     VALUES
                           (@Sex
                           ,@BornTimeKnown
                           ,@IsDead
                           ,@BornTime
                           ,@DeadTime
                           ,@IsActive
                           ,@DateCreated
                           ,@DateModified)

                INSERT INTO [PersonName]
                           ([PersonId]
                           ,[Name]
                           ,[Lastname]
                           ,[Patronym]
                           ,[IsActive]
                           ,[DateCreated]
                           ,[DateModified])
                     VALUES
                           ((SELECT Id FROM @output)
                           ,@Name
                           ,@Lastname
                           ,@Patronym
                           ,1
                           ,GETDATE()
                           ,GETDATE())

                COMMIT TRAN";
            #endregion

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }
        public bool UpdatePerson(Person entity)
        {
            #region Q1
            var query = @"
                BEGIN TRAN
                UPDATE [Person]
                    SET [Sex] = @Sex
                        ,[BornTimeKnown] = @BornTimeKnown
                        ,[IsDead] = @IsDead
                        ,[BornTime] = @BornTime
                        ,[DeadTime] = @DeadTime
                        ,[IsActive] = @IsActive
                        ,[DateCreated] = @DateCreated
                        ,[DateModified] = @DateModified
                    WHERE Id = @Id

                IF @NameModified = 1
                BEGIN
                INSERT INTO [PersonName]
                           ([PersonId]
                           ,[Name]
                           ,[Lastname]
                           ,[Patronym]
                           ,[IsActive]
                           ,[DateCreated]
                           ,[DateModified])
                     VALUES
                           (@Id
                           ,@Name
                           ,@Lastname
                           ,@Patronym
                           ,1
                           ,GETDATE()
                           ,GETDATE())
                END

                COMMIT TRAN";
            #endregion

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }
        public bool DeletePerson(string id)
        {
            var query = @"
                DECLARE @groups table (Id int)

                BEGIN TRAN
                DELETE FROM [Person]
                WHERE Id = @Id

                DELETE FROM [PersonName]
                WHERE PersonId = @Id

                DELETE FROM [PersonRelation]
                WHERE PersonId = @Id

                INSERT INTO @groups(Id)
                SELECT prg.Id
                FROM [PersonRelationGroup] AS prg
                JOIN [PersonRelation] AS pr ON prg.Id = pr.Id
                WHERE pr.PersonId = @Id

                DELETE FROM [PersonRelation]
                WHERE PersonRelationGroupId IN (SELECT Id FROM @groups)

                DELETE FROM [PersonRelationGroup]
                WHERE Id IN ((SELECT Id FROM @groups))

                COMMIT TRAN";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }
        public IEnumerable<KeyValuePair<string, string>> GetPersonSelectList()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region PersonName

        public PersonName ReadPersonName(string id)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<PersonName> ReadAllPersonNameByPersonId(string id)
        {
            throw new System.NotImplementedException();
        }
        public string CreatePersonName(PersonName entity)
        {
            throw new System.NotImplementedException();
        }
        public bool UpdatePersonName(PersonName entity)
        {
            throw new System.NotImplementedException();
        }
        public bool DeletePersonName(string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region PersonRelation

        public PersonRelation ReadPersonRelation(string id)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationByPersonId(string id)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationByGroupId(string id)
        {
            throw new System.NotImplementedException();
        }
        public string CreatePersonRelation(PersonRelation entity)
        {
            throw new System.NotImplementedException();
        }
        public bool UpdatePersonRelation(PersonRelation entity)
        {
            throw new System.NotImplementedException();
        }
        public bool DeletePersonRelation(string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region PersonRelationGroup

        public PersonRelationGroup ReadPersonRelationGroup(string id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroupsByPersonId(string id)
        {
            throw new System.NotImplementedException();
        }

        public string CreatePersonRelationGroup(PersonRelationGroup entity)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdatePersonRelationGroup(PersonRelationGroup entity)
        {
            throw new System.NotImplementedException();
        }

        public bool DeletePersonRelationGroup(string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}