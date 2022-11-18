using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Employee
    {
        public string Id { get; set; }  
        public string StaffNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime DateJoin { get; set; }
        public decimal AnnualSalary { get; set; }



    }
}
