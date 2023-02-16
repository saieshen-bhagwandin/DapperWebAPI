using DapperProject.Classes;
using DapperProject.Dto;
using DapperProject.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private readonly ICompanyRepository _companyRepo;

        public CompanyController(ICompanyRepository companyRepo)
        {

            _companyRepo = companyRepo;
        }


        [HttpGet(Name = "Companies")]
        public async Task<IActionResult> GetCompanies()
        {
           try
            {
                var companies = await _companyRepo.GetCompanies();
                return Ok(companies);
            }
            catch (Exception ex) {

                return StatusCode(500, ex.Message);    
            }
        
        
        }


        [HttpGet("{id}",Name = "CompanyById")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyById(id);

                return Ok(company);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }


        }


        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyDTO company)
        {
            try
            {

                var createdcompany = await _companyRepo.CreateCompany(company);

                return CreatedAtRoute("CompanyById", new { id = createdcompany.Id }, createdcompany);


            }
            catch (Exception ex) {

                return StatusCode(500, ex.Message);

            }
        
        
        }


        [HttpPut]

        public async Task<IActionResult> UpdateCompany(Company company) {

            try
            {
               await _companyRepo.UpdateCompany(company);

                return CreatedAtRoute("CompanyById", new { id = company.Id }, company);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        [HttpGet]
        [Route("CompanyByEmployeeId/{id}")]

        public async Task<IActionResult> GetCompanyByEmployeeId(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompanyByEmployeeId(id);
                return Ok(company);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


    }
}
