using Level_plus___Team_23.Data;
using Level_plus___Team_23.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Linq;

namespace Level_plus___Team_23.Controllers
{
    public class AdminController : Controller
    {
        private DbSet<ApplicationUser> entities;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;
        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            entities = context.Set<ApplicationUser>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DeleteStudentsAccount()
        {
            return View(entities.ToList());
        }
        public IActionResult DeleteStudentsAccountPost(Guid accountId)
        {
            var student = entities.ToList().Find(x => x.Id == accountId.ToString());
            entities.Remove(student);
            context.SaveChanges();
            return RedirectToAction ("DeleteStudentsAccount");
        }
    }
}
