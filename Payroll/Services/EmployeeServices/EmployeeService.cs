using Payroll.Models;
using Payroll.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeResponses CreateEmployee(CreateEmployeeModel model)
        {
            var employees = EmployeeRepository.employees;
            int id = employees.Count == 0 ? 1 : employees[employees.Count - 1].Id + 1;

            if (EmailExist(model.Email))
            {
                return new EmployeeResponses
                {
                    Status = false,
                    Message = $"Email {model.Email} already exist."
                };
            }

            if (model.AnnualSalary <= 0)
            {
                return new EmployeeResponses
                {
                    Status = false,
                    Message = "invalid salary"
                };
            }

            var dateJoin = DateTime.Now;
            var employee = _employeeRepository.AddNewEmployee(new Employee(id, model.FirstName, model.LastName, model.Address, model.Email, dateJoin, model.AnnualSalary));

            return new EmployeeResponses
            {
                Status = true,
                Message = "Success",
                Data = employee
            };
        }

        public EmployeeResponses Get(int id)
        {
            var employees = EmployeeRepository.employees;
            foreach (var employee in employees)
            {
                if (employee.Id == id)
                {
                    return new EmployeeResponses
                    {
                        Status = true,
                        Message = "Success",
                        Data = employee
                    };
                }
            }
            return new EmployeeResponses
            {
                Status = false,
                Message = $"Emplyee with the id: {id} not found."
            };
        }

        public EmployeeResponses Get(string email)
        {
            var employees = EmployeeRepository.employees;
            foreach (var employee in employees)
            {
                if (employee.Email == email)
                {
                    return new EmployeeResponses
                    {
                        Status = true,
                        Message = "Success",
                        Data = employee
                    };
                }
            }
            return new EmployeeResponses
            {
                Status = false,
                Message = $"Employee with the email: {email} not found."
            };
        }

        public EmployeesResponse GetEmployees()
        {
            return new EmployeesResponse
            {
                Status = true,
                Message = "Success",
                Data = EmployeeRepository.employees
            };
        }

        private bool EmailExist(string email)
        {
            var employees = EmployeeRepository.employees;
            foreach (var employee in employees)
            {
                if (employee.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
