using Microsoft.AspNetCore.Identity;

namespace Level_plus___Team_23.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }

       // public string Picture { get; set; }
    }
}