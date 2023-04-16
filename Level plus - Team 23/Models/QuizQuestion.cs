using System.Collections.Generic;

namespace Level_plus___Team_23.Models
{
    public class QuizQuestion
    {

        public QuizQuestion() {
            this.Questions = new List<Question>();
        }
        public Quiz Quiz { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
