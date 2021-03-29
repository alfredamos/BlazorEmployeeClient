using AutoMapper;
using BlazorEmployeeClient.Client.Contracts;
using BlazorEmployeeClient.Client.ViewModels;
using BlazorEmployeeClient.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Pages.Departments
{
    public class DepartmentListBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<DepartmentView> Departments { get; set; } = new List<DepartmentView>();

        public List<Department> DepartnentsDB { get; set; } = new List<Department>();

        protected async override Task OnInitializedAsync()
        {
            DepartnentsDB = (await DepartmentService.GetAll()).ToList();

            Mapper.Map(DepartnentsDB, Departments);
        }

        
       
    }
}
