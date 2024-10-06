using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Contexts;
using Demo.DataAccessLayer.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MvcDemoDbContext dbContext) : base(dbContext) { }


        public async Task<IEnumerable<Employee>> GetEmployeeByAddressAsync(string Address)
        {

            return (IEnumerable<Employee>) await _dbContext.Employees.FindAsync(Address);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByNameAsync(string SearchValue)
        {
            return  _dbContext.Employees.Where(e => e.Name.ToLower().Contains(SearchValue.ToLower()));
        }
    }
}
