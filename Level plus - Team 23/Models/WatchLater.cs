using System.Collections;
using System.Collections.Generic;

namespace Level_plus___Team_23.Models
{
    public class WatchLater
    {

        public int WatchLaterID { get; set; }

        public ApplicationUser Student { get; set; }

        public Course Course { get; set; }
    }
}
