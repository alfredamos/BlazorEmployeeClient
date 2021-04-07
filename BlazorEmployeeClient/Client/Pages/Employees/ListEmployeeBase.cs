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
    public class ListEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<EmployeeView> Employees { get; set; } = new List<EmployeeView>();

        public List<Employee> EmployeesDB { get; set; } = new List<Employee>();

        public List<HeadCounter> HeadCounts { get; set; } = new List<HeadCounter>();

        public string SearchDept { get; set; }

        protected async override Task OnInitializedAsync()
        {
            EmployeesDB = (await EmployeeService.GetAll()).ToList();

            Mapper.Map(EmployeesDB, Employees);

            HeadCounts = (await EmployeeService.Searcher()).ToList();

        }

        protected async Task HandleSearch(string searchKey)
        {
            EmployeesDB = (!string.IsNullOrWhiteSpace(searchKey.ToString().Trim())) ? (await EmployeeService.Search(searchKey)).ToList() :
                                                                   (await EmployeeService.GetAll()).ToList();

            Mapper.Map(EmployeesDB, Employees);
        }


    }
}
