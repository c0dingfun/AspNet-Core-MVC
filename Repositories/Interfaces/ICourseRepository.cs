using System;
using System.Collections.Generic;
using System.Text;
using DomainModels.Models;

namespace Repositories.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetCourses();
    }
}
