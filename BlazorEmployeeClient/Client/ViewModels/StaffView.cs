using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.ViewModels
{
    public class StaffView
    {
        public int StaffID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<AbodeView> Addresses { get; set; }

        public int DepartmentID { get; set; }
        public DepartmentView Department { get; set; }
    }
}
