using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Level_plus___Team_23.Data;
using Level_plus___Team_23.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Diagnostics;
using Level_plus___Team_23.Interfaces;
using Microsoft.Extensions.Logging;
using Level_plus___Team_23.DTO;
using Level_plus___Team_23.Relations;

namespace Level_plus___Team_23.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICourseService _courseService;
        private readonly ILogger<CoursesController> _logger;
        private readonly DbSet<Order> entitiesOrders;
        private readonly DbSet<CourseInShoppingCart> entitiesCourseInShoppingCarts;


        public CoursesController(ApplicationDbContext context, ICourseService courseService, ILogger<CoursesController> logger)
        {
            _context = context;
            _courseService = courseService;
            _logger = logger;
            entitiesOrders = context.Set<Order>();
            entitiesCourseInShoppingCarts = context.Set<CourseInShoppingCart>();

        }

        // GET: Courses

        public IActionResult Index()
        {

            string user = GetUserID();
            List<Course> courses = _context.courses.Select(x => new Course() { Id = x.Id, Description = x.Description
             , Title = x.Title, Price = x.Price, VideoUrl = x.VideoUrl, Student = x.Student }).ToList();
            List<Course> purchasedCourses = entitiesOrders
                .Include("CourseInOrders.Course")
                .Where(u => u.UserId == user)
                .ToList()
                .Select(x => x.CourseInOrders.Select(x => x.Course).ToList())
                .SelectMany(x => x)
                .ToList();

            List<Course> coursesInShoppingCart = entitiesCourseInShoppingCarts
                .Where(u => u.UserCart.OwnerId == user)
                .Select(u => u.CurrentCourse)
                .ToList();

            purchasedCourses.AddRange(coursesInShoppingCart);
                
                
            List<Course> watchLaters = _context.WatchLaterCourses.Where(c => c.Student.Id == user).Select(x => x.Course).ToList();
            List<Course> likedCourses = _context.FavouriteLists.Where(c => c.Student.Id == user).Select(x => x.Course).ToList();


            return View(new AllCourses
            {
                Courses = courses,
                PurchasedCourses = purchasedCourses,
                WatchLaters = watchLaters,
                LikedCourses = likedCourses
            });
        }

        // GET: Courses/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return this.authenticate();
        }
        public IActionResult authenticate(Course course=null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Error", "Home");

            }
            ApplicationUser user = _context.users.Find(userId);
            if (user.Role == "Admin")
            {
                if (course == null)
                    return View();
                else return View (course);
            }
            return RedirectToAction("Error", "Home");
        }
        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseID,Title,Description,VideoUrl,Price")] Course course)
        {
            if (ModelState.IsValid)
            {
                course.Student = _context.users.Single(x => x.Email == User.Identity.Name);
                _context.Add(course);
               
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return this.authenticate(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CourseID,Title,Description,VideoUrl,Price")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.courses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return this.authenticate(course);
        }

        // POST: Courses/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var course = await _context.courses.FindAsync(id);
            _context.courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return _context.courses.Any(e => e.Id == id);
        }

        public IActionResult AddCourseToCart(Guid id)
        {
            var result = this._courseService.GetShoppingCartInfo(id);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCourseToCart(AddToShoppingCartDTO model)
        {

            _logger.LogInformation("User Request -> Add Product in ShoppingCart and save changes in database!");


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = this._courseService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Courses");
            }
            return View(model);
        }


        [Authorize]
        public ActionResult Bought()
        {
            var user = GetUserID();

            List<Course> Courses = entitiesOrders
                .Include("CourseInOrders.Course")
                .Where(u => u.UserId == user)
                .ToList()
                .Select(x => x.CourseInOrders.Select(x => x.Course).ToList())
                .SelectMany(x => x)
                .ToList();

            return View(Courses);

        }

        [Authorize]
        public ActionResult LikedCourses()
        {
            var user = GetUserID();

            List<Course> Courses = _context.FavouriteLists.Where(x => x.Student.Id == user)
                .Select(el => el.Course)
                .ToList();

            return View(Courses);

        }

        [Authorize]
        public ActionResult WatchLaterCourses()
        {
            var user = GetUserID();

            List<Course> Courses = _context.WatchLaterCourses.Where(x => x.Student.Id == user)
                .Select(el => el.Course)
                .ToList();

            return View(Courses);

        }
        [HttpGet]
        [Authorize]
        public JsonResult Buy(Guid id)
        {
            string userLoggedId = GetUserID();
            Course course = _context.courses.Single(c => c.Id == id);


            List<PurchasedCourse> purchased = _context.PurchasedCourses.Where(c => c.Course.Id == course.Id && userLoggedId == c.Student.Id).ToList();
            if (purchased.Count == 0)
            {
                PurchasedCourse newPurchasedCourse = new
                    PurchasedCourse()
                {
                    Course = course,
                    Student = _context.users.Single(u => u.Id == userLoggedId)

                };
                _context.PurchasedCourses.Add(newPurchasedCourse);
                _context.SaveChanges();
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }



        }

        [HttpGet]
        [Authorize]
        public JsonResult WatchLater(Guid id)
        {
            string userLoggedId = GetUserID();
            Course course = _context.courses.Single(c => c.Id == id);
            List<WatchLater> watchLaters = _context.WatchLaterCourses.Where(c => c.Course.Id == course.Id &&
                userLoggedId == c.Student.Id).ToList();
            if (watchLaters.Count == 0)
            {
                WatchLater watchLater = new
                    Models.WatchLater()
                {
                    Course = course,
                    Student = _context.users.Single(u => u.Id == userLoggedId)
                };
                _context.WatchLaterCourses.Add(watchLater);
                _context.SaveChanges();
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }



        }

        [HttpGet]
        [Authorize]
        public JsonResult PutIntoFavourite(Guid id)
        {
            string userLoggedId = GetUserID();
            Course course = _context.courses.Single(c => c.Id == id);
            List<FavouriteList> favouriteList = _context.FavouriteLists.Where(c => c.Course.Id == course.Id &&
                userLoggedId == c.Student.Id).ToList();
            if (favouriteList.Count == 0)
            {
                FavouriteList favourite = new
                    Models.FavouriteList()
                {
                    Course = course,
                    Student = _context.users.Single(u => u.Id == userLoggedId)
                };
                _context.FavouriteLists.Add(favourite);
                _context.SaveChanges();
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }



        }


        [HttpGet]
        [Authorize]
        public JsonResult RemoveFromFavourite(Guid id)
        {
            string userLoggedId = GetUserID();
            Course course = _context.courses.Single(c => c.Id == id);
            List<FavouriteList> favouriteList = _context.FavouriteLists.Where(c => c.Course.Id == course.Id &&
                userLoggedId == c.Student.Id).ToList();
            if (favouriteList.Count != 0)
            {

                _context.FavouriteLists.Remove(favouriteList[0]);
                _context.SaveChanges();
                return Json(new { Success = true });
            }
            else
            {
                return Json(new { Success = false });
            }



        }

        [HttpGet]

        public JsonResult GiveExampleCourses()
        {
            string user = GetUserID();


            List<CourseUserPopular> favouriteLists = _context.FavouriteLists
                .Select(c => new CourseUserPopular() { Course = c.Course, User = c.Student })
                .ToList();

            List<CoursePopularity> coursePopularityList = new List<CoursePopularity>();

            HashSet<Guid> coursesCounter = new HashSet<Guid>();

            favouriteLists.ForEach(course =>
            {
                int time = 0;
                favouriteLists.ForEach(c1 =>
                {
                    if (course.Course.Id == c1.Course.Id && !coursesCounter.Contains(c1.Course.Id))
                    {
                        time++;

                    }
                });
                coursesCounter.Add(course.Course.Id);
                if (coursePopularityList.Where(c => c.CourseID == course.Course.Id).Count() == 0)
                    coursePopularityList.Add(new CoursePopularity() { Count = time, Title = course.Course.Title, CourseID = course.Course.Id });

            });

            coursePopularityList.Sort();





            /*
            var courses = _context
                .FavouriteLists
                .FromSqlInterpolated($"SELECT f.CourseID,f.ID, c.Description, c.VideoUrl, c.Price, c.Title as Title, count(f.StudentId) as Count from dbo.FavouriteLists as f join dbo.Courses as c on c.CourseID = f.CourseID  where f.StudentId != {user} group by f.CourseID,, c.Title order by count(f.StudentId) desc;")
                .ToList();
            */



            return Json(new { courses = coursePopularityList });
        }
        [HttpGet]

        public JsonResult Popular()
        {
            string user = GetUserID();


            List<CourseUserPopular> favouriteLists = entitiesOrders
                .Include("CourseInOrders.Course")
                .ToList()
                .Select(x => x.CourseInOrders.Select(x => new CourseUserPopular () { Course = x.Course, User = x.Order.User}).ToList())
                .SelectMany(x => x)
                .ToList();

            List<CoursePopularity> coursePopularityList = new List<CoursePopularity>();

            HashSet<Guid> coursesCounter = new HashSet<Guid>();

            favouriteLists.ForEach(course =>
            {
                int time = 0;
                favouriteLists.ForEach(c1 =>
                {
                    if (course.Course.Id == c1.Course.Id && !coursesCounter.Contains(c1.Course.Id))
                    {
                        time++;

                    }
                });
                coursesCounter.Add(course.Course.Id);
                if (coursePopularityList.Where(c => c.CourseID == course.Course.Id).Count() == 0)
                    coursePopularityList.Add(new CoursePopularity() { Count = time, Title = course.Course.Title, CourseID = course.Course.Id });

            });

            coursePopularityList.Sort();





            /*
            var courses = _context
                .FavouriteLists
                .FromSqlInterpolated($"SELECT f.CourseID,f.ID, c.Description, c.VideoUrl, c.Price, c.Title as Title, count(f.StudentId) as Count from dbo.FavouriteLists as f join dbo.Courses as c on c.CourseID = f.CourseID  where f.StudentId != {user} group by f.CourseID,, c.Title order by count(f.StudentId) desc;")
                .ToList();
            */



            return Json(new { courses = coursePopularityList });
        }




       

        private string GetUserID()
        {
            string email =  User.Identity.Name;

            return _context.users.Where(x => x.Email == email).Select(x => x.Id)
                .FirstOrDefault();
        }
    

}
}
