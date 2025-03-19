using AutoMapper;
using IKEA.BLL.Models.Departments;
using IKEA.BLL.Services;
using IKEA.PL.Models.Departments;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<CreatedDepartmentDTO> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, ILogger<CreatedDepartmentDTO> logger, IWebHostEnvironment environment, IMapper mapper)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
            _mapper = mapper;
        }


        #region Index
        // BaseUrl/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            // View's dictionary : Pass data from controller to View [from this view => Partial view, LayOut]
            // 1- ViewData : is a Dictionary type property (Introduced in ASP.NET FrameWork 3.5) helps us to transfer the data from controller[Action] to view
            ViewData["Message"] = "Hello ViewData";
            // 2- ViewBag : is a Dynamic type property (Introduced in ASP.NET FrameWork 4(*based on dynamic prop*)) helps us to transfer the data from controller[Action] to view
            ViewBag.Message = "Hello ViewBag";
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
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentEditViewModel departmentVm)
        {
            if(!ModelState.IsValid) //Server side validation
            {
                return View(departmentVm);
            }
            var message = string.Empty;
            try
            {
                var createddepartment = _mapper.Map<CreatedDepartmentDTO>(departmentVm);
                var result = _departmentService.CreateDepartment(createddepartment);
                // 3- ViewData : is a property type Dictionary object (Introduced in ASP.NET FrameWork 3.5) helps us to transfer the data between 2 requests
                if (result > 0)
                {
                    TempData["Message"] = "Department Is Created";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Department Has Not been Created";
                    message = "Department Has Not been Created";
                    ModelState.AddModelError(string.Empty,message);
                    return View(departmentVm);
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
                    return View(departmentVm);
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
            var departmentVm = _mapper.Map<DepartmentDetailsToReturnDTO, DepartmentEditViewModel>(department);
            return View(departmentVm);
        }
        #endregion
        #region Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if(!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var message = string.Empty;
            try
            {
                //manual mapping
                //var updateDepartment = new UpdatedDepartmentDTO()
                //{
                //    Id = id,
                //    Code = departmentVM.Code,
                //    Name = departmentVM.Name,
                //    Description = departmentVM.Description,
                //    CreationDate = departmentVM.CreationDate
                //};
                var updatedDepartment = _mapper.Map<UpdatedDepartmentDTO>(departmentVM);
                var result = _departmentService.UpdateDepartment(updatedDepartment) > 0;
                if(result)
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
            if (id is null)
            {
                return BadRequest();
            }
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
            {
                return NotFound();
            }
            return View(department);
        }
        #endregion
        #region Post
        [HttpPost]
        //[ValidateAntiForgeryToken]

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
                // 2- Get Detailed Error Message
                var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                // 3- Set Message
                message = _environment.IsDevelopment() ? innerException : "Sorry, an error occurred while deleting this Department :(";
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion
        #endregion
    }
}
