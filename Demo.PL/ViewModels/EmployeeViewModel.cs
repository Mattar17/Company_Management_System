using Demo.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Lenght is 50")]
        [MinLength(5, ErrorMessage = "Min Lenght is 5")]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        [Range(22, 60, ErrorMessage = "Age Must be between 22,60")]
        public int? Age { get; set; }

        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress] public string Email { get; set; }

        [Phone] public string Phone { get; set; }
        public DateTime HiringDate { get; set; }

        #region Relation
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        [InverseProperty("Employees")]
        public Department Department { get; set; }

        #endregion


    }
}
