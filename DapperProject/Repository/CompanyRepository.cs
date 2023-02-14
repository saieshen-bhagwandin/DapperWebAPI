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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;

        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "SELECT * FROM Companies";

            using (var connection = _context.CreateConnection()) {

                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            
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
    }
}
