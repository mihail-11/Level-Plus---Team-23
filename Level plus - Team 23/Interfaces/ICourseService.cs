using Level_plus___Team_23.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.DTO;

namespace Level_plus___Team_23.Interfaces
{
    public interface ICourseService
    {
        List<Course> GetAllCourses();
        Course GetDetailsForCourse(Guid? id);
        void CreateNewCourse(Course p);
        void UpdeteExistingCourse(Course p);
        AddToShoppingCartDTO GetShoppingCartInfo(Guid? id);
        void DeleteCourse(Guid id);
        bool AddToShoppingCart(AddToShoppingCartDTO item, string userID);
    }
}
