namespace Level_plus___Team_23.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string VideoUrl { get; set; }

        public float Price { get; set; }

        public ApplicationUser Student { get; set; }
    }
}
