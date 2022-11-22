using Microsoft.AspNetCore.Mvc;
using Payroll.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpGet]
        public IActionResult GetMonthlyPAYE(string email)
        {
            var response = _payrollService.CalculateEmployeePayePerMonth(email);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetAnnualPAYE(string email)
        {
            var response = _payrollService.CalculateEmployeePayePerAnnum(email);
            return Ok(response);
        }

        [HttpGet]
        public IActionResult GetEmployeeSalaryPerMonth(string email)
        {
            var response = _payrollService.GetMonthlySalaryAfterTax(email);
            return Ok(response);
        }
    }
}
