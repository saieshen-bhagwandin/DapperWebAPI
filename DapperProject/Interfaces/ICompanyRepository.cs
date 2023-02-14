using DapperProject.Classes;
using DapperProject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject.Interfaces
{
   public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();

        public Task<Company> GetCompanyById(int id);

        public Task<Company> CreateCompany(CompanyDTO company);

    }
}
