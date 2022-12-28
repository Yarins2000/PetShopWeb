using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PetShopWeb.Repositories.AccountRepository;
using PetShopWeb.ViewModels;
using System.Data;

namespace PetShopWeb.Controllers
{
    [Authorize(Roles = "admin")]
    public class AccountController : Controller
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly SignInManager<IdentityUser> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;

        readonly IAccountRepository _accountRepository;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IAccountRepository accountRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        #region User
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginmodel, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginmodel.Email, loginmodel.Password, loginmodel.RememberMe, false);
                if (result.Succeeded)
                {
                    //if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    //    return Redirect(returnUrl); 
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    else
                        return RedirectToAction("HomePage", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email or password are incorrect");
            }
            return View(loginmodel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("HomePage", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registermodel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registermodel.Email, Email = registermodel.Email };
                var result = await _userManager.CreateAsync(user, registermodel.Password);

                if (result.Succeeded)
                {
                    if (_signInManager.IsSignedIn(User) && User.IsInRole("admin"))
                        return RedirectToAction("UsersManager");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("HomePage", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registermodel);
        }

        [AcceptVerbs(new[] { "Get", "Post" })]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return Json(true);
            else
                return Json($"The provided email - {email} is already in use");
        }

        public IActionResult UsersManager()
        {
            return View(_accountRepository.GetUsers());
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                ViewBag.ErrorMessage = $"User with Id - {id} hasn't been found";
                return NotFound();
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            var model = new EditUserViewModel
            {
                Id = id,
                Email = user.Email,
                UserName = user.UserName,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user is null)
            {
                ViewBag.ErrorMessage = $"User with Id - {model.Id} hasn't been found";
                return NotFound();
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return RedirectToAction("UsersManager");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                ViewBag.ErrorMessage = $"User with Id - {id} hasn't been found";
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return RedirectToAction("UsersManager");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("UsersManager");
        }
        #endregion

        #region Role
        [Authorize]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel rolemodel)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole() { Name = rolemodel.RoleName };
                var identityResult = await _roleManager.CreateAsync(identityRole);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("RolesManager");
                }

                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(rolemodel);
        }

        [Authorize]
        public IActionResult RolesManager()
        {
            return View(_accountRepository.GetRoles());
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return View(nameof(RolesManager));
            }

            var editModel = new EditRoleViewModel
            {
                Id = id,
                RoleName = role.Name
            };
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    editModel.Users!.Add(user.UserName);
            }
            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editModel)
        {
            var role = await _roleManager.FindByIdAsync(editModel.Id);
            if (role is null)
            {
                return View(nameof(RolesManager));
            }
            else
            {
                role.Name = editModel.RoleName;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("RolesManager");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(editModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with Id - {id} hasn't been found";
                return NotFound();
            }

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return RedirectToAction("RolesManager");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("RolesManager");
        }

        #endregion

        #region User-Role
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with Id - {roleId} hasn't been found";
                return NotFound();
            }

            var model = new List<UserRoleViewModel>();
            foreach (var user in _userManager.Users.ToList())
            {
                var userRoleViewModel = new UserRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                userRoleViewModel.IsSelected = await _userManager.IsInRoleAsync(user, role.Name);
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
            {
                ViewBag.ErrorMessage = $"Role with id - {roleId} hasn't been found";
                return NotFound();
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult? result;
                if (model[i].IsSelected && !await _userManager.IsInRoleAsync(user, role.Name))

                    result = await _userManager.AddToRoleAsync(user, role.Name);
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                else
                    continue;

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1)) continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            ViewBag.userId = userId;

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                ViewBag.ErrorMessage = $"User with id - {userId} hasn't been found";
                return NotFound();
            }

            var model = new List<UserRolesViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var userRolesModel = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                userRolesModel.IsSelected = await _userManager.IsInRoleAsync(user, role.Name);
                model.Add(userRolesModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                ViewBag.ErrorMessage = $"User with id - {userId} hasn't been found";
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }

            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(u => u.RoleName));

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }

            return RedirectToAction("EditUser", new { Id = userId });
        }

        #endregion
    }

}
