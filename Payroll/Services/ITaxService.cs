using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Services
{
    public interface ITaxService
    {
        decimal CalculateTax(decimal grossIncome);
    }
}
