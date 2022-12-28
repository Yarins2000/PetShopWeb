using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PetShopWeb.Repositories.AccountRepository
{
    public interface IAccountRepository
    {
        IQueryable<IdentityRole> GetRoles();
        IQueryable<IdentityUser> GetUsers();
    }
}
