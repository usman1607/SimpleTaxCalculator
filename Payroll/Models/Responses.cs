using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class EmployeeResponses
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public Employee? Data { get; set; }
    }

    public class EmployeesResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<Employee> Data { get; set; } = new List<Employee>();
    }

    public class PAYEResopnse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public decimal? PAYE { get; set; }
    }

    public class SalaryResponse
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string FullName { get; set; }
        public string StaffNo { get; set; }
        public string Email { get; set; }
        public decimal SalaryAfteTax { get; set; }
    }
}
