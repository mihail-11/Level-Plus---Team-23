
namespace Level_plus___Team_23.Models
{
    public class PurchasedCourse
    {



        public int PurchasedCourseID { get; set; }

        public ApplicationUser Student { get; set; }


        public Course Course { get; set; }
    }
}
