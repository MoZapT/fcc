using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces.Repositories
{
    public interface IUserRepository : ISqlBaseRepository
    {

        #region Roles

        bool AddUsersToRoles(string[] usernames, string[] roleNames);
        string CreateRole(string roleName);
        bool DeleteRole(string roleName, bool throwOnPopulatedRole);
        string[] FindUsersInRole(string roleName, string usernameToMatch);
        string[] GetAllRoles();
        string[] GetRolesForUser(string username);
        string[] GetUsersInRole(string roleName);
        bool IsUserInRole(string username, string roleName);
        bool RemoveUsersFromRoles(string[] usernames, string[] roleNames);
        bool RoleExists(string roleName);

        #endregion

    }
}
