using Dapper;
using DapperProject.Classes;
using DapperProject.Context;
using DapperProject.Dto;
using DapperProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Employee> CreateEmployee(EmployeeDTO employee)
        {
            var query = "INSERT INTO Employees (Name,Age,Position,CompanyId) VALUES (@Name,@Age,@Position,@CompanyId)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();

            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Age", employee.Age, DbType.Int32);
            parameters.Add("Position", employee.Position, DbType.String);
            parameters.Add("CompanyId", employee.CompanyId, DbType.Int32);

            using (var connection = _context.CreateConnection()) {

                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdemployee = new Employee
                {

                    Id = id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Position = employee.Position,
                    CompanyId = employee.CompanyId

                };


                return createdemployee;
            
            }
        }

        public async Task UpdateEmployee(Employee employee)
        {
            var query = "UPDATE Employees SET NAME = @Name, Age = @Age, Position = @Position, CompanyId = @CompanyId WHERE Id = @Id";

            var parameters = new DynamicParameters();

            parameters.Add("Id", employee.Id, DbType.Int32);
            parameters.Add("Name", employee.Name, DbType.String);
            parameters.Add("Age", employee.Age, DbType.Int32);
            parameters.Add("Position", employee.Position, DbType.String);
            parameters.Add("CompanyId", employee.CompanyId, DbType.Int32);

            using (var connection = _context.CreateConnection()) {

                await connection.ExecuteAsync(query, parameters);
            
            }
        }
    }
}
