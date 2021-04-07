using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.ViewModels
{
    public class AddressView
    {
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public bool IsHomeAddress { get; set; } = false;
        public bool IsPostalAddress { get; set; } = false;
        public bool IsNextOfKinAddress { get; set; } = false;

        public int EmployeeID { get; set; }
        public EmployeeView Employee { get; set; }

    }
}
