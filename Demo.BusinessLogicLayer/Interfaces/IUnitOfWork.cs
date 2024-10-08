﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogicLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IDepartmentRepository DepartmentRepository { get; set; }

        public IEmployeeRepository EmployeeRepository { get; set; }

        public Task<int> CompleteAsync();

        public void Dispose();
    }
}
