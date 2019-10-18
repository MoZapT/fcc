using System;
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
                SELECT *
                FROM [Person]
                WHERE 
	                Id = @Id
	                AND IsActive = 1";

            return QueryFoD<Person>(query, new { @Id = id });
        }
        public IEnumerable<Person> ReadAllPerson()
        {
            var query = @"
                SELECT *
                FROM [Person]
                WHERE 
	                IsActive = 1";

            return Query<Person>(query);
        }
        public IEnumerable<Person> ReadAllPersonByRelationGroupId(string id)
        {
            var query = @"
                SELECT p.*
                FROM [Person] AS p
	                JOIN [PersonRelation] AS pr ON p.Id = pr.PersonId
		                AND pr.PersonRelationGroupId = @PersonRelationGroupId
		                AND pr.IsActive = 1
                WHERE 
	                p.IsActive = 1";

            return Query<Person>(query, new { @PersonRelationGroupId = id });
        }
        public IEnumerable<Person> ReadAllPersonByRelationGroupIdExcludeCaller(string id, string callerId)
        {
            var query = @"
                SELECT p.*
                FROM [Person] AS p
	                JOIN [PersonRelation] AS pr ON p.Id = pr.PersonId
		                AND pr.PersonRelationGroupId = @PersonRelationGroupId
		                AND pr.IsActive = 1
                WHERE 
	                p.IsActive = 1
	                AND NOT p.Id = @Id";

            return Query<Person>(query, new { @PersonRelationGroupId = id, @Id = callerId });
        }
        public string CreatePerson(Person entity)
        {
            var query = @"
				INSERT INTO [Person]
                           ([Id]
                           ,[Sex]
                           ,[BirthDate]
                           ,[DeathDate]
                           ,[IsActive]
                           ,[DateCreated]
                           ,[DateModified]
                           ,[Firstname]
                           ,[Lastname]
                           ,[Patronym])
	                 OUTPUT INSERTED.Id
                     VALUES
                           (@Id
                           ,@Sex
                           ,@BirthDate
                           ,@DeathDate
                           ,@IsActive
                           ,@DateCreated
                           ,@DateModified
                           ,@Firstname
                           ,@Lastname
                           ,@Patronym)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }
        public bool UpdatePerson(Person entity)
        {
            var query = @"
                DECLARE @NameChanged bit = 
                (SELECT
                CASE WHEN NOT ([Firstname] = @Firstname 
	                AND [Lastname] = @Lastname 
	                AND [Patronym] = @Patronym) THEN 1
                ELSE 0 END AS NameChanged
                FROM [Person]
                WHERE Id = @Id)

                BEGIN TRAN

                IF @NameChanged = 1
                BEGIN
                INSERT INTO [PersonName]
                        ([Id]
                        ,[PersonId]
                        ,[DateNameChanged]
                        ,[DateCreated]
                        ,[DateModified]
                        ,[IsActive]
                        ,[Firstname]
                        ,[Lastname]
                        ,[Patronym])
		                OUTPUT INSERTED.Id
                    VALUES
                        (NEWID()
                        ,@Id
                        ,GETDATE()
                        ,GETDATE()
                        ,GETDATE()
                        ,1
                        ,(SELECT Firstname FROM [Person] WHERE Id = @Id)
                        ,(SELECT Lastname FROM [Person] WHERE Id = @Id)
                        ,(SELECT Patronym FROM [Person] WHERE Id = @Id))
                END

                UPDATE [Person]
                    SET [Sex] = @Sex
                        ,[BirthDate] = @BirthDate
                        ,[DeathDate] = @DeathDate
                        ,[IsActive] = @IsActive
                        ,[DateCreated] = @DateCreated
                        ,[DateModified] = @DateModified
                        ,[Firstname] = @Firstname
                        ,[Lastname] = @Lastname
                        ,[Patronym] = @Patronym
                    WHERE Id = @Id

                COMMIT TRAN";

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
            var query = @"
                SELECT
	                Id AS 'Key'
	                ,FirstName + ' ' + LastName + ' ' + Patronym AS 'Value'
                FROM [Person]";

            return Query<KeyValuePair<string, string>>(query);
        }
        public IEnumerable<KeyValuePair<string, string>> GetPersonSelectList(string excludePersonId, string search)
        {
            var query = @"
                SELECT
	                Id AS 'Key'
	                ,FirstName + ' ' + LastName + ' ' + Patronym AS 'Value'
                FROM [Person]
                WHERE NOT Id = @ExcludeId
	                AND (Firstname LIKE '%'+@Search+'%' OR LastName LIKE '%'+@Search+'%' OR Patronym LIKE '%'+@Search+'%')";

            return Query<KeyValuePair<string, string>>(query, new { @ExcludeId = excludePersonId, @Search = search });
        }

        #endregion

        #region PersonName

        public PersonName ReadLastPersonName(string id)
        {
            var query = @"
                SELECT TOP 1 *
                FROM [PersonName]
                WHERE PersonId = @Id
                ORDER BY
	                DateCreated DESC";

            return QueryFoD<PersonName>(query, new { @Id = id });
        }
        public IEnumerable<PersonName> ReadAllPersonNameByPersonId(string id)
        {
            var query = @"
                SELECT *
                FROM [PersonName]
                WHERE PersonId = @Id
                ORDER BY
	                DateCreated DESC";

            return Query<PersonName>(query, new { @Id = id });
        }
        public string CreatePersonName(PersonName entity)
        {
            var query = @"
                INSERT INTO [PersonName]
                       ([Id]
                       ,[PersonId]
                       ,[DateNameChanged]
                       ,[DateCreated]
                       ,[DateModified]
                       ,[IsActive]
                       ,[Firstname]
                       ,[Lastname]
                       ,[Patronym])
		               OUTPUT INSERTED.Id
                 VALUES
                       (@Id
                       ,@PersonId
                       ,@DateNameChanged
                       ,@DateCreated
                       ,@DateModified
                       ,@IsActive
                       ,@Firstname
                       ,@Lastname
                       ,@Patronym)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }
        public bool UpdatePersonName(PersonName entity)
        {
            var query = @"
                UPDATE [PersonName]
                SET [DateCreated] = @DateCreated  
                    ,[DateModified] = @DateModified
                    ,[DateNameChanged] = @DateNameChanged
                    ,[IsActive] = @IsActive        
                    ,[FirstName] = @FirstName      
                    ,[LastName] = @LastName        
                    ,[Patronym] = @Patronym        
                WHERE PersonId = @Id";

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }
        public bool DeletePersonName(string id)
        {
            var query = @"
                DELETE FROM [PersonName]
                WHERE PersonId = @Id";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }

        #endregion

        #region PersonRelation

        public PersonRelation ReadPersonRelation(string id)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE Id = @Id";

            return QueryFoD<PersonRelation>(query, new { @Id = id });
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationByPersonId(string id)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE PersonId = @PersonId";

            return Query<PersonRelation>(query, new { @PersonId = id });
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationByGroupId(string id)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE PersonRelationGroupId = @RelationGroupId";

            return Query<PersonRelation>(query, new { @RelationGroupId = id });
        }
        public string CreatePersonRelation(PersonRelation entity)
        {
            string query = @"
                    INSERT INTO [PersonRelation]
                               ([Id]
                               ,[PersonId]
                               ,[PersonRelationGroupId]
                               ,[DateCreated]
                               ,[DateModified]
                               ,[IsActive])
                         OUTPUT INSERTED.Id
                         VALUES
                               (@Id
                               ,@PersonId
                               ,@PersonRelationGroupId
                               ,@DateCreated
                               ,@DateModified
                               ,@IsActive)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }
        public bool UpdatePersonRelation(PersonRelation entity)
        {
            string query = @"
                    UPDATE [PersonRelation]
                       SET [PersonId] = PersonId
                          ,[PersonRelationGroupId] = PersonRelationGroupId
                          ,[DateCreated] = DateCreated
                          ,[DateModified] = DateModified
                          ,[IsActive] = IsActive
                     WHERE Id = @Id";

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }
        public bool DeletePersonRelation(string id)
        {
            string query = @"
                    DELETE FROM [PersonRelation]
                          WHERE Id = @Id";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }
        public bool DeletePersonRelationByPersonId(string id)
        {
            string query = @"
                    DELETE FROM [PersonRelation]
                          WHERE PersonId = @PersonId";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }

        #endregion

        #region PersonRelationGroup

        public PersonRelationGroup ReadPersonRelationGroup(string id)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelationGroup]
                    WHERE Id = @Id";

            return QueryFoD<PersonRelationGroup>(query, new { @Id = id });
        }

        public PersonRelationGroup ReadPersonRelationGroupByPersonAndType(string personId, int type)
        {
            string query = @"
                   SELECT *
                   FROM [PersonRelation] AS pr
                   JOIN [PersonRelationGroup] AS prg
	                   ON pr.PersonRelationGroupId = prg.Id
                   WHERE
	                   prg.RelationType = @Type
	                   AND pr.PersonId = @PersonId";

            return QueryFoD<PersonRelationGroup>(query, new { @PersonId = personId, @Type = type });
        }

        public IEnumerable<PersonRelationGroup> ReadAllPersonRelationGroupsByPersonId(string id)
        {
            string query = @"
                    SELECT prg.*
                    FROM [PersonRelationGroup] AS prg
	                    JOIN [PersonRelation] AS pr ON prg.Id = pr.PersonRelationGroupId
		                    AND PersonId = @PersonId";

            return Query<PersonRelationGroup>(query, new { @PersonId = id });
        }

        public string CreatePersonRelationGroup(PersonRelationGroup entity)
        {
            string query = @"
                    INSERT INTO [PersonRelationGroup]
                               ([Id]
                               ,[DateCreated]
                               ,[DateModified]
                               ,[IsActive]
                               ,[RelationType])
                         OUTPUT INSERTED.Id
                         VALUES
                               (@Id
                               ,@DateCreated
                               ,@DateModified
                               ,@IsActive
                               ,@RelationType)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }

        public bool UpdatePersonRelationGroup(PersonRelationGroup entity)
        {
            string query = @"
                    UPDATE [PersonRelationGroup]
                       SET [DateCreated] = @DateCreated
                          ,[DateModified] = @DateModified
                          ,[IsActive] = @IsActive
                          ,[RelationType] = @RelationType
                     WHERE Id = @Id";

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }

        public bool DeletePersonRelationGroup(string id)
        {
            string query = @"
                    DELETE FROM [PersonRelationGroup]
                          WHERE Id = @Id";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }

        public bool MoveRelationsToOtherRelationGroupAndDelete(string fromId, string toId)
        {
            string query = @"
                    BEGIN TRAN

                    UPDATE [dbo].[PersonRelation]
                    SET [PersonRelationGroupId] = @ToId
                     WHERE [PersonRelationGroupId] = @FromId

                    DELETE FROM [PersonRelationGroup]
                    WHERE Id = @FromId

                    COMMIT TRAN";

            return Execute(query, new { @FromId = fromId, @ToId = toId }) > 0 ? true : false;
        }

        #endregion

    }
}