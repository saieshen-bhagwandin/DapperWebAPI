using Dapper;
using DapperProject.Classes;
using DapperProject.Context;
using DapperProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var query = "SELECT * FROM Employees";

            using (var connection = _context.CreateConnection())
            {

                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();

            }

        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var query = "SELECT * FROM Employees WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {

                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { id });
                return employee;

            }
        }

    }
}
