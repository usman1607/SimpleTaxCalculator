using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Services
{
    public interface IPayrollService
    {
        PAYEResopnse CalculateEmployeePayePerMonth(string email);
        PAYEResopnse CalculateEmployeePayePerAnnum(string email);
        SalaryResponse GetMonthlySalaryAfterTax(string email); 
    }
}
