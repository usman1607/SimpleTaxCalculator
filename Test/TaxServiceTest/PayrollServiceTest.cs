using Moq;
using Payroll.Models;
using Payroll.Repositories;
using Payroll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TaxServiceTest
{
    public class PayrollServiceTest
    {
        private readonly TaxService _taxService;
        private readonly PayrollService _payrollService;
        private readonly Mock<ITaxService> _taxServiceMock = new Mock<ITaxService>();
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock = new Mock<IEmployeeRepository>();

        public PayrollServiceTest()
        {
            _taxService = new TaxService();
            _payrollService = new PayrollService(_employeeRepositoryMock.Object, _taxServiceMock.Object);
        }

        [Fact]
        public void CalculatePayePerAnnumForInvalidEmployeeShouldReturnNotFound()
        {
            //Arrange
            var email = "fake@gmail.com";
            var expected = $"Employee with email {email} not found";

            //Act
            var response = _payrollService.CalculateEmployeePayePerAnnum(email);

            //Assert            
            Assert.Equal(expected, response.Message);
        }

        [Fact]
        public void CalculatePayePerAnnumForEmployeeShouldReturnCorrectPAYE()
        {
            //Arrange
            var email = GetFakeEmployee()[0].Email;
            var salary = GetFakeEmployee()[0].AnnualSalary;  //4,000,000
            var expected = 464240m;

            _taxServiceMock.Setup(x => x.CalculateTax(salary)).Returns(_taxService.CalculateTax(salary));
            _employeeRepositoryMock.Setup(x => x.GetEmployee(email)).Returns(GetFakeEmployee()[0]);            

            //Act
            var response = _payrollService.CalculateEmployeePayePerAnnum(email);

            //Assert            
            Assert.Equal(expected, response.PAYE);
        }

        [Fact]
        public void CalculatePayePerAnnumForEmployeeAbove20M_ShouldReturnCorrectPAYE()
        {
            //Arrange
            var email = GetFakeEmployee()[2].Email;
            var salary = GetFakeEmployee()[2].AnnualSalary;  //30,000,000
            var expected = 5019200m;

            /*
             * 
             * pension = 8/100 * 30M = 2,400,000
                Higher of NGN                 = 200,000 or 1% of Gross Income(If income > 20M) = 300,000
                Consolidated relief allowance = 20% of ( Gross income - pensio) = 20/100 * (30M - pension) = 5,520,000
                
                taxable income = 30M - (300,000 + 5,520,000 + 2,400,000) = 30M - 8,220,000 = 21,780,000

                tax             %
                First 300,000	7	21,000	 
                Next 300,000	11	33,000	 
                Next 500,000	15	75,000	 
                Next 500,000	19	95,000	 
                Next 1,600,000	21  336,000
    24% 0f (21,780,000 - 3,200,000) 4,459,200
               PAYE                 5,019,200
             * 
             */
            _taxServiceMock.Setup(x => x.CalculateTax(salary)).Returns(_taxService.CalculateTax(salary));
            _employeeRepositoryMock.Setup(x => x.GetEmployee(email)).Returns(GetFakeEmployee()[2]);

            //Act
            var response = _payrollService.CalculateEmployeePayePerAnnum(email);

            //Assert            
            Assert.Equal(expected, response.PAYE);
        }

        [Fact]
        public void CalculatePayePerMonthForEmployeeShouldReturnCorrectPAYE()
        {
            //Arrange
            var email = GetFakeEmployee()[0].Email;
            var salary = GetFakeEmployee()[0].AnnualSalary;  //4,000,000
            var expected = 38686.67m;

            _taxServiceMock.Setup(x => x.CalculateTax(salary)).Returns(_taxService.CalculateTax(salary));
            _employeeRepositoryMock.Setup(x => x.GetEmployee(email)).Returns(GetFakeEmployee()[0]);

            //Act
            var response = _payrollService.CalculateEmployeePayePerMonth(email);

            //Assert            
            Assert.Equal(expected, response.PAYE);
        }

        [Fact]
        public void CalculatePayePerMonthForEmployeeAbove20M_ShouldReturnCorrectPAYE()
        {
            //Arrange
            var email = GetFakeEmployee()[2].Email;
            var salary = GetFakeEmployee()[2].AnnualSalary;  //30,000,000
            var expected = 418266.67m;

            
            _taxServiceMock.Setup(x => x.CalculateTax(salary)).Returns(_taxService.CalculateTax(salary));
            _employeeRepositoryMock.Setup(x => x.GetEmployee(email)).Returns(GetFakeEmployee()[2]);

            //Act
            var response = _payrollService.CalculateEmployeePayePerMonth(email);

            //Assert            
            Assert.Equal(expected, response.PAYE);
        }

        [Fact]
        public void GetSalaryPerMonthForEmployeeShouldReturnCorrectSalary()
        {
            //Arrange
            var email = GetFakeEmployee()[0].Email;
            var salary = GetFakeEmployee()[0].AnnualSalary;  //4,000,000
            var expected = 294646.67m;

            _taxServiceMock.Setup(x => x.CalculateTax(salary)).Returns(_taxService.CalculateTax(salary));
            _employeeRepositoryMock.Setup(x => x.GetEmployee(email)).Returns(GetFakeEmployee()[0]);

            //Act
            var response = _payrollService.GetMonthlySalaryAfterTax(email);

            //Assert            
            Assert.Equal(expected, response.SalaryAfteTax);
        }

        [Fact]
        public void GetSalaryPerMonthForEmployeeAbove20M_ShouldReturnCorrectSalary()
        {
            //Arrange
            var email = GetFakeEmployee()[2].Email;
            var salary = GetFakeEmployee()[2].AnnualSalary;  //30,000,000
            var expected = 2081733.33m;


            _taxServiceMock.Setup(x => x.CalculateTax(salary)).Returns(_taxService.CalculateTax(salary));
            _employeeRepositoryMock.Setup(x => x.GetEmployee(email)).Returns(GetFakeEmployee()[2]);

            //Act
            var response = _payrollService.GetMonthlySalaryAfterTax(email);

            //Assert            
            Assert.Equal(expected, response.SalaryAfteTax);
        }

        public List<Employee> GetFakeEmployee()
        {
            return new List<Employee>
            {
                new Employee
                (
                    1,
                    "Ade",
                    "Gbenga",
                    "ABK",
                    "ade@gmail.com",
                    DateTime.Now,
                    4000000m
                ),

                new Employee
                (
                    2,
                    "Hellen",
                    "Paul",
                    "Lagos",
                    "paul@gmail.com",
                    DateTime.Now,
                    3000000m
                ),

                new Employee
                (
                    3,
                    "Big",
                    "Boss",
                    "Abuja",
                    "boss@gmail.com",
                    DateTime.Now,
                    30000000m
                ),

                new Employee
                (
                    4,
                    "Nana",
                    "John",
                    "Lagos",
                    "nana@gmail.com",
                    DateTime.Now,
                    1000000m
                ),
            };
        }
    }
}
