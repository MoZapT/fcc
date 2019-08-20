using DomainInfrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using DomainInfrastructure.Common;
using DomainInfrastructure.Interfaces.Common;
using DomainInfrastructure.Models.Family;
using DomainInfrastructure.Interfaces.Models;
using System.Threading.Tasks;
using DomainInfrastructure.Interfaces.Repositories;

namespace DomainInfrastructure.Repository
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository()
        {
        }

        #region Person

        public IPerson Read(int id)
        {
            var query = @"
                SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
                FROM [FCC].[dbo].[Person] AS p
                JOIN (SELECT TOP 1 * FROM [FCC].[dbo].[PersonName] 
                WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id
                WHERE p.[Id] = @Id";

            return QueryFoD<IPerson>(query, new { @Id = id });
        }
        public IEnumerable<IPerson> ReadAll()
        {
            var query = @"
                SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
                FROM [FCC].[dbo].[Person] AS p
                JOIN (SELECT TOP 1 * FROM [FCC].[dbo].[PersonName] 
                WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id";

            return Query<IPerson>(query);
        }
        public int Create(IPerson entity)
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

            return QueryFoD<int>(query, new DynamicParameters(entity));
        }
        public bool Update(IPerson entity)
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

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }
        public bool Delete(IPerson entity)
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

            return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        }

        #endregion

    }
}