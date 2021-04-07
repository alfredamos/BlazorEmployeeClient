using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Shared.Models
{
    public class Abode
    {
        public int AbodeID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public bool IsHomeAddress { get; set; } = false;
        public bool IsPostalAddress { get; set; } = false;
        public bool IsNextOfKinAddress { get; set; } = false;

        public int StaffID { get; set; }
        public Staff Staff { get; set; }

    }
}
