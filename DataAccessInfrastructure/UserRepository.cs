using System;
using System.Linq;
using System.Threading.Tasks;
using Shared.Interfaces.Repositories;

namespace WebAppFcc.Repository
{
    public class UserRepository : SqlBaseRepository, IUserRepository
    {
        public UserRepository()
        {
        }

        #region Roles

        public async Task<bool> AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            string query = @"
                DECLARE @Users table (Id nvarchar(128))
                DECLARE @Roles table (Id nvarchar(128))
                DECLARE @UserId nvarchar(128), @RoleId nvarchar(128) 

                INSERT INTO @Users
                SELECT Id
                FROM [AspNetUsers]
                WHERE 
	                [UserName] IN @Usernames

                INSERT INTO @Roles
                SELECT Id
                FROM [AspNetRoles]
                WHERE 
	                [Name] IN @RoleNames

                BEGIN TRAN

                DECLARE users_cursor CURSOR FOR 
                SELECT Id 
                FROM @Users

                OPEN users_cursor  
                FETCH NEXT FROM users_cursor INTO @UserId  

                WHILE @@FETCH_STATUS = 0  
                BEGIN 
 
	                DECLARE roles_cursor CURSOR FOR 
	                SELECT Id 
	                FROM @Roles

	                OPEN roles_cursor  
	                FETCH NEXT FROM roles_cursor INTO @RoleId  

	                WHILE @@FETCH_STATUS = 0  
	                BEGIN  
		                INSERT INTO [AspNetUserRoles]
			                ([UserId]
			                ,[RoleId])
		                VALUES
			                (@UserId
			                ,@RoleId)

		                FETCH NEXT FROM roles_cursor INTO @RoleId 
	                END 

	                CLOSE roles_cursor  
	                DEALLOCATE roles_cursor 

	                FETCH NEXT FROM users_cursor INTO @UserId 
                END 

                CLOSE users_cursor  
                DEALLOCATE users_cursor

                COMMIT TRAN";

            return await (Execute(query, new { @Usernames = usernames, @RoleNames = roleNames })) > 0;
        }

        public Task<string> CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public async Task<string[]> GetAllRoles()
        {
            var query = @"
                SELECT Name
                FROM [AspNetRoles]";

            return (await Query<string>(query)).ToArray();
        }

        public async Task<string[]> GetRolesForUser(string username)
        {
            var query = @"
                SELECT r.Name
                FROM [AspNetUserRoles] AS ur
                JOIN [AspNetUsers] AS u ON u.Id = ur.UserId AND u.UserName = @Username
                JOIN [AspNetRoles] AS r ON r.Id = ur.RoleId";

            return (await Query<string>(query, new { @Username = username })).ToArray();
        }

        public async Task<string[]> GetUsersInRole(string roleName)
        {
            var query = @"
                SELECT u.UserName
                FROM [AspNetUserRoles] AS ur
                JOIN [AspNetUsers] AS u ON u.Id = ur.UserId
                JOIN [AspNetRoles] AS r ON r.Id = ur.RoleId AND r.Name = @RoleName";

            return (await Query<string>(query, new { @RoleName = roleName })).ToArray();
        }

        public async Task<bool> IsUserInRole(string username, string roleName)
        {
            var query = @"
                SELECT COUNT(ur.UserId)
                FROM [AspNetUserRoles] AS ur
                JOIN [AspNetUsers] AS u ON u.Id = ur.UserId AND u.UserName = @Username
                JOIN [AspNetRoles] AS r ON r.Id = ur.RoleId AND r.Name = @RoleName";

            return (await QueryFoD<int>(query, new { @Username = username, @RoleName = roleName })) > 0;
        }

        public Task<bool> RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RoleExists(string roleName)
        {
            var query = @"
                SELECT COUNT(Id)
                FROM [AspNetRoles]
                WHERE Name = @RoleName";

            return (await QueryFoD<int>(query, new { @RoleName = roleName })) > 0;
        }

        #endregion

    }
}