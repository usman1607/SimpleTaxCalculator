using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public static List<Employee> employees = new List<Employee>();

        public Employee AddNewEmployee(Employee employee)
        {            
            employees.Add(employee);

            return employee;
        }

        public Employee GetEmployee(string email)
        {
            foreach (var employee in employees)
            {
                if(employee.Email == email)
                {
                    return employee;
                }
            }
            return null;
        }
    }
}
