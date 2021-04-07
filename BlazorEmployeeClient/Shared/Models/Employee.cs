using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Shared.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }       
        public string PhotoPath { get; set; }
        public Dept Department { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<Address> Addresses { get; set; }

        //public int DepartmentRefID { get; set; }
        //public Department Department { get; set; }
        
    }
}
