using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Shared.Enums;
using Shared.Interfaces.Repositories;
using Shared.Models;

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
            throw new NotImplementedException();

            //var query = @"";

            //return Query<Person>(query, new { @Usernames = usernames, @RoleNames = roleNames });
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

            return QueryFoD<int>(query, new { @Username = username, @RoleName = roleName }) > 0 ? true : false;
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

            return QueryFoD<int>(query, new { @RoleName = roleName }) > 0 ? true : false;
        }

        #endregion

    }
}