using Level_plus___Team_23.Data;
using Level_plus___Team_23.Domain;
using Level_plus___Team_23.Interfaces;
using Level_plus___Team_23.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Level_plus___Team_23.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> getAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.CourseInOrders)
                .Include("CourseInOrders.Course")
                .ToListAsync().Result;
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.CourseInOrders)
               .Include("CourseInOrders.Course")
               .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
