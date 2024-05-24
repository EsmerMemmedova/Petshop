using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PetShop.Areas.Admin.ViewModels;

namespace PetShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManger;
        private readonly SignInManager<AppUser> _signInManger;
        private readonly RoleManager<IdentityRole> _roleManger;

        public AccountController(UserManager<AppUser> userManger, SignInManager<AppUser> signInManger, RoleManager<IdentityRole> roleManger)
        {
            _userManger = userManger;
            _signInManger = signInManger;
            _roleManger = roleManger;
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    IdentityRole role1 = new IdentityRole("Admin");
        //    IdentityRole role2 = new IdentityRole("Member"); 
        //    await _roleManger.CreateAsync(role1);
        //    await _roleManger.CreateAsync(role2);
        //    return Ok("rollar yarandi!");
        //}
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser admin = new()
        //    {
        //        FullName = "Esmer Memmedova",
        //        UserName = "SuperAdmin",
        //        Email = "esmermemmedova127@gmail.com"
        //    };
        //    await _userManger.CreateAsync(admin, "Esmer20@");
        //    return Ok("adminler yarandi");
        //}
        //public async Task<IActionResult> AddToAdminRole()
        //{
        //    var admin = await _userManger.FindByNameAsync("SuperAdmin");
        //    await _userManger.AddToRoleAsync(admin, "Admin");
        //    return Ok("Rollar elave olundu");
        //}
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM adminLoginVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var admin = await _userManger.FindByNameAsync(adminLoginVM.UserName);
                if (admin == null)
                {


                ModelState.AddModelError("", "UserName or Password not Correct!");
                    return View();


                }
                var check=await _userManger.CheckPasswordAsync(admin,adminLoginVM.Password);
            if(!check) 
            {
                ModelState.AddModelError("", "UserName or Password not Correct!");
                return View();  
            }
            var result = _signInManger.PasswordSignInAsync(admin, adminLoginVM.Password, false, false);
            return RedirectToAction(nameof(Index),"Dashboard");
        }
        public async Task< IActionResult> SignOut()
        {
           await  _signInManger.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    } 
}
