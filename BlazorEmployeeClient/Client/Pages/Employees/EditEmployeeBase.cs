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
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public List<DepartmentView> Departments { get; set; } = new List<DepartmentView>();

        public List<Department> DepartnentsDB { get; set; } = new List<Department>();

        public EmployeeView Employee { get; set; } = new EmployeeView();

        public Employee EmployeeDB { get; set; } = new Employee();

        public AddressView Address { get; set; } = new AddressView();

        public AddressView AddressOld { get; set; } = new AddressView();

        public Address AddressDB { get; set; } = new Address();

        public List<AddressView> Addresses { get; set; } = new List<AddressView>();

        public bool ShowAddress { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            DepartnentsDB = (await DepartmentService.GetAll()).ToList();

            EmployeeDB = await EmployeeService.GetById(Id);            

            Mapper.Map(DepartnentsDB, Departments);
            Mapper.Map(EmployeeDB, Employee);

            Addresses = Employee.Addresses;
            Address = Addresses.FirstOrDefault(x => x.EmployeeID == Id);
            AddressOld = Address;            

        }

        protected void InsertAddress()
        {
            ShowAddress = true;
        }

        protected async Task UpdateEmployee()
        {            
            Addresses[Addresses.FindIndex(ind => ind.Equals(AddressOld))] = Address;

            Employee.Addresses = Addresses;

            Mapper.Map(Employee, EmployeeDB);

            var employee = await EmployeeService.UpdateEntity(EmployeeDB);

            //Addresses.Clear();

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
