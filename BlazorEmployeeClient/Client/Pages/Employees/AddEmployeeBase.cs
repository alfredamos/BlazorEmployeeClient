using AutoMapper;
using BlazorEmployeeClient.Client.Contracts;
using BlazorEmployeeClient.Client.ViewModels;
using BlazorEmployeeClient.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Pages.Employees
{
    public class AddEmployeeBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<DepartmentView> Departments { get; set; } = new List<DepartmentView>();

        public List<Department> DepartnentsDB { get; set; } = new List<Department>();

        public EmployeeView Employee { get; set; } = new EmployeeView();

        public Employee EmployeeDB { get; set; } = new Employee();

        public AddressView Address { get; set; } = new AddressView();

        public Address AddressDB { get; set; } = new Address();

        public List<AddressView> Addresses { get; set; } = new List<AddressView>();

        public bool ShowAddress { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            DepartnentsDB = (await DepartmentService.GetAll()).ToList();

            Mapper.Map(DepartnentsDB, Departments);
        }

        protected void InsertAddress()
        {
            ShowAddress = true;
        }

        protected async Task CreateEmployee()
        {
            Addresses.Add(Address);

            Employee.Addresses = Addresses;

            Mapper.Map(Employee, EmployeeDB);

            var employee = await EmployeeService.AddEntity(EmployeeDB);

            Addresses.Clear();

            if (employee != null)
            {
                NavigationManager.NavigateTo("employeeList");
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("employeeList");
        }
    }
}
