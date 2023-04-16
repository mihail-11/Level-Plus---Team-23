using System.Collections;
using System.Collections.Generic;

namespace Level_plus___Team_23.Models
{
    public class GivenOption
    {
       
        public GivenOption() {
            this.Options = new List
                <Option>();
        }
        public virtual ICollection<Option> Options { get; set; }

        public Question Question { get; set; }
    }
}
