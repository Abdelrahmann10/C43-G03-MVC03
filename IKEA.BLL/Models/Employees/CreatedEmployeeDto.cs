﻿using IKEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Models.Employees
{
    public class CreatedEmployeeDto
    {
        //[Required]
        [MaxLength(50, ErrorMessage = "Max length of name is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min length of name is 5 Chars")]
        public string Name { get; set; } = null!;

        [Range(22,30)]
        public int? Age { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$"
                            , ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string? Address { get; set; }
        public decimal Salary { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Display(Name = "Phone Number")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

    }
}
