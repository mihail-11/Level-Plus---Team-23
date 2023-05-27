using Microsoft.VisualBasic;
using System.Collections.Generic;
using Level_plus___Team_23.Domain;
using Level_plus___Team_23.Relations;

namespace Level_plus___Team_23.Models
{
    public class ShoppingCart : BaseEntity
    {

        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        public virtual ICollection<CourseInShoppingCart> CourseInShoppingCarts { get; set; }
    }
}
