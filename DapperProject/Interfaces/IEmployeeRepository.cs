using DapperProject.Classes;
using DapperProject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject.Interfaces
{
   public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();

        public Task<Employee> GetEmployeeById(int id);

        public Task<Employee> CreateEmployee(EmployeeDTO employee);


    }
}
