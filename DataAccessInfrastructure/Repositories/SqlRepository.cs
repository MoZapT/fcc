using System.Collections.Generic;
using Dapper;
using Shared.Interfaces.Repositories;

namespace DataAccessInfrastructure.Repositories
{
    public class SqlRepository : SqlBaseRepository, ISqlBaseRepository, ISqlRepository
    {
        public SqlRepository()
        {
        }

        #region Person

        //public IPerson Read<IPerson>(int id)
        //{
        //    var query = @"
        //        SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
        //        FROM [FCC].[dbo].[Person] AS p
        //        JOIN (SELECT TOP 1 * FROM [FCC].[dbo].[PersonName] 
        //        WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id
        //        WHERE p.[Id] = @Id";

        //    return QueryFoD<IPerson>(query, new { @Id = id });
        //}
        //public IEnumerable<IPerson> ReadAll<IPerson>()
        //{
        //    var query = @"
        //        SELECT p.*, pn.Name, pn.Lastname, pn.Patronym
        //        FROM [FCC].[dbo].[Person] AS p
        //        JOIN (SELECT TOP 1 * FROM [FCC].[dbo].[PersonName] 
        //        WHERE IsActive = 1 ORDER BY DateCreated DESC) AS pn ON PersonId = p.Id";

        //    return Query<IPerson>(query);
        //}
        //public int Create<IPerson>(IPerson entity)
        //{
        //    #region Q1
        //    var query = @"
        //        DECLARE @output table (Id int)

        //        BEGIN TRAN
        //        INSERT INTO [FCC].[dbo].[Person]
        //                   ([Sex]
        //                   ,[BornTimeKnown]
        //                   ,[IsDead]
        //                   ,[BornTime]
        //                   ,[DeadTime]
        //                   ,[IsActive]
        //                   ,[DateCreated]
        //                   ,[DateModified])
	       //          OUTPUT INSERTED.Id INTO @output
        //             VALUES
        //                   (@Sex
        //                   ,@BornTimeKnown
        //                   ,@IsDead
        //                   ,@BornTime
        //                   ,@DeadTime
        //                   ,@IsActive
        //                   ,@DateCreated
        //                   ,@DateModified)

        //        INSERT INTO [FCC].[dbo].[PersonName]
        //                   ([PersonId]
        //                   ,[Name]
        //                   ,[Lastname]
        //                   ,[Patronym]
        //                   ,[IsActive]
        //                   ,[DateCreated]
        //                   ,[DateModified])
        //             VALUES
        //                   ((SELECT Id FROM @output)
        //                   ,@Name
        //                   ,@Lastname
        //                   ,@Patronym
        //                   ,1
        //                   ,GETDATE()
        //                   ,GETDATE())

        //        COMMIT TRAN";
        //    #endregion

        //    return QueryFoD<int>(query, new DynamicParameters(entity));
        //}
        //public bool Update<IPerson>(IPerson entity)
        //{
        //    #region Q1
        //    var query = @"
        //        BEGIN TRAN
        //        UPDATE [FCC].[dbo].[Person]
        //            SET [Sex] = @Sex
        //                ,[BornTimeKnown] = @BornTimeKnown
        //                ,[IsDead] = @IsDead
        //                ,[BornTime] = @BornTime
        //                ,[DeadTime] = @DeadTime
        //                ,[IsActive] = @IsActive
        //                ,[DateCreated] = @DateCreated
        //                ,[DateModified] = @DateModified
        //            WHERE Id = @Id

        //        IF @NameModified = 1
        //        BEGIN
        //        INSERT INTO [FCC].[dbo].[PersonName]
        //                   ([PersonId]
        //                   ,[Name]
        //                   ,[Lastname]
        //                   ,[Patronym]
        //                   ,[IsActive]
        //                   ,[DateCreated]
        //                   ,[DateModified])
        //             VALUES
        //                   (@Id
        //                   ,@Name
        //                   ,@Lastname
        //                   ,@Patronym
        //                   ,1
        //                   ,GETDATE()
        //                   ,GETDATE())
        //        END

        //        COMMIT TRAN";
        //    #endregion

        //    return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        //}
        //public bool Delete<IPerson>(IPerson entity)
        //{
        //    var query = @"
        //        DECLARE @groups table (Id int)

        //        BEGIN TRAN
        //        DELETE FROM [FCC].[dbo].[Person]
        //        WHERE Id = @Id

        //        DELETE FROM [FCC].[dbo].[PersonName]
        //        WHERE PersonId = @Id

        //        DELETE FROM [FCC].[dbo].[PersonRelation]
        //        WHERE PersonId = @Id

        //        INSERT INTO @groups(Id)
        //        SELECT prg.Id
        //        FROM [FCC].[dbo].[PersonRelationGroup] AS prg
        //        JOIN [FCC].[dbo].[PersonRelation] AS pr ON prg.Id = pr.Id
        //        WHERE pr.PersonId = @Id

        //        DELETE FROM [FCC].[dbo].[PersonRelation]
        //        WHERE PersonRelationGroupId IN (SELECT Id FROM @groups)

        //        DELETE FROM [FCC].[dbo].[PersonRelationGroup]
        //        WHERE Id IN ((SELECT Id FROM @groups))

        //        COMMIT TRAN";

        //    return Execute(query, new DynamicParameters(entity)) > 0 ? true : false;
        //}

        #endregion

        #region PersonName

        //public IPersonName Read<IPersonName>(int id)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public IEnumerable<IPersonName> ReadAll<IPersonName>()
        //{
        //    throw new System.NotImplementedException();
        //}
        //public int Create<IPersonName>(IPersonName entity)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public bool Update<IPersonName>(IPersonName entity)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public bool Delete<IPersonName>(IPersonName entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        #endregion

        #region PersonRelationGroup

        //public IPersonRelationGroup Read<IPersonRelationGroup>(int id)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public IEnumerable<IPersonRelationGroup> ReadAll<IPersonRelationGroup>()
        //{
        //    throw new System.NotImplementedException();
        //}
        //public int Create<IPersonRelationGroup>(IPersonRelationGroup entity)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public bool Update<IPersonRelationGroup>(IPersonRelationGroup entity)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public bool Delete<IPersonRelationGroup>(IPersonRelationGroup entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        #endregion

        #region PersonRelation

        //public IPersonRelation Read<IPersonRelation>(int id)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public IEnumerable<IPersonRelation> ReadAll<IPersonRelation>()
        //{
        //    throw new System.NotImplementedException();
        //}
        //public int Create<IPersonRelation>(IPersonRelation entity)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public bool Update<IPersonRelation>(IPersonRelation entity)
        //{
        //    throw new System.NotImplementedException();
        //}
        //public bool Delete<IPersonRelation>(IPersonRelation entity)
        //{
        //    throw new System.NotImplementedException();
        //}

        #endregion

    }
}