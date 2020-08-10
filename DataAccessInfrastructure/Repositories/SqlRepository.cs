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

        public async Task<Person> ReadPerson(string id)
        {
            var query = @"SELECT * FROM [dbo].[ReadPerson](@Id, @MarriedType, @InRelationType, default)";

            return await QueryFoD<Person>(query, new 
            { 
                @Id = id, 
                @MarriedType = RelationType.HusbandWife, 
                @InRelationType = RelationType.LivePartner 
            });
        }
        public async Task<IEnumerable<Person>> ReadAllPerson()
        {
            var query = @"SELECT * FROM [dbo].[ReadPerson](default, @MarriedType, @InRelationType, default)";

            return await Query<Person>(query, new
            {
                @MarriedType = RelationType.HusbandWife,
                @InRelationType = RelationType.LivePartner
            });
        }
        public async Task<IEnumerable<Person>> ReadAllPersonByRelation(string personId, RelationType type)
        {
            var query = @"
                SELECT per.*
                FROM (SELECT * FROM [dbo].[ReadPersonRelation](@Id, default, @RelationType)) AS rel
                JOIN (SELECT * FROM [dbo].[ReadPerson](default, @MarriedType, @InRelationType, default)) AS per
	                ON per.Id = rel.InvitedId";

            return await Query<Person>(query, 
                new { 
                    @Id = personId, 
                    @RelationType = type,
                    @MarriedType = RelationType.HusbandWife,
                    @InRelationType = RelationType.LivePartner
                });
        }
        public async Task<string> CreatePerson(Person entity)
        {
            var parameters = new DynamicParameters(entity);
            parameters.Add("@RetVal", string.Empty, direction: System.Data.ParameterDirection.Output);
            await Execute("Insert_Person", parameters, System.Data.CommandType.StoredProcedure);

            return parameters.Get<string>("@RetVal");
        }
        public async Task<bool> UpdatePerson(Person entity)
        {
            string query = @"
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
                    WHERE Id = @Id";

            return await Execute(query, new DynamicParameters(entity)) > 0;
        }
        public async Task<bool> DeletePerson(string id)
        {
            var query = @"
                DELETE FROM [Person]
                WHERE Id = @Id";

            return await Execute(query, new { @Id = id }) > 0;
        }
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonSelectList()
        {
            var query = @"
                SELECT
	                Id AS 'Key'
	                ,FirstName + ' ' + LastName + ' ' + Patronym AS 'Value'
                FROM (SELECT * FROM [dbo].[ReadPerson](default, default, default, default))";

            return await Query<KeyValuePair<string, string>>(query);
        }
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonSelectList(string excludePersonId, string search)
        {
            var query = @"
                SELECT
	                Id AS 'Key'
	                ,FirstName + ' ' + LastName + ' ' + Patronym AS 'Value'
                FROM (SELECT * FROM [dbo].[ReadPerson](@ExcludeId, default, default, @Search))";

            return await Query<KeyValuePair<string, string>>(query, new { @ExcludeId = excludePersonId, @Search = search });
        }
        public async Task<IEnumerable<KeyValuePair<string, string>>> GetOnlyPossiblePersonSelectList(string excludePersonId, string search)
        {
            var query = @"
                DECLARE @list table(Id nvarchar(128))
                INSERT INTO @list
                SELECT InvitedId FROM [dbo].[ReadPersonRelation](@ExcludeId, default, default)

                SELECT
                    Id AS 'Key'
                    ,ISNULL(FirstName, '') + ' ' + ISNULL(LastName, '') + ' ' + ISNULL(Patronym, '') AS 'Value'
                FROM (SELECT * FROM [dbo].[ReadPerson](default, default, default, @Search)) AS tmp
                WHERE 
                    NOT Id = @ExcludeId
                    AND NOT Id IN(SELECT * FROM @list)";

            return await Query<KeyValuePair<string, string>>(query, new { @ExcludeId = excludePersonId, @Search = search });
        }
        public async Task<int> GetOnlyPossiblePersonCount(string excludePersonId)
        {
            var query = @"
                DECLARE @list table(Id nvarchar(128))
                INSERT INTO @list
                SELECT InvitedId [dbo].[ReadPersonRelation](@ExcludeId, default, default)

                SELECT
	                COUNT(Id)
                FROM (SELECT * FROM [dbo].[ReadPerson](default, default, default, default)) AS tmp
                WHERE 
                    NOT Id = @ExcludeId
                    AND NOT Id IN(SELECT * FROM @list)";

            return await QueryFoD<int>(query, new { @ExcludeId = excludePersonId });
        }

        public async Task<FileContent> ReadPhotoByPersonId(string id)
        {
            var query = @"
                SELECT fc.*
                FROM (SELECT * FROM [dbo].[ReadFileContent]()) AS fc
                JOIN (SELECT * FROM [dbo].[ReadPerson](@Id, default, default, default)) AS p
	                ON fc.Id = p.FileContentId";

            return await QueryFoD<FileContent>(query, new { @Id = id });
        }
        public async Task<IEnumerable<FileContent>> ReadAllPhotoByPersonId(string id)
        {
            var query = @"
                SELECT fc.*
                FROM (SELECT * FROM [dbo].[ReadFileContent]()) AS fc
                JOIN [PersonPhoto] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonId = @Id";

            return await Query<FileContent>(query, new { @Id = id });
        }
        public async Task<string> CreatePersonPhoto(string personId, FileContent entity)
        {
            var parameters = new DynamicParameters(entity);
            parameters.Add("@PersonId", personId);
            parameters.Add("@RetVal", string.Empty, direction: System.Data.ParameterDirection.Output);
            await Execute("Insert_PersonFileContent", parameters, System.Data.CommandType.StoredProcedure);

            return parameters.Get<string>("@RetVal");
        }
        public async Task<bool> DeleteAllPersonPhoto(string personId)
        {
            var query = @"
                DELETE FROM [PersonPhoto]
                WHERE PersonId = @Id";

            return await Execute(query, new { @Id = personId }) > 0;
        }

        public async Task<PersonDocument> ReadDocumentByPersonId(string id)
        {
            var query = @"
                SELECT fc.*
                FROM [PersonDocument] AS fc
                WHERE 
	                fc.PersonId = @Id";

            return await QueryFoD<PersonDocument>(query, new { @Id = id });
        }
        public async Task<IEnumerable<PersonDocument>> ReadAllDocumentByPersonId(string id)
        {
            var query = @"
                SELECT fc.*, pfc.PersonActivityId
                FROM (SELECT * FROM [dbo].[ReadFileContent]()) AS fc
                JOIN [PersonDocument] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonId = @Id";

            return await Query<PersonDocument>(query, new { @Id = id });
        }
        public async Task<IEnumerable<PersonDocument>> ReadAllDocumentByPersonIdAndCategory(string id, string category)
        {
            var query = @"
                SELECT fc.*, pfc.PersonActivityId
                FROM (SELECT * FROM [dbo].[ReadFileContent]()) AS fc
                JOIN [PersonDocument] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonId = @Id
	                AND pfc.CategoryName = @Category";

            return await Query<PersonDocument>(query, new { @Id = id, @Category = category });
        }
        public async Task<IEnumerable<PersonDocument>> ReadAllDocumentByActivity(string activity)
        {
            var query = @"
                SELECT fc.*, pfc.PersonActivityId
                FROM (SELECT * FROM [dbo].[ReadFileContent]()) AS fc
                JOIN [PersonDocument] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonActivityId = @Activity";

            return await Query<PersonDocument>(query, new { @Activity = activity });
        }
        public async Task<IEnumerable<PersonDocument>> ReadAllDocumentByPersonIdAndActivity(string id, string activity)
        {
            var query = @"
                SELECT fc.*, pfc.PersonActivityId
                FROM (SELECT * FROM [dbo].[ReadFileContent]()) AS fc
                JOIN [PersonDocument] AS pfc
	                ON fc.Id = pfc.FileContentId
                WHERE 
	                pfc.PersonId = @Id
	                AND ISNULL(pfc.PersonActivityId, '') = ISNULL(@Activity, '')";

            return await Query<PersonDocument>(query, new { @Id = id, @Activity = activity });
        }
        public async Task<string> CreatePersonDocument(FileContent content, string personId, string activityId = null)
        {
            var parameters = new DynamicParameters(content);
            parameters.Add("@PersonId", personId);
            parameters.Add("@ActivityId", activityId);
            parameters.Add("@RetVal", string.Empty, direction: System.Data.ParameterDirection.Output);
            await Execute("Insert_PersonDocument", parameters, System.Data.CommandType.StoredProcedure);

            return parameters.Get<string>("@RetVal");

        }
        public async Task<bool> DeletePersonDocument(string personId, string fileId)
        {
            var query = @"
                DELETE FROM [PersonDocument]
                WHERE PersonId = @Id AND FileContentId = @FileContentId";

            return await Execute(query, new { @Id = personId, @FileContentId = fileId }) > 0;
        }
        public async Task<bool> DeleteAllPersonDocument(string personId)
        {
            var query = @"
                DELETE FROM [PersonDocument]
                WHERE PersonId = @Id";

            return await Execute(query, new { @Id = personId }) > 0;
        }

        public async Task<IEnumerable<ActivityType>> ReadAllDocumentActivities(string personId)
        {
            var query = @"
                SELECT pa.ActivityType
                FROM [PersonActivity] AS pa
                JOIN [PersonBiography] AS pb
	                ON pb.Id = pa.BiographyId
                WHERE
	                pb.PersonId = @PersonId";

            return await Query<ActivityType>(query, new { @PersonId = personId });
        }

        #endregion

        #region PersonName

        public async Task<string> ReadCurrentPersonName(string personId)
        {
            var query = @"
                SELECT Firstname + ' ' + Lastname + ' ' + Patronym AS 'Name'
                FROM (SELECT * FROM [dbo].[ReadPerson](@PersonId, default, default, default))";

            return await QueryFoD<string>(query, new { @PersonId = personId });
        }
        public async Task<PersonName> ReadLastPersonName(string id)
        {
            var query = @"
                SELECT TOP 1 *
                FROM [PersonName]
                WHERE PersonId = @Id AND IsActive = 1
                ORDER BY
	                DateCreated DESC";

            return await QueryFoD<PersonName>(query, new { @Id = id });
        }
        public async Task<IEnumerable<PersonName>> ReadAllPersonNameByPersonId(string id)
        {
            var query = @"
                SELECT *
                FROM [PersonName]
                WHERE PersonId = @Id
                ORDER BY
	                DateCreated DESC";

            return await Query<PersonName>(query, new { @Id = id });
        }
        public async Task<string> CreatePersonName(PersonName entity)
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

            return await QueryFoD<string>(query, new DynamicParameters(entity));
        }
        public async Task<bool> UpdatePersonName(PersonName entity)
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

            return await Execute(query, new DynamicParameters(entity)) > 0;
        }
        public async Task<bool> DeletePersonName(string id)
        {
            var query = @"
                DELETE FROM [PersonName]
                WHERE Id = @Id";

            return await Execute(query, new { @Id = id }) > 0;
        }

        #endregion

        #region PersonRelation

        public async Task<PersonRelation> ReadPersonRelation(string inviter, string invited, RelationType type)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId
                    AND InvitedId = @InvitedId
                    AND RelationType = @RelationType
                    AND IsActive = 1";

            var parameters = new DynamicParameters();
            parameters.Add("@InviterId", inviter);
            parameters.Add("@InvitedId", invited);
            parameters.Add("@RelationType", type);
            return await QueryFoD<PersonRelation>(query, parameters);
        }
        public async Task<IEnumerable<PersonRelation>> ReadAllPersonRelationBetweenInviterAndInvited(string inviter, string invited)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId
                    AND InvitedId = @InvitedId
                    AND IsActive = 1";

            var parameters = new DynamicParameters();
            parameters.Add("@InviterId", inviter);
            parameters.Add("@InvitedId", invited);
            return await Query<PersonRelation>(query, parameters);
        }
        public async Task<IEnumerable<PersonRelation>> ReadAllPersonRelationsByInviterId(string id)
        {
            string query = @"
                    SELECT *
                    FROM [PersonRelation]
                    WHERE 
                        InviterId = @PersonId
                        AND IsActive = 1";

            return await Query<PersonRelation>(query, new { @PersonId = id });
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsThatHaveRelativesWithPossibleRelations()
        {
            return await Query<KeyValuePair<string, string>>("SELECT * FROM [dbo].[GetPersonsThatHaveRelativesWithPossibleRelations]()");
        }

        public async Task<IEnumerable<PersonRelation>> ReadAllPersonRelationsThatArePossible(string personId, string inviterId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PersonId", personId);
            parameters.Add("@InviterId", inviterId);
            return await Query<PersonRelation>("SELECT * FROM [dbo].[ReadAllPersonRelationsThatArePossible](@PersonId, @InviterId)", parameters);
        }

        public async Task<bool> CheckIfSameRelationsAvaible(string personId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PersonId", personId);
            return await QueryFoD<bool>("SELECT [dbo].[CheckIfSameRelationsAvaible](@PersonId)", parameters);
        }

        public async Task<IEnumerable<KeyValuePair<string, string>>> GetPersonsKvpWithPossibleRelations(string personId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@PersonId", personId);
            return await Query<KeyValuePair<string, string>>("SELECT * FROM [dbo].[GetPersonsKvpWithPossibleRelations](@PersonId)", parameters);
        }

        public async Task<string> CreatePersonRelation(PersonRelation entity)
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

            return await QueryFoD<string>(query, new DynamicParameters(entity));
        }
        public async Task<bool> UpdatePersonRelation(PersonRelation entity)
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

            return await Execute(query, new DynamicParameters(entity)) > 0;
        }
        public async Task<bool> DeletePersonRelation(string inviter, string invited)
        {
            string query = @"
                    DELETE FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId
                    AND InvitedId = @InvitedId";

            return await Execute(query,
                new { @InviterId = inviter, @InvitedId = invited })
                > 0;
        }
        public async Task<bool> DeletePersonRelation(string inviter, string invited, RelationType type, RelationType ctrType)
        {
            string query = @"
                    DELETE FROM [PersonRelation]
                    WHERE 
                        InviterId = @InviterId 
                    AND InvitedId = @InvitedId
                    AND RelationType = @RelationType

                    DELETE FROM [PersonRelation]
                    WHERE 
                        InviterId = @InvitedId 
                    AND InvitedId = @InviterId
                    AND RelationType = @CounterType";

            return await Execute(query,
                new { @InviterId = inviter, @InvitedId = invited, @RelationType = type, @CounterType = ctrType })
                > 0;
        }
        public async Task<bool> IsMarried(string personId)
        {
            string query = @"
				   SELECT COUNT(Id)
                   FROM [PersonRelation]
                   WHERE 
                       InviterId = @PersonId
                   AND IsActive = 1
                   AND RelationType = @RelationType";

            return await QueryFoD<int>(query, new { @PersonId = personId, @RelationType = RelationType.HusbandWife })
                > 0;
        }

        public async Task<bool> IsInRelationship(string personId)
        {
            string query = @"
				   SELECT COUNT(Id)
                   FROM [PersonRelation]
                   WHERE 
                       InviterId = @PersonId
                   AND IsActive = 1
                   AND RelationType = @RelationType";

            return await QueryFoD<int>(query, new { @PersonId = personId, @RelationType = RelationType.LivePartner })
                > 0;
        }

        public async Task<IEnumerable<RelationType>> GetPersonsRelationTypes(string personId)
        {
            string query = @"
                    SELECT RelationType
                    FROM [PersonRelation]
                    WHERE 
	                    InviterId = @PersonId
                        AND NOT RelationType = @Spouse
                        AND NOT RelationType = @LivePartner
	                    AND IsActive = 1
                    GROUP BY
                        RelationType";

            return await Query<RelationType>(query, 
                new { 
                    @PersonId = personId,
                    @Spouse = RelationType.HusbandWife,
                    RelationType.LivePartner
                });
        }

        #endregion

        #region PersonBiography

        public async Task<PersonBiography> ReadPersonBiographyByPersonId(string personId)
        {
            string query = @"
                    SELECT *
                    FROM [PersonBiography]
                    WHERE 
                        PersonId = @PersonId
                        AND IsActive = 1";

            return await QueryFoD<PersonBiography>(query, new { @PersonId = personId});
        }

        public async Task<PersonBiography> ReadPersonBiography(string biographyId)
        {
            string query = @"
                    SELECT *
                    FROM [PersonBiography]
                    WHERE 
                        Id = @Id
                        AND IsActive = 1";

            return await QueryFoD<PersonBiography>(query, new { @Id = biographyId });
        }

        public async Task<string> CreatePersonBiography(PersonBiography entity)
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

            return await QueryFoD<string>(query, new DynamicParameters(entity));
        }

        public async Task<bool> UpdatePersonBiography(PersonBiography entity)
        {
            string query = @"
                    UPDATE [PersonBiography]
                       SET [PersonId] = @PersonId
                          ,[BiographyText] = @BiographyText
                          ,[DateCreated] = @DateCreated
                          ,[DateModified] = @DateModified
                          ,[IsActive] = @IsActive
                     WHERE Id = @Id";

            return await Execute(query, new DynamicParameters(entity)) > 0;
        }

        #endregion

        #region PersonActivity

        public async Task<PersonActivity> ReadPersonActivity(string id)
        {
            var query = @"
                SELECT *
                FROM [PersonActivity]
                WHERE [Id] = @Id
                AND IsActive = 1";

            return await QueryFoD<PersonActivity>(query, new { @Id = id });
        }

        public async Task<IEnumerable<PersonActivity>> ReadAllPersonActivityByPerson(string id)
        {
            var query = @"
                SELECT pa.*
                FROM [PersonActivity] AS pa
                JOIN [PersonBiography] AS pb
	                ON pa.BiographyId = pb.Id 
	                AND pb.PersonId = @Id
                    AND pa.IsActive = 1
                    AND pb.IsActive = 1";

            return await Query<PersonActivity>(query, new { @Id = id });
        }

        public async Task<IEnumerable<PersonActivity>> ReadAllPersonActivityByPerson(string id, ActivityType type)
        {
            var query = @"
                SELECT pa.*
                FROM [PersonActivity] AS pa
                JOIN [PersonBiography] AS pb
	                ON pa.BiographyId = pb.Id 
	                AND pb.PersonId = @Id
	                AND pa.ActivityType = @Type
                    AND pb.IsActive = 1
                    AND pa.IsActive = 1";

            return await Query<PersonActivity>(query, new { @Id = id, @Type = type });
        }

        public async Task<IEnumerable<PersonActivity>> ReadCategorizedPersonActivityByPerson(string id)
        {
            var query = @"
                SELECT 
	                pa.*
                FROM 
	                [PersonActivity] AS pa
                JOIN [PersonBiography] AS pb
	                ON pa.BiographyId = pb.Id 
	                AND pb.PersonId = @Id
                    AND pa.IsActive = 1
                    AND pb.IsActive = 1";

            return await Query<PersonActivity>(query, new { @Id = id });
        }

        public async Task<string> CreatePersonActivity(PersonActivity entity)
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

            return await QueryFoD<string>(query, new DynamicParameters(entity));
        }

        public async Task<bool> UpdatePersonActivity(PersonActivity entity)
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

            return await Execute(query, new DynamicParameters(entity)) > 0;
        }

        public async Task<bool> DeletePersonActivity(string id)
        {
            var query = @"
                DELETE FROM [PersonActivity]
                WHERE Id = @Id";

            return await Execute(query, new { @Id = id }) > 0;
        }

        public async Task<bool> DeleteAllPersonActivityByPersonId(string id)
        {
            var query = @"
                DELETE FROM [PersonActivity]
                WHERE BiographyId = (SELECT Id FROM [PersonBiography] WHERE PersonId = @Id)";

            return await Execute(query, new { @Id = id }) > 0;
        }

        #endregion

        #region FileContent

        public async Task<FileContent> ReadFileContent(string id)
        {
            var query = @"
                SELECT *
                FROM [FileContent]
                WHERE 
                    Id = @Id
                    AND IsActive = 1";

            return await QueryFoD<FileContent>(query, new { @Id = id });
        }

        public async Task<string> CreateFileContent(FileContent entity)
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

            return await QueryFoD<string>(query, new DynamicParameters(entity));
        }

        public async Task<bool> UpdateFileContent(FileContent entity)
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

            return await Execute(query, new DynamicParameters(entity)) > 0;
        }

        public async Task<bool> DeleteFileContent(string id)
        {
            var query = @"
                DELETE FROM [FileContent]
                WHERE Id = @Id";

            return await Execute(query, new { @Id = id }) > 0;
        }

        #endregion

    }
}