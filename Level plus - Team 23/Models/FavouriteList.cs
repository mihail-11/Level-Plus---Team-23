﻿using System.Collections;
using System.Collections.Generic;

namespace Level_plus___Team_23.Models
{
    public class FavouriteList

       
    {

        public FavouriteList()
        {
            this.Courses = new List<Course>();
        }
        public Student Student { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}