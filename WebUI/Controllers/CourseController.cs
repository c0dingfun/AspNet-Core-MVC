using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModelServices.Interfaces;
using ViewModelServices.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseViewModelService mService;
        public CourseController(ICourseViewModelService service)
        {
            mService = service;
        }
        


        // GET: /<controller>/
        public IActionResult Index()
        {
            CourseViewModel viewModel = mService.GetCourseViewModel();
            return View(viewModel);
        }
    }
}
