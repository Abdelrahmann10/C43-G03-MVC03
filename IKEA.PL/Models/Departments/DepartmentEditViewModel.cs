using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.Models.Departments
{
    public class DepartmentEditViewModel
    {
        [Required(ErrorMessage = "Code is Required!!")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }
    }
}
