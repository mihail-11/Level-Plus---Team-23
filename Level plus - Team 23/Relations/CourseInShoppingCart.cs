using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.Domain;
using Level_plus___Team_23.Models;

namespace Level_plus___Team_23.Relations
{
    public class CourseInShoppingCart : BaseEntity
    {
        public Guid CourseId { get; set; }
        public virtual Course CurrentCourse { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

    }
}
