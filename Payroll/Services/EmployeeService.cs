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

        public Employee CreateEmployee(CreateEmployeeModel model)
        {
            var employees = EmployeeRepository.employees;
            int id = employees.Count == 0 ? 1 : employees[employees.Count - 1].Id + 1;            

            if (EmailExist(model.Email))
            {
                throw new Exception($"Email {model.Email} already exist.");
            }

            if(model.AnnualSalary <= 0)
            {
                throw new Exception("invalid salary");
            }

            var dateJoin = DateTime.Now;
            var employee = new Employee(id, model.FirstName, model.LastName, model.Address, model.Email, dateJoin, model.AnnualSalary);

            return _employeeRepository.AddNewEmployee(employee);
        }

        public Employee GetEmployee(int id)
        {
            var employees = EmployeeRepository.employees;
            foreach (var employee in employees)
            {
                if (employee.Id == id)
                {
                    return employee;
                }
            }
            throw new Exception($"Employee with the id: {id} not found.");
        }

        public Employee GetEmployeeByEmail(string email)
        {
            var employees = EmployeeRepository.employees;
            foreach (var employee in employees)
            {
                if (employee.Email == email)
                {
                    return employee;
                }
            }
            throw new Exception($"Employee with the email: {email} not found.");
        }

        public List<Employee> GetEmployees()
        {
            return EmployeeRepository.employees;
        }

        private bool EmailExist(string email)
        {
            var employees = EmployeeRepository.employees;
            foreach(var employee in employees)
            {
                if(employee.Email == email)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
