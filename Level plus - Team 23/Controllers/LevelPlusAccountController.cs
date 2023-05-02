using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using Level_plus___Team_23.Models;
using Level_plus___Team_23.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Level_plus___Team_23.Controllers
{
    public class LevelPlusAccountController : Controller
    {
        private DbSet<ApplicationUser> entities;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        public LevelPlusAccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            entities = context.Set<ApplicationUser>();
        }


        public IActionResult Register()
        {
            ApplicationUser model = new ApplicationUser();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(ApplicationUser request)
        {
            if (ModelState.IsValid)
            {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null)
                {
                    var user = new ApplicationUser
                    {
                        Name = request.Name,
                        Surname = request.Surname,
                        UserName = request.Email,
                        Role = "User",
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        PhoneNumber = request.PhoneNumber,
                        //Picture = "urlTODO" //request.Picture
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        if (result.Errors.Count() > 0)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else
                {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }

            return View(request);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            ApplicationUser model = new ApplicationUser();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false)
                {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

                if (result.Succeeded)
                {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult GiveRole()
        {
            List<ApplicationUser> users = context.users.ToList();
            return View(users);
        }
        public IActionResult GiveRoleTo(Guid id)
        {
            string normalizedId = id.ToString();
            ApplicationUser user = entities.SingleOrDefault(x => x.Id.Equals(normalizedId));
            user.Role = "Admin";
            context.SaveChanges();
            return RedirectToAction("GiveRole", "LevelPlusAccount");
        }
    }
}
