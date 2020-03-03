using System;
using System.Collections.Generic;
using System.Text;
using DomainModels.Models;

namespace ViewModelServices.ViewModels
{
    public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
    }
}
