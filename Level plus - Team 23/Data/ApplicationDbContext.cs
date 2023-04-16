using Level_plus___Team_23.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Level_plus___Team_23.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<ApplicationUser> users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Level_plus___Team_23.Models.Course> Course { get; set; }
    }
}
