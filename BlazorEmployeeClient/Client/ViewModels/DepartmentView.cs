using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.ViewModels
{
    public class DepartmentView
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public EmployeeView Employee { get; set; }
    }
}
