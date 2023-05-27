using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.Domain;
using Level_plus___Team_23.Models;

namespace Level_plus___Team_23.Relations
{
    public class CourseInOrder : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
