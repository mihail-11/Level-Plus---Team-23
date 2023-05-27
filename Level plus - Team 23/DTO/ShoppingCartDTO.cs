using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.Relations;

namespace Level_plus___Team_23.DTO
{
    public class ShoppingCartDTO
    {
        public List<CourseInShoppingCart> Courses { get; set; }

        public double TotalPrice { get; set; }
    }
}
