using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Repositories.Repositories;
using ViewModelServices.Interfaces;
using ViewModelServices.Services;

namespace IOCContainer
{
    /// <summary>
    /// Basically, it try to have a static method to register all dependencies
    /// for injection into the IServiceCollection.
    /// Note: We can do the same in the Startup's ConfigureServices(), but just
    /// to exercise the IServiceCollection out side of it, for fun.
    /// </summary>
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // CourseViewModel Service 
            services.AddScoped<ICourseViewModelService, CourseViewModelService>();

            // Course Repository
            services.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}
