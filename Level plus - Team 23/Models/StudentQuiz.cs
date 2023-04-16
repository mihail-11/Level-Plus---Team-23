using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;

namespace Level_plus___Team_23.Models
{
    public class StudentQuiz
    {

        public StudentQuiz
            ()
        { 
            this.Quizzes = new List<Quiz> ();
            }

        public Student Student { get; set; }

        public virtual ICollection<Quiz> Quizzes { get; set; }
        public DateAndTime Date { get; set; }

        public float Score { get; set; }
    }
}
