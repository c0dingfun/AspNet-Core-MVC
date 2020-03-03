using System;
using System.Collections.Generic;
using System.Text;
using DomainModels.Models;
using Repositories.Contexts;
using Repositories.Interfaces;

namespace Repositories.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CollegeDbContext mDbContext;

        // This is injected 
        public CourseRepository(CollegeDbContext dbContext)
        {
            mDbContext = dbContext;
        }
        public IEnumerable<Course> GetCourses()
        {
            return mDbContext.Courses;
        }
    }
}
