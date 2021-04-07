using AutoMapper;
using BlazorEmployeeClient.Client.Contracts;
using BlazorEmployeeClient.Client.Shared.UI;
using BlazorEmployeeClient.Client.ViewModels;
using BlazorEmployeeClient.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Pages.Employees
{
    public class EmployeeDetailBase : ComponentBase
    {        
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }      

        public EmployeeView Employee { get; set; } = new EmployeeView();

        public Employee EmployeeDB { get; set; } = new Employee();

        public AddressView Address { get; set; } = new AddressView();

        public List<HeadCounter> HeadCounts { get; set; } = new List<HeadCounter>();

        public Dept SearchDept { get; set; }
        
        protected ConfirmDelete DeleteConfirmation { get; set; }

        protected async override Task OnInitializedAsync()
        {            
            EmployeeDB = await EmployeeService.GetById(Id);

            SearchDept = EmployeeDB.Department;

            HeadCounts = (await EmployeeService.Searcher(SearchDept)).ToList();

            Mapper.Map(EmployeeDB, Employee);
          
        }

        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
        }

        protected async Task DeleteEmployee(bool deleteConfirmed)
        {
            Mapper.Map(Employee, EmployeeDB);

            if (deleteConfirmed)
            {
                await EmployeeService.DeleteEntity(Id);
            }

            NavigationManager.NavigateTo("employeeList");
        }

        protected void EditEmployee()
        {
            NavigationManager.NavigateTo($"/editEmployee/{Id}");
        }
   

        protected void BackToList()
        {
            NavigationManager.NavigateTo("employeeList");
        }
    }
}
