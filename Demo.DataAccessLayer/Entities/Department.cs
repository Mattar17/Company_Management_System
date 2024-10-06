using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccessLayer.Entities
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreationDate { get; set; }

        [InverseProperty("Department")]
        public ICollection<Employee> Employees { get; set; }
    }
}
