using Application.Dtos.Identity;
using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AboutMe.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(PageNames.Register.ToString());
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(PageNames.Login.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (userExists != null)
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.EmailIsAlreadyTaken);
                    return View(registerDTO);
                }

                var user = new User { UserName = registerDTO.Name, Email = registerDTO.Email };
                var result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded )
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Home.ToString());
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(registerDTO);
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string returnURL = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);

                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.InvalidLoginAttempt);
                    return View(loginDTO);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, loginDTO.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnURL) && Url.IsLocalUrl(returnURL))
                    {
                        return Redirect(returnURL);
                    }
                    else
                    {
                        return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Blog.ToString());
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ErrorMessages.InvalidLoginAttempt);
                    return View(loginDTO);
                }
            }

            return View(loginDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(PageNames.Index.ToString(), ControllerNames.Home.ToString());
        }
    }
}
