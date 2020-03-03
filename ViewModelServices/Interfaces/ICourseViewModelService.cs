using System;
using System.Collections.Generic;
using System.Text;
using ViewModelServices.ViewModels;

namespace ViewModelServices.Interfaces
{
    public interface ICourseViewModelService
    {
        CourseViewModel GetCourseViewModel();
    }
}
