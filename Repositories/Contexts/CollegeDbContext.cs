using System;
using System.Collections.Generic;
using System.Text;
using DomainModels.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Contexts
{
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions options) : base (options) { }

        public DbSet<Course> Courses { get; set; }  // the DbContext will retrieve course(s) in the Courses table
    }
}
