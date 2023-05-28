using Level_plus___Team_23.Domain;
using Level_plus___Team_23.Relations;
using System;
using System.Collections.Generic;

namespace Level_plus___Team_23.Models
{
    public class Course : BaseEntity
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public float Price { get; set; }

        public ApplicationUser Student { get; set; }

        public virtual ICollection<CourseInShoppingCart> CourseInShoppingCarts { get; set; }
        public virtual ICollection<CourseInOrder> CourseInOrders { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            Course course = obj as Course;

            return course.Id.Equals(this.Id);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
             return Id.GetHashCode();
        }
    }
}
