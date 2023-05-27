using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.Models;

namespace Level_plus___Team_23.DTO
{
    public class AddToShoppingCartDTO
    {
        public Course SelectedCourse { get; set; }
        public Guid SelectedCourseId { get; set; }
    }
}
