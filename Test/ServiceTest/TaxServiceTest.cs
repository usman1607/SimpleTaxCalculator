using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Payroll.Models;
using Payroll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Test.ServiceTest
{
    public class TaxServiceTest
    {
        private readonly TaxService _taxService;

        public TaxServiceTest()
        {
            _taxService = new TaxService();
        }

        [Fact]
        public void CalculateTaxShouldReturnCorrectPAYE()
        {
            //Arrang
            var grossIncome = 4000000m;
            var expectedPaye = 464240m;

            //Act
            var paye = _taxService.CalculateTax(grossIncome);

            //Assert
            Assert.Equal(expectedPaye, paye);
        }

        [Fact]
        public void CalculateTaxForEmployeeAbove20MillionShouldReturnCorrectPAYE()
        {
            //Arrang
            var grossIncome = 30000000m;
            var expectedPaye = 5019200m;

            //Act
            var paye = _taxService.CalculateTax(grossIncome);

            //Assert
            Assert.Equal(expectedPaye, paye);
        }
        
        [Fact]
        public void CalculateTaxForEmployeeWithTaxableLessThanOnePersentOfIncomeShouldReturnCorrectPAYE()
        {
            //Arrang
            var grossIncome = 275000m;
            var expectedPaye = 2750m; //Where the taxable Income obtained is lower than 1% of the gross income then 1%
                                      //of the gross income shall be the Tax Payable Per Annum.

            //Act
            var paye = _taxService.CalculateTax(grossIncome);

            //Assert
            Assert.Equal(expectedPaye, paye);
        }
        
        [Fact]
        public void CalculateTaxForEmployeeThatEarnNotMoreThanMinimumWage30K_ShouldReturnCorrectPAYE()
        {
            //Arrang
            var grossIncome = 360000m;
            var expectedPaye = 3600m; //Note that employees who earn not more than the national minimum wage(currently
                                      //NGN 30,000) are no longer liable to tax or deduction of monthly pay-as-you-earn
                                      //(PAYE).

            //Act
            var paye = _taxService.CalculateTax(grossIncome);

            //Assert
            Assert.Equal(expectedPaye, paye);
        }
    }
}
