using Microsoft.VisualBasic;

namespace Level_plus___Team_23.Models
{
    public class FinishedCourse
    {
        public Student Student { get; set; }

        public Course Course { get; set; }

        public DateAndTime Date { get; set; }


        
        /*
         * Врз база на квизовите ќе се пресмета оцената.
         * 
         * public float Grade { get; set; }
         */


    }
}
