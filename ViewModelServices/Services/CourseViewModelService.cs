using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Interfaces;
using ViewModelServices.Interfaces;
using ViewModelServices.ViewModels;

namespace ViewModelServices.Services
{
    public class CourseViewModelService : ICourseViewModelService
    {
        private readonly ICourseRepository mRepository;

        public CourseViewModelService(ICourseRepository repository)
        {
            mRepository = repository;
        }


        public CourseViewModel GetCourseViewModel()
        {
            return new CourseViewModel
            {
                Courses = mRepository.GetCourses()
            };
        }
    }
}
