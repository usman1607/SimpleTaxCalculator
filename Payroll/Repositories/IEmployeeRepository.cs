using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Repositories
{
    public interface IEmployeeRepository
    {
        Employee AddNewEmployee(Employee employee);
        Employee GetEmployee(string email);
    }
}
