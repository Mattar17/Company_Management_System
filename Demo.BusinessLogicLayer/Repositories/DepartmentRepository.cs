using Demo.BusinessLogicLayer.Interfaces;
using Demo.DataAccessLayer.Contexts;
using Demo.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(MvcDemoDbContext dbContext) : base(dbContext) { }


    }
}
