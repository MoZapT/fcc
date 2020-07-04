using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FamilyControlCenter.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(ConfigurationManager.ConnectionStrings["FccConStr"].ConnectionString, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class FccRoleProvider : RoleProvider
    {
        private readonly ApplicationDbContext _context;

        public FccRoleProvider()
        {
            _context = new ApplicationDbContext();
        }

        public override string ApplicationName
        {
            get => "FCC";
            set => throw new System.NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            return _context.Users
                .Where(u => u.Roles.FirstOrDefault(r => r.RoleId == roleName) != null)
                .Select(u => u.UserName)
                .ToArray();
        }

        public override string[] GetAllRoles()
        {
            return _context.Roles.Select(r => r.Name).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);
            var userRoles = _context.Roles.Select(r => r.Name);

            if (user == null)
                return new string[] { };
            return user.Roles == null ? new string[] { } :
                userRoles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _context.Users.FirstOrDefault(u => u.Roles.FirstOrDefault(r => r.RoleId == roleName) != null) != null;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            return _context.Roles.FirstOrDefault(r => r.Name == roleName) != null;
        }
    }
}