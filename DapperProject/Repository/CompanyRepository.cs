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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<Company> CreateCompany(CompanyDTO company)
        {
            var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name,@Address,@Country)" + 
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();

            parameters.Add("Name", company.CompanyName, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection()) {

               var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdcompany = new Company {

                    Id = id,
                    Name = company.CompanyName,
                    Address = company.Address,
                    Country = company.Country

                };

                return createdcompany;

            }

        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";

            using (var connection = _context.CreateConnection()) {

                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            
            }

        }

        public async Task<Company> GetCompanyByEmployeeId(int id)
        {
            var procedurename = "ShowCompanyForProvidedEmployeeId";

            var parameters = new DynamicParameters();

            parameters.Add("Id", id, DbType.Int32, ParameterDirection.Input);

            using (var connection = _context.CreateConnection()) {

                var company = await connection.QueryFirstOrDefaultAsync<Company>(procedurename, parameters, commandType : CommandType.StoredProcedure);

                return company;
            }
        }

        public async Task<Company> GetCompanyById(int id)
        {
            var query = "SELECT * FROM Companies WHERE Id = @id";

            using (var connection = _context.CreateConnection())
            {

                var companies = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });
                return companies;

            }
        }

        public async Task UpdateCompany(Company company)
        {
            var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";

            var parameters = new DynamicParameters();

            parameters.Add("Id", company.Id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection()) {

   
                await connection.ExecuteAsync(query, parameters);
            
            }

        }
    }
}
