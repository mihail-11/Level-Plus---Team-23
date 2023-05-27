using System;
using Level_plus___Team_23.Domain;

namespace Level_plus___Team_23.Models
{
    public class EmailMessage : BaseEntity
    {
        public string mailAddress { get; set; }
        public Course course { get; set; }
    }
}
