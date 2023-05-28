using Level_plus___Team_23.Models;
using System.Collections.Generic;

namespace Level_plus___Team_23.DTO
{
    public class AllCourses
    {
        public List<Course> Courses { get; set; }

        public List<Course> PurchasedCourses { get; set; }

        public List<Course> WatchLaters { get; set; }

        public List<Course> LikedCourses { get; set; }
    }
}
