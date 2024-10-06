using Demo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Task<IEnumerable<Employee>> GetEmployeeByAddressAsync(string Address);

        public Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string SearchValue);
    }
}
