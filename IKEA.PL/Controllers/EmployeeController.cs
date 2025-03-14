using IKEA.BLL.Models.Departments;
using IKEA.BLL.Models.Employees;
using IKEA.BLL.Services.Employees;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment webHostEnvironment, ILogger<EmployeeController> logger) //Ask CLR for creating object from EmployeeService implicitly
        {
            _employeeService = employeeService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }
        #endregion

        #region Index
        //Employee/Index
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployee();
            return View(employees);
        }
        #endregion

        #region Create
        #region Get
        [HttpGet]
        //Employee/Create
        public IActionResult Create()
        {
            return View();
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid) //Server side validation
            {
                return View(employee);
            }
            var message = string.Empty;
            try
            {
                var result = _employeeService.CreateEmployee(employee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee has not been created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                // 1- Log Exeption
                _logger.LogError(ex, ex.Message);
                // 2- Get Detailed Error Message
                var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                // 3- Set Message
                message = _webHostEnvironment.IsDevelopment() ? innerException : "Sorry, an error occurred while creating this employee :(";

                ModelState.AddModelError(string.Empty, message);
                return View(employee);
            }
        }
        #endregion
        #endregion

        #region Details
        [HttpGet] //Employee/Details/id
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest(); // error "400"
            }
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
            {
                return NotFound(); // error "404"
            }
            return View(employee);
        }
        #endregion

        #region Edit
        #region Get
        [HttpGet] //Employee/Edit/id?
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest(); // error 400
            }
            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
            {
                return NotFound(); // error 404
            }
            return View(new UpdatedEmployeeDto()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Age = employee.Age,
                Salary = employee.Salary,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
            });
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            var message = string.Empty;
            try
            {
                var updated = _employeeService.UpdateEmployee(employee) > 0;
                if (updated)
                    return RedirectToAction(nameof(Index));
                message = "Sorry, An error ocuured while updating the Employee";
            }
            catch (Exception ex)
            {
                // 1- Log Exception
                _logger.LogError(ex, ex.Message);
                // 2- Get Details Error Message
                var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                // 3- Set Message
                message = _webHostEnvironment.IsDevelopment() ? innerException : "Sorry, an error occurred while updating this Employee :(";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);
        }
        #endregion
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry, An error ocuured during deleting the Employee";
            }
            catch (Exception ex)
            {
                // 1- Log Exception
                _logger.LogError(ex, ex.Message);
                // 2- Set Message
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Sorry, An error ocuured during deleting the Employee";
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
