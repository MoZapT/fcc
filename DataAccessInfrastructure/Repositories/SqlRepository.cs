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

        public Task<Person> ReadPerson(string id)
        {
            var query = @"
                SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
                FROM [FCC].[dbo].[Person] AS p
                JOIN (SELECT TOP 1 * FROM [FCC].[dbo].[PersonName] 
                WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id
                WHERE p.[Id] = @Id";

            return QueryFoD<Person>(query, new { @Id = id });
        }
        public Task<IEnumerable<Person>> ReadAllPerson()
        {
            var query = @"
                SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
                FROM [FCC].[dbo].[Person] AS p
                JOIN (SELECT TOP 1 * FROM [FCC].[dbo].[PersonName] 
                WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id";

            return Query<Person>(query);
        }
        public Task<string> CreatePerson(Person entity)
        {
            #region Q1
            var query = @"
                DECLARE @output table (Id int)

                BEGIN TRAN
                INSERT INTO [FCC].[dbo].[Person]
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

                INSERT INTO [FCC].[dbo].[PersonName]
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
        public Task<bool> UpdatePerson(Person entity)
        {
            #region Q1
            var query = @"
                BEGIN TRAN
                UPDATE [FCC].[dbo].[Person]
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
                INSERT INTO [FCC].[dbo].[PersonName]
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

            return Task.FromResult(Execute(query, new DynamicParameters(entity)).Result > 0 ? true : false);
        }
        public Task<bool> DeletePerson(string id)
        {
            var query = @"
                DECLARE @groups table (Id int)

                BEGIN TRAN
                DELETE FROM [FCC].[dbo].[Person]
                WHERE Id = @Id

                DELETE FROM [FCC].[dbo].[PersonName]
                WHERE PersonId = @Id

                DELETE FROM [FCC].[dbo].[PersonRelation]
                WHERE PersonId = @Id

                INSERT INTO @groups(Id)
                SELECT prg.Id
                FROM [FCC].[dbo].[PersonRelationGroup] AS prg
                JOIN [FCC].[dbo].[PersonRelation] AS pr ON prg.Id = pr.Id
                WHERE pr.PersonId = @Id

                DELETE FROM [FCC].[dbo].[PersonRelation]
                WHERE PersonRelationGroupId IN (SELECT Id FROM @groups)

                DELETE FROM [FCC].[dbo].[PersonRelationGroup]
                WHERE Id IN ((SELECT Id FROM @groups))

                COMMIT TRAN";

            return Task.FromResult(Execute(query, new { @Id = id }).Result > 0 ? true : false);
        }

        #endregion

        #region PersonName

        public Task<PersonName> ReadPersonName(string id)
        {
            throw new System.NotImplementedException();
        }
        public Task<IEnumerable<PersonName>> ReadAllPersonName()
        {
            throw new System.NotImplementedException();
        }
        public Task<int> CreatePersonName(PersonName entity)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> UpdatePersonName(PersonName entity)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> DeletePersonName(string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region PersonRelationGroup

        public Task<PersonRelationGroup> ReadPersonRelationGroup(string id)
        {
            throw new System.NotImplementedException();
        }
        public Task<IEnumerable<PersonRelationGroup>> ReadAllPersonRelationGroup()
        {
            throw new System.NotImplementedException();
        }
        public Task<int> CreatePersonRelationGroup(PersonRelationGroup entity)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> UpdatePersonRelationGroup(PersonRelationGroup entity)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> DeletePersonRelationGroup(string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region PersonRelation

        public Task<PersonRelation> ReadPersonRelation(string id)
        {
            throw new System.NotImplementedException();
        }
        public Task<IEnumerable<PersonRelation>> ReadAllPersonRelation()
        {
            throw new System.NotImplementedException();
        }
        public Task<int> CreatePersonRelation(PersonRelation entity)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> UpdatePersonRelation(PersonRelation entity)
        {
            throw new System.NotImplementedException();
        }
        public Task<bool> DeletePersonRelation(string id)
        {
            throw new System.NotImplementedException();
        }

        #endregion

    }
}