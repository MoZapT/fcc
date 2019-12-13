using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Shared.Enums;
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
        public IEnumerable<Person> ReadAllPersonByRelation(string personId, RelationType type)
        {
            var query = @"
                SELECT per.*
                FROM [PersonRelation] AS rel
                JOIN [Person] AS per
	                ON per.Id = rel.InvitedId
                WHERE 
	                rel.InviterId = @Id
                    AND rel.RelationType = @RelationType
	                AND rel.IsActive = 1
	                AND per.IsActive = 1";

            return Query<Person>(query, new { @Id = personId, @RelationType = type});
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
                        ,[FileContentId] = @FileContentId
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
        public IEnumerable<KeyValuePair<string, string>> GetOnlyPossiblePersonSelectList(string excludePersonId, string search)
        {
            var query = @"
                DECLARE @list table(Id nvarchar(128))
                INSERT INTO @list
                SELECT InvitedId
                FROM [PersonRelation]
                WHERE InviterId = @ExcludeId

                SELECT
                Id AS 'Key'
                ,ISNULL(FirstName, '') + ' ' + ISNULL(LastName, '') + ' ' + ISNULL(Patronym, '') AS 'Value'
                FROM [Person]
                WHERE 
                    NOT Id = @ExcludeId
                    AND NOT Id IN(SELECT * FROM @list)
                    AND (Firstname LIKE '%'+@Search+'%' 
                    OR LastName LIKE '%'+@Search+'%'
                    OR Patronym LIKE '%'+@Search+'%')";

            return Query<KeyValuePair<string, string>>(query, new { @ExcludeId = excludePersonId, @Search = search });
        }

        public FileContent ReadFileContentByPersonId(string id)
        {
            var query = @"
                SELECT fc.*
                FROM [FileContent] AS fc
                JOIN [Person] AS p
	                ON fc.Id = p.FileContentId
                WHERE 
	                p.Id = @Id";

            return QueryFoD<FileContent>(query, new { @Id = id });
        }
        public IEnumerable<FileContent> ReadAllFileContentByPersonId(string id)
        {
            var query = @"
                SELECT fc.*
                FROM [FileContent] AS fc
                JOIN [PersonFileContent] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonId = @Id";

            return Query<FileContent>(query, new { @Id = id });
        }
        public string CreatePersonFileContent(string personId, string fileId)
        {
            var query = @"
                INSERT INTO [dbo].[PersonFileContent]
                    ([Id]
                    ,[PersonId]
                    ,[FileContentId])
                OUTPUT INSERTED.Id
                VALUES
                    (NEWID()
                    ,@Id
                    ,@FileContentId)";

            return QueryFoD<string>(query, new { @Id = personId, @FileContentId = fileId });
        }
        public bool DeletePersonFileContent(string personId, string fileId)
        {
            var query = @"
                DELETE FROM [PersonFileContent]
                WHERE PersonId = @Id AND FileContentId = @FileContentId";

            return Execute(query, new { @Id = personId, @FileContentId = fileId }) > 0 ? true : false;
        }
        public bool DeleteAllPersonFileContent(string personId)
        {
            var query = @"
                DELETE FROM [PersonFileContent]
                WHERE PersonId = @Id";

            return Execute(query, new { @Id = personId }) > 0 ? true : false;
        }

        public PersonDocument ReadDocumentByPersonId(string id)
        {
            var query = @"
                SELECT fc.*
                FROM [PersonDocument] AS fc
                JOIN [Person] AS p
	                ON fc.Id = p.FileContentId
                WHERE 
	                p.Id = @Id";

            return QueryFoD<PersonDocument>(query, new { @Id = id });
        }
        public IEnumerable<PersonDocument> ReadAllDocumentByPersonId(string id)
        {
            var query = @"
                SELECT fc.*
                FROM [FileContent] AS fc
                JOIN [PersonDocument] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonId = @Id";

            return Query<PersonDocument>(query, new { @Id = id });
        }
        public IEnumerable<PersonDocument> ReadAllDocumentByPersonIdAndCategory(string id, string category)
        {
            var query = @"
                SELECT fc.*
                FROM [FileContent] AS fc
                JOIN [PersonDocument] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonId = @Id
	                AND pfc.CategoryName = @Category";

            return Query<PersonDocument>(query, new { @Id = id, @Category = category });
        }       
        public string CreatePersonDocument(string personId, string fileId, string category, string activityId = null)
        {
            var query = @"
                INSERT INTO [dbo].[PersonDocument]
                    ([Id]
                    ,[PersonId]
                    ,[FileContentId]
					,[CategoryName]
					,[PersonActivityId])
                OUTPUT INSERTED.Id
                VALUES
                    (NEWID()
                    ,@Id
                    ,@FileContentId
					,@Category
					,@ActivityId)";

            return QueryFoD<string>(query, 
                new { @Id = personId, @FileContentId = fileId, @Category = category, @ActivityId = activityId });
        }
        public bool DeletePersonDocument(string personId, string fileId)
        {
            var query = @"
                DELETE FROM [PersonDocument]
                WHERE PersonId = @Id AND FileContentId = @FileContentId";

            return Execute(query, new { @Id = personId, @FileContentId = fileId }) > 0 ? true : false;
        }
        public bool DeleteAllPersonDocument(string personId)
        {
            var query = @"
                DELETE FROM [PersonDocument]
                WHERE PersonId = @Id";

            return Execute(query, new { @Id = personId }) > 0 ? true : false;
        }

        public IEnumerable<string> ReadAllDocumentCategories(string search)
        {
            var query = @"
                SELECT CategoryName
                FROM [PersonDocument]
                WHERE CategoryName LIKE '%'+@Search+'%'
                GROUP BY CategoryName
                ORDER BY CategoryName DESC";

            return Query<string>(query, new { @Search = search });
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
                WHERE Id = @Id";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }

        #endregion

        #region PersonRelation

        public PersonRelation ReadPersonRelation(string inviter, string invited, RelationType type)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId
                    AND InvitedId = @InvitedId
                    AND RelationType = @RelationType";

            return QueryFoD<PersonRelation>(query, 
                new { @InviterId = inviter, @InvitedId = invited, @RelationType = type });
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationBetweenInviterAndInvited(string inviter, string invited)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId
                    AND InvitedId = @InvitedId";

            return Query<PersonRelation>(query,
                new { @InviterId = inviter, @InvitedId = invited });
        }
        public IEnumerable<PersonRelation> ReadAllPersonRelationsByInviterId(string id)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE 
                        InviterId = @PersonId";

            return Query<PersonRelation>(query, new { @PersonId = id });
        }

        public IEnumerable<PersonRelation> ReadAllPersonRelationsThatExistByRelatedPerson(string personId)
        {
            string query = @"
                    DECLARE @table table (Id nvarchar(128))

                    INSERT INTO @table
                    SELECT InvitedId
                    FROM [FCC].[dbo].[PersonRelation]
                    WHERE InviterId = @PersonId

                    SELECT *
                    FROM [FCC].[dbo].[PersonRelation]
                    WHERE InviterId IN (SELECT * FROM @table)
                    AND NOT InvitedId = @PersonId";

            return Query<PersonRelation>(query, new { @PersonId = personId });
        }

        public string CreatePersonRelation(PersonRelation entity)
        {
            string query = @"
                    INSERT INTO [PersonRelation]
                               ([Id]
                               ,[InviterId]
                               ,[InvitedId]
                               ,[RelationType]
                               ,[DateCreated]
                               ,[DateModified]
                               ,[IsActive])
                         OUTPUT INSERTED.Id
                         VALUES
                               (@Id
                               ,@InviterId
                               ,@InvitedId
                               ,@RelationType
                               ,@DateCreated
                               ,@DateModified
                               ,@IsActive)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }
        public bool UpdatePersonRelation(PersonRelation entity)
        {
            string query = @"
                    UPDATE [PersonRelation]
                       SET [InviterId] = @InviterId
                          ,[InvitedId] = @InvitedId
                          ,[RelationType] = @RelationType
                          ,[DateCreated] = @DateCreated
                          ,[DateModified] = @DateModified
                          ,[IsActive] = @IsActive
                     WHERE Id = @Id";

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }
        public bool DeletePersonRelation(string inviter, string invited)
        {
            string query = @"
                    DELETE FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId
                    AND InvitedId = @InvitedId";

            return Execute(query,
                new { @InviterId = inviter, @InvitedId = invited })
                > 0 ? true : false;
        }
        public bool DeletePersonRelation(string inviter, string invited, RelationType type)
        {
            string query = @"
                    DELETE FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId 
                    AND InvitedId = @InvitedId
                    AND RelationType = @RelationType";

            return Execute(query,
                new { @InviterId = inviter, @InvitedId = invited, @RelationType = type })
                > 0 ? true : false;
        }
        public bool IsMarried(string personId)
        {
            string query = @"
				   SELECT COUNT(Id)
                   FROM [PersonRelation]
                   WHERE 
                       InviterId = @PersonId
                   AND RelationType = @RelationType";

            return QueryFoD<int>(query, new { @PersonId = personId, @RelationType = RelationType.HusbandWife })
                > 0 ? true : false;
        }

        #endregion

        #region PersonBiography

        public PersonBiography ReadPersonBiographyByPersonId(string personId)
        {
            string query = @"
                    SELECT *
                    FROM [PersonBiography]
                    WHERE 
                        PersonId = @PersonId";

            return QueryFoD<PersonBiography>(query, new { @PersonId = personId});
        }

        public PersonBiography ReadPersonBiography(string biographyId)
        {
            string query = @"
                    SELECT *
                    FROM [PersonBiography]
                    WHERE 
                        Id = @Id";

            return QueryFoD<PersonBiography>(query, new { @Id = biographyId });
        }

        public string CreatePersonBiography(PersonBiography entity)
        {
            string query = @"
                   INSERT INTO [PersonBiography]
                       ([Id]
                       ,[PersonId]
                       ,[BiographyText]
                       ,[DateCreated]
                       ,[DateModified]
                       ,[IsActive])
                   OUTPUT INSERTED.Id
                   VALUES
                       (@Id
                       ,@PersonId
                       ,@BiographyText
                       ,@DateCreated
                       ,@DateModified
                       ,@IsActive)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }

        public bool UpdatePersonBiography(PersonBiography entity)
        {
            string query = @"
                    UPDATE [PersonBiography]
                       SET [PersonId] = @PersonId
                          ,[BiographyText] = @BiographyText
                          ,[DateCreated] = @DateCreated
                          ,[DateModified] = @DateModified
                          ,[IsActive] = @IsActive
                     WHERE Id = @Id";

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }

        #endregion

        #region PersonActivity

        public PersonActivity ReadPersonActivity(string id)
        {
            var query = @"
                SELECT *
                FROM [PersonActivity]
                WHERE [Id] = @Id";

            return QueryFoD<PersonActivity>(query, new { @Id = id });
        }

        public IEnumerable<PersonActivity> ReadAllPersonActivityByPerson(string id)
        {
            var query = @"
                SELECT *
                FROM [PersonActivity] AS pa
                JOIN [PersonBiography] AS pb
	                ON pa.BiographyId = pb.Id 
	                AND pb.PersonId = @Id";

            return Query<PersonActivity>(query, new { @Id = id });
        }

        public IEnumerable<PersonActivity> ReadAllPersonActivityByPerson(string id, ActivityType type)
        {
            var query = @"
                SELECT *
                FROM [PersonActivity] AS pa
                JOIN [PersonBiography] AS pb
	                ON pa.BiographyId = pb.Id 
	                AND pb.PersonId = @Id
	                AND pa.ActivityType = @Type";

            return Query<PersonActivity>(query, new { @Id = id, @Type = type });
        }

        public string CreatePersonActivity(PersonActivity entity)
        {
            var query = @"
                INSERT INTO [PersonActivity]
                    ([Id]
                    ,[BiographyId]
                    ,[DateCreated]
                    ,[DateModified]
                    ,[IsActive]
                    ,[Activity]
                    ,[ActivityType]
                    ,[HasBegun]
                    ,[DateBegin]
                    ,[HasEnded]
                    ,[DateEnd])
                OUTPUT INSERTED.Id
                VALUES
                    (@Id
                    ,@BiographyId
                    ,@DateCreated
                    ,@DateModified
                    ,@IsActive
                    ,@Activity
                    ,@ActivityType
                    ,@HasBegun
                    ,@DateBegin
                    ,@HasEnded
                    ,@DateEnd)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }

        public bool UpdatePersonActivity(PersonActivity entity)
        {
            var query = @"
                UPDATE [PersonActivity]
                SET [BiographyId] = @BiographyId
                    ,[DateCreated] = @DateCreated
                    ,[DateModified] = @DateModified
                    ,[IsActive] = @IsActive
                    ,[Activity] = @Activity
                    ,[ActivityType] = @ActivityType
                    ,[HasBegun] = @HasBegun
                    ,[DateBegin] = @DateBegin
                    ,[HasEnded] = @HasEnded
                    ,[DateEnd] = @DateEnd
                 WHERE Id = @Id";

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }

        public bool DeletePersonActivity(string id)
        {
            var query = @"
                DELETE FROM [PersonActivity]
                WHERE Id = @Id";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }

        public bool DeleteAllPersonActivityByPersonId(string id)
        {
            var query = @"
                DELETE FROM [PersonActivity]
                WHERE BiographyId = (SELECT Id FROM [PersonBiography] WHERE PersonId = @Id)";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }

        #endregion

        #region FileContent

        public FileContent ReadFileContent(string id)
        {
            var query = @"
                SELECT *
                FROM [FileContent]
                WHERE 
                    Id = @Id";

            return QueryFoD<FileContent>(query, new { @Id = id });
        }

        public string CreateFileContent(FileContent entity)
        {
            var query = @"
                INSERT INTO [FileContent]
                    ([Id]
                    ,[DateCreated]
                    ,[DateModified]
                    ,[IsActive]
                    ,[BinaryContent]
                    ,[FileType]
                    ,[Name])
                OUTPUT INSERTED.Id
                VALUES
                    (@Id
                    ,@DateCreated
                    ,@DateModified
                    ,@IsActive
                    ,@BinaryContent
                    ,@FileType
                    ,@Name)";

            return QueryFoD<string>(query, new DynamicParameters(entity));
        }

        public bool UpdateFileContent(FileContent entity)
        {
            var query = @"
                UPDATE [FileContent]
                SET [DateCreated] = @DateCreated
                    ,[DateModified] = @DateModified
                    ,[IsActive] = @IsActive
                    ,[BinaryContent] = @BinaryContent
                    ,[FileType] = @FileType
                    ,[Name] = @Name
                WHERE
                    Id = @Id";

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }

        public bool DeleteFileContent(string id)
        {
            var query = @"
                DELETE FROM [FileContent]
                WHERE Id = @Id";

            return Execute(query, new { @Id = id }) > 0 ? true : false;
        }

        #endregion

    }
}