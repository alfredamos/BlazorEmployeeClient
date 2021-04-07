using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.ViewModels
{
    public class EmployeeView
    {
        public int EmployeeID { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        public Dept Department { get; set; }
        public Gender Gender { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        public List<AddressView> Addresses { get; set; }
        
    }
}
