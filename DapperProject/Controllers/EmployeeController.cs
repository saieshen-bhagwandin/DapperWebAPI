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
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRepository employeeRepo;

        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            employeeRepo = _employeeRepository;
        }

        [HttpGet(Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {

            try
            {

                var employee = await employeeRepo.GetEmployees();

                return Ok(employee);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await employeeRepo.GetEmployeeById(id);

                return CreatedAtRoute("EmployeeById", new { id = employee.Id }, employee);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }


        }


        [HttpPost(Name = "CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employee)
        {

            try
            {
                var createdemployee = await employeeRepo.CreateEmployee(employee);

                return Ok(createdemployee);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }


        }



        [HttpPut]
        public async Task<IActionResult> UpdatedEmployee(Employee employee) {

            try
            {
                await employeeRepo.UpdateEmployee(employee);

                return CreatedAtRoute("EmployeeById", new { id = employee.Id }, employee);

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }



        }

    }

}
