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

        [HttpGet( Name = "GetEmployees")]
        public async Task<IActionResult> GetEmployees() {

            var employee = await employeeRepo.GetEmployees();

            return Ok(employee);
        }

        [HttpGet("{id}",Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {

            var employee = await employeeRepo.GetEmployeeById(id);

            return Ok(employee);
        }

    }
}
