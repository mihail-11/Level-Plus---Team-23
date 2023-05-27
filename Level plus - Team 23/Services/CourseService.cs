using Level_plus___Team_23.DTO;
using Level_plus___Team_23.Interfaces;
using Level_plus___Team_23.Models;
using Level_plus___Team_23.Relations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Level_plus___Team_23.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<CourseInShoppingCart> _courseInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(IRepository<Course> courseRepository, IRepository<CourseInShoppingCart> courseInShoppingCartRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _courseInShoppingCartRepository = courseInShoppingCartRepository;
        }


        public bool AddToShoppingCart(AddToShoppingCartDTO item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCart = user.UserCart;

            if (item.SelectedCourseId != null && userShoppingCart != null)
            {
                var course = this.GetDetailsForCourse(item.SelectedCourseId);
                //{896c1325-a1bb-4595-92d8-08da077402fc}

                if (course != null)
                {
                    CourseInShoppingCart itemToAdd = new CourseInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        CurrentCourse = course,
                        CourseId = course.Id,
                        UserCart = userShoppingCart,
                        ShoppingCartId = userShoppingCart.Id,
                    };

                    var existing = userShoppingCart.CourseInShoppingCarts.Where(z => z.ShoppingCartId == userShoppingCart.Id && z.CourseId == itemToAdd.CourseId).FirstOrDefault();

                    if (existing != null)
                    {
                        this._courseInShoppingCartRepository.Update(existing);
                    }
                    else
                    {
                        this._courseInShoppingCartRepository.Insert(itemToAdd);
                    }
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewCourse(Course p)
        {
            this._courseRepository.Insert(p);
        }

        public void DeleteCourse(Guid id)
        {
            var course = this.GetDetailsForCourse(id);
            this._courseRepository.Delete(course);
        }

        public List<Course> GetAllCourses()
        {
            return this._courseRepository.GetAll().ToList();
        }

        public Course GetDetailsForCourse(Guid? id)
        {
            return this._courseRepository.Get(id);
        }

        public AddToShoppingCartDTO GetShoppingCartInfo(Guid? id)
        {
            var course = this.GetDetailsForCourse(id);
            AddToShoppingCartDTO model = new AddToShoppingCartDTO
            {
                SelectedCourse = course,
                SelectedCourseId = course.Id,
            };

            return model;
        }

        public void UpdeteExistingCourse(Course p)
        {
            this._courseRepository.Update(p);
        }
    }
}
