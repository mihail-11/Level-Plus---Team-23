using Microsoft.VisualBasic;

namespace Level_plus___Team_23.Models
{
    public class ReportStudent
    {
        public Student ReportedStudent { get; set; }

        public Student StudentReporting { get; set; }

        public DateAndTime Date { get; set; }

        public string Cause { get; set; }
    }
}
