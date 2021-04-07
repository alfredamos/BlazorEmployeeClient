using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Shared.Models
{
    public class HeadCount
    {
        public Department Department { get; set; }
        public Employee Employee { get; set; }
        public int Count { get; set; }
    }
}
