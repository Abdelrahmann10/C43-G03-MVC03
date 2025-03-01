using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<CreatedDepartmentDTO> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<CreatedDepartmentDTO> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }


        #region Index
        // BaseUrl/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartment();
            return View(department);
        }
        #endregion
        #region Create
        #region Get
        [HttpGet]
        //BaseUrl/Department/Create
        public IActionResult Create()
        {
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        public IActionResult Create(CreatedDepartmentDTO department)
        {
            if(!ModelState.IsValid) //Server side validation
            {
                return View(department);
            }
            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(department);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department is not created";
                    ModelState.AddModelError(string.Empty,message);
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                // 1- Log Exeption
                _logger.LogError(ex,ex.Message);
                // 2- Set Frindly Message
                if(_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else
                {
                    message = "Department is not created";
                    return View("Error", message);
                }
            }
        }
        #endregion
        #endregion
    }
}
