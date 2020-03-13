# AspNet-Core-MVC

This project is meant to capture the essence the following using dotNet core 3.1: 

1. ASP.Net Core MVC and out of box Authentication/Authorization
2. Dependency Injection via IOC Container
3. Entity Framework Core (DbContext)
4. Repository Pattern
5. Razor View
6. CSS styling

## Details on Implementing the Repository Pattern and the DI/IOC

1. A Controller (CourseController) creates the CourseViewModel, then passes to the View (Views/Course/Index.cshtml).

```aspx-csharp
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
```

2. CourseViewModel

```aspx-csharp
    public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
    }
```

3. ICourseViewModelService and CourseViewModelService 

```csharp
    public interface ICourseViewModelService
    {
        CourseViewModel GetCourseViewModel();
    }
```

```csharp
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
```

4. Domain Model - Course.cs

```csharp
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
```

5. Repository - ICourseRepository, CourseRepository and CollegeDbContext

```csharp
    public interface ICourseRepository
    {
        IEnumerable<Course> GetCourses();
    }
```

```csharp
    public class CourseRepository : ICourseRepository
    {
        private readonly CollegeDbContext mDbContext;

        // The CollegeDbContext is injected due to the setup of the IOC container in Startup.cs:
        //services.AddDbContext<CollegeDbContext>(options =>
        //        options.UseSqlServer(
        //            Configuration.GetConnectionString("CollegeDBConnection")));
        public CourseRepository(CollegeDbContext dbContext)
        {
            mDbContext = dbContext;
        }
        public IEnumerable<Course> GetCourses()
        {
            return mDbContext.Courses;
        }
    }
```

```csharp
    public class CollegeDbContext : DbContext
    {
        public CollegeDbContext(DbContextOptions options) : base (options) { }

        public DbSet<Course> Courses { get; set; }  // the DbContext will retrieve course(s) in the Courses table
    }
```

6. IOC Container

```csharp
    /// <summary>
    /// Basically, it tries to have a static method to register all dependencies
    /// for injection into the IServiceCollection.
    /// Note: We can do the same in the Startup.cs' ConfigureServices(), but just
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
```

7. In order for the Repository Pattern to work, need to NuGet the following Entity Framework Core packages

    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools

8. Do migration to setup Database and Tables, for each DbContext, via the Code First style:

    - setup the connection string in the appsetting.json file

    - add-migration initmigration -Context CollegeDbContext
    - update-database -Context CollegeDbContext

- Note: To remove last migration

```iecst
PM> update-database -Migration 0 -Context CollegeDbContext
PM> remove-migration -Context CollegeDbContext
```

## Summary

Basically, a Controller gets the ViewModel, via the injected ViewModelService, which in turn uses the injected Repository to create the ViewModel. The Repository uses the injected DbContext to return the Domain Models (Courses) which used by the ViewModel Service to construct the ViewModel.

Note: there are 3 injections in the above sequence. Two injections are setup by our own DependencyInject.RegisterService() (see above) and one is setup in the Startup.cs

```csharp
    // Prepare the dotnet core IOC container for doing injection of the DbContext
    services.AddDbContext<CollegeDbContext>(options =>
        options.UseSqlServer(
            Configuration.GetConnectionString("CollegeDBConnection")));
```

## Credit

Ideas of this project came from one of Udemy dotnet core classes.

