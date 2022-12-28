using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PetShopWeb.Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        readonly RoleManager<IdentityRole> roleManager;
        readonly UserManager<IdentityUser> userManager;

        public AccountRepository(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            return roleManager.Roles;
        }
        public IQueryable<IdentityUser> GetUsers()
        {
            return userManager.Users;
        }
    }
}
