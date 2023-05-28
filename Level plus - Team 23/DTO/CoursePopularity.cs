using System;
using System.Diagnostics.CodeAnalysis;

namespace Level_plus___Team_23.DTO
{
    public class CoursePopularity : IComparable
        <CoursePopularity>
    {
        public string Title { get; set; }

        public int Count { get; set; }

        public Guid CourseID { get; set; }

        public int CompareTo([AllowNull] CoursePopularity other)
        {
            return -Count.CompareTo(other.Count);
        }
    }
}
