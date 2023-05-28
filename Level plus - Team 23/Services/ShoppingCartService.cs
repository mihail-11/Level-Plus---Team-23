using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.Interfaces;
using Level_plus___Team_23.Models;
using Level_plus___Team_23.DTO;
using Level_plus___Team_23.Relations;
using System.Text;

namespace Level_plus___Team_23.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<EmailMessage> _mailRepository;
        private readonly IRepository<CourseInOrder> _courseInOrderRepository;
        private readonly IUserRepository _userRepository;
        private EmailService emailService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IUserRepository userRepository, IRepository<EmailMessage> mailRepository, IRepository<Order> orderRepository, IRepository<CourseInOrder> courseInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _courseInOrderRepository = courseInOrderRepository;
            _mailRepository = mailRepository;
            emailService = new EmailService();
        }


        public bool deleteCourseFromShoppingCart(string userId, Guid courseId)
        {
            if (!string.IsNullOrEmpty(userId) && courseId != null)
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.CourseInShoppingCarts.Where(z => z.CourseId.Equals(courseId)).FirstOrDefault();

                userShoppingCart.CourseInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userCart = loggedInUser.UserCart;

                var allCourses = userCart.CourseInShoppingCarts.ToList();

                var allCoursePrices = allCourses.Select(z => new
                {
                    Price = z.CurrentCourse.Price,
                }).ToList();

                double totalPrice = 0.0;

                foreach (var item in allCoursePrices)
                {
                    totalPrice += item.Price;
                }

                var result = new ShoppingCartDTO
                {
                    Courses = allCourses,
                    TotalPrice = totalPrice
                };

                return result;
            }
            return new ShoppingCartDTO();
        }

        public bool order(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                var loggedInUser = this._userRepository.Get(userId);
                var userCart = loggedInUser.UserCart;

                EmailMessage mail = new EmailMessage();
                mail.mailAddress = loggedInUser.Email;
                //mail.Subject = "Sucessfuly created order!";
                //mail.Status = false;


                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<CourseInOrder> courseInOrders = new List<CourseInOrder>();

                var result = userCart.CourseInShoppingCarts.Select(z => new CourseInOrder
                {
                    Id = Guid.NewGuid(),
                    CourseId = z.CourseId,
                    Course = z.CurrentCourse,
                    OrderId = order.Id,
                    Order = order
                }).ToList();

                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= result.Count(); i++)
                {
                    var currentItem = result[i - 1];
                    totalPrice += currentItem.Course.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Course.Title + " with price of: $" + currentItem.Course.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                //mail.Content = sb.ToString();


                courseInOrders.AddRange(result);

                foreach (var item in courseInOrders)
                {
                    this._courseInOrderRepository.Insert(item);
                    var emailMessage = new EmailMessage
                    {
                        mailAddress = loggedInUser.Email,
                        course = item.Course,
                    };

                    emailService.SendEmailAsync(emailMessage);
                }

                loggedInUser.UserCart.CourseInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);
                this._mailRepository.Insert(mail);

                return true;
            }

            return false;
        }
    }
}
