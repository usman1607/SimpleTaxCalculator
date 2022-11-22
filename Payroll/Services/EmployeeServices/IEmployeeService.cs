using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Services
{
    public interface IEmployeeService
    {
        EmployeeResponses CreateEmployee(CreateEmployeeModel model);
        EmployeeResponses Get(int id);
        EmployeesResponse GetEmployees();
        EmployeeResponses Get(string email);
    }
}
