using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Level_plus___Team_23.DTO;

namespace Level_plus___Team_23.Interfaces
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO getShoppingCartInfo(string userId);
        bool deleteCourseFromShoppingCart(string userId, Guid courseId);
        bool order(string userId);
    }
}
