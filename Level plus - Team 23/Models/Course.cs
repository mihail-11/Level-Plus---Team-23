using Level_plus___Team_23.Domain;
using Level_plus___Team_23.Relations;
using System;
using System.Collections.Generic;

namespace Level_plus___Team_23.Models
{
    public class Course : BaseEntity
    {
        public Guid  CourseID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public float Price { get; set; }

        public ApplicationUser Student { get; set; }

        public virtual ICollection<CourseInShoppingCart> CourseInShoppingCarts { get; set; }
        public virtual ICollection<CourseInOrder> CourseInOrders { get; set; }
    }
}
