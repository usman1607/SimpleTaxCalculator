using Payroll.Models;
using Payroll.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Services
{
    public class TaxService : ITaxService
    {
        public decimal CalculateTax(decimal grossIncome)
        {
            decimal tax = 0;

            decimal taxableIncome = GetTaxableIncome(grossIncome);

            if (taxableIncome > 300000)
            {
                tax += 0.07m * 300000;
            }
            else
            {
                tax = 0.07m * taxableIncome;
                return tax;
            }

            if (taxableIncome > 600000)
            {
                tax += 0.11m * 300000;
            }
            else
            {
                tax += 0.11m * (taxableIncome - 300000);
                return tax;
            }

            if (taxableIncome > 1100000)
            {
                tax += 0.15m * 500000;
            }
            else
            {
                tax += 0.15m * (taxableIncome - 600000);
                return tax;
            }

            if (taxableIncome > 1600000)
            {
                tax += 0.19m * 500000;
            }
            else
            {
                tax += 0.19m * (taxableIncome - 1100000);
                return tax;
            }

            if (taxableIncome > 3200000)
            {
                tax += 0.21m * 1600000;
            }
            else
            {
                tax += 0.21m * (taxableIncome - 1600000);
                return tax;
            }
            tax += 0.24m * (taxableIncome - 3200000);

            return tax;
        }

        private decimal GetTaxableIncome(decimal grossIncome)
        {
            decimal pension = 0.08m * grossIncome;
            decimal GIForConsolidatedReliefAllowance = grossIncome - pension;

            decimal higherOfNGN = grossIncome > 20000000 ? 0.01m * grossIncome : 200000;
            decimal consolidatedReliefAllowance = 0.2m * GIForConsolidatedReliefAllowance;

            decimal taxableIncome = grossIncome - (higherOfNGN + consolidatedReliefAllowance + pension);

            return taxableIncome;
        }
    }
}
