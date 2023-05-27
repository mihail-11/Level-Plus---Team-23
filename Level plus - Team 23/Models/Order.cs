using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.Domain;
using Level_plus___Team_23.Relations;

namespace Level_plus___Team_23.Models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public virtual ICollection<CourseInOrder> CourseInOrders { get; set; }
    }
}
