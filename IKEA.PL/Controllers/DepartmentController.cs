using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services;
using IKEA.PL.Models.Departments;
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

        #region Details
        [HttpGet] //Department/Details/id
        public IActionResult Details(int? id)
        {
            if(id is null)
            {
                return BadRequest(); // error "400"
            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if(department is null)
            {
                return NotFound(); // error "404"
            }
            return View(department);
        }

        #endregion

        #region Edit
        #region Get
        [HttpGet] //Department/Edit/id?
        public IActionResult Edit(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if(department is null)
            {
                return NotFound();
            }
            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });
        }
        #endregion
        #region Post
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if(!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                var updateDepartment = new UpdatedDepartmentDTO()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };
                var updated = _departmentService.UpdateDepartment(updateDepartment) > 0;
                if(updated)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry, An error ocuured while updating the department";
            }
            catch(Exception ex)
            {
                // 1- Log Exception
                _logger.LogError(ex, ex.Message);
                // 2- Set Message
                message = _environment.IsDevelopment() ? ex.Message : "Sorry, An error ocuured while updating the department";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);
        }
        #endregion
        #endregion

        #region Delete
        #region Get
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if(department is null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion
        #region Post
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;
            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                message = "Sorry, An error ocuured during deleting the department";
            }
            catch (Exception ex)
            {
                // 1- Log Exception
                _logger.LogError(ex, ex.Message);
                // 2- Set Message
                message = _environment.IsDevelopment()? ex.Message : "Sorry, An error ocuured during deleting the department";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
        #endregion
    }
}
