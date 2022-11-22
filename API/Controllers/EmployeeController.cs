using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.Models;
using Payroll.Services;
using System.Reflection;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var response = _employeeService.Get(id);
            
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetByEmail(string email)
        {
            var response = _employeeService.Get(email);

            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _employeeService.GetEmployees();

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeModel model)
        {
            var response = _employeeService.CreateEmployee(model);
            
            return Ok(response);
        }        

        

    }
}
