using System;
using System.Linq;
using Shared.Interfaces.Repositories;

namespace DataAccessInfrastructure.Repositories
{
    public class UserRepository : SqlBaseRepository, IUserRepository
    {
        public UserRepository()
        {
        }

        #region Roles

        public bool AddUsersToRoles(string[] usernames, string[] roleNames)
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

            return Execute(query, new { @Usernames = usernames, @RoleNames = roleNames }) > 0;
        }

        public string CreateRole(string roleName)
        {
            throw new NotImplementedException();

            //var query = @"";

            //return Query<Person>(query, new { @RoleName = roleName });
        }

        public bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();

            //var query = @"";

            //return Query<Person>(query, new { @RoleName = roleName, @ThrowOnPopulatedRole = throwOnPopulatedRole });
        }

        public string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();

            //var query = @"";

            //return Query<Person>(query, new { @RoleName = roleName, @UsernameToMatch = usernameToMatch });
        }

        public string[] GetAllRoles()
        {
            var query = @"
                SELECT Name
                FROM [AspNetRoles]";

            return Query<string>(query).ToArray();
        }

        public string[] GetRolesForUser(string username)
        {
            var query = @"
                SELECT r.Name
                FROM [AspNetUserRoles] AS ur
                JOIN [AspNetUsers] AS u ON u.Id = ur.UserId AND u.UserName = @Username
                JOIN [AspNetRoles] AS r ON r.Id = ur.RoleId";

            return Query<string>(query, new { @Username = username }).ToArray();
        }

        public string[] GetUsersInRole(string roleName)
        {
            var query = @"
                SELECT u.UserName
                FROM [AspNetUserRoles] AS ur
                JOIN [AspNetUsers] AS u ON u.Id = ur.UserId
                JOIN [AspNetRoles] AS r ON r.Id = ur.RoleId AND r.Name = @RoleName";

            return Query<string>(query, new { @RoleName = roleName }).ToArray();
        }

        public bool IsUserInRole(string username, string roleName)
        {
            var query = @"
                SELECT COUNT(ur.UserId)
                FROM [AspNetUserRoles] AS ur
                JOIN [AspNetUsers] AS u ON u.Id = ur.UserId AND u.UserName = @Username
                JOIN [AspNetRoles] AS r ON r.Id = ur.RoleId AND r.Name = @RoleName";

            return QueryFoD<int>(query, new { @Username = username, @RoleName = roleName }) > 0;
        }

        public bool RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();

            //var query = @"";

            //return Query<Person>(query, new { @Usernames = usernames, @RoleNames = roleNames });
        }

        public bool RoleExists(string roleName)
        {
            var query = @"
                SELECT COUNT(Id)
                FROM [AspNetRoles]
                WHERE Name = @RoleName";

            return QueryFoD<int>(query, new { @RoleName = roleName }) > 0;
        }

        #endregion

    }
}