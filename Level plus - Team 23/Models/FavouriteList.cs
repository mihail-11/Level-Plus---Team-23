
namespace Level_plus___Team_23.Models
{
    public class FavouriteList


    {
        public int ID { get; set; }
        public ApplicationUser Student { get; set; }

        public Course Course { get; set; }
    }
}