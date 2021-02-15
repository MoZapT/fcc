using System.Threading.Tasks;

namespace WebAppFcc.Shared.Interfaces.Repositories
{
    public interface IUserRepository : ISqlBaseRepository
    {

        #region Roles

        Task<bool> AddUsersToRoles(string[] usernames, string[] roleNames);
        Task<string> CreateRole(string roleName);
        Task<bool> DeleteRole(string roleName, bool throwOnPopulatedRole);
        Task<string[]> FindUsersInRole(string roleName, string usernameToMatch);
        Task<string[]> GetAllRoles();
        Task<string[]> GetRolesForUser(string username);
        Task<string[]> GetUsersInRole(string roleName);
        Task<bool> IsUserInRole(string username, string roleName);
        Task<bool> RemoveUsersFromRoles(string[] usernames, string[] roleNames);
        Task<bool> RoleExists(string roleName);

        #endregion

    }
}
