using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using University.Data;
using University.ViewModels;

namespace University.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<Users> _signInManager;
        private UserManager<Users> _userManager;
        private List<UserDetail> user = new List<UserDetail>();
        private readonly AppDbContext _appDbContext;
        public AccountController(SignInManager<Users> signInManager, AppDbContext appDbContext, UserManager<Users> userManager)
        {
            _signInManager = signInManager;
            _appDbContext = appDbContext;
            _userManager = userManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("UserName,EmailAddress,UserPassWord,ConfirmPassword,UserRole")] UserDetail detail)
        {
            if (!ModelState.IsValid)
            {
                if (detail.UserPassWord != detail.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
                    return View(detail);
                }
            }
            Users user = new Users
            {
                UserName = detail.UserName,
                Email = detail.EmailAddress,
                UserRole = detail.UserRole
            };
            var result = await _userManager.CreateAsync(user, detail.UserPassWord);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, detail.UserRole);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View("Error");
            }
        }
        public IActionResult Login()
        {
            LoginViewModel _model = new LoginViewModel();
            return View(_model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var usr = await _userManager.FindByNameAsync(model.Username);
            var res = await _signInManager.PasswordSignInAsync(usr, model.Password, false, false);
            if (res.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "Username or Password is incorrect!");
            }
            return View(model);
        }
    }
}
        
            

        
                
        


            
    
    

    






