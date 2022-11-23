using Payroll.Models;
using Payroll.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly ITaxService _taxService;
        private readonly IEmployeeRepository _employeeRepository;

        public PayrollService(IEmployeeRepository employeeRepository, ITaxService taxService)
        {
            _taxService = taxService;
            _employeeRepository = employeeRepository;
        }

        public PAYEResopnse CalculateEmployeePayePerAnnum(string email)
        {
            var employee = _employeeRepository.GetEmployee(email);
            if (employee == null)
            {
                return new PAYEResopnse
                {
                    Status = false,
                    Message = $"Employee with email {email} not found"
                };
            }

            var paye = _taxService.CalculateTax(employee.AnnualSalary);

            return new PAYEResopnse
            {
                Status = true,
                Message = "Success",
                PAYE = Math.Round(paye, 2, MidpointRounding.AwayFromZero)
            };
        }

        public PAYEResopnse CalculateEmployeePayePerMonth(string email)
        {
            var employee = _employeeRepository.GetEmployee(email);
            if (employee == null)
            {
                return new PAYEResopnse
                {
                    Status = false,
                    Message = $"Employee with email {email} not found"
                };
            }

            var paye = _taxService.CalculateTax(employee.AnnualSalary);

            return new PAYEResopnse
            {
                Status = true,
                Message = "Success",
                PAYE = Math.Round(paye/12, 2, MidpointRounding.AwayFromZero)
            };
        }

        public SalaryResponse GetMonthlySalaryAfterTax(string email)
        {
            var employee = _employeeRepository.GetEmployee(email);
            if (employee == null)
            {
                return new SalaryResponse
                {
                    Status = false,
                    Message = $"Employee with email {email} not found"
                };
            }

            var paye = _taxService.CalculateTax(employee.AnnualSalary);

            var annualSalaryAfterTax = employee.AnnualSalary - paye;

            return new SalaryResponse
            {
                Status = true,
                Message = "Success",
                FullName = employee.FirstName + " " + employee.LastName,
                StaffNo = employee.StaffNo,
                Email = email,
                SalaryAfteTax = Math.Round(annualSalaryAfterTax / 12, 2, MidpointRounding.AwayFromZero) 
            };
        }
    }
}
