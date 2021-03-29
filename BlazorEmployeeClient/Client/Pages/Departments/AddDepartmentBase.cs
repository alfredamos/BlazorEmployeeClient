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
    public class AddDepartmentBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public DepartmentView Department { get; set; } = new DepartmentView();

        public Department DepartnentDB { get; set; } = new Department();

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected async Task CreateDepartment()
        {
            Mapper.Map(Department, DepartnentDB);

            var department = await DepartmentService.AddEntity(DepartnentDB);

            if (department != null)
            {
                NavigationManager.NavigateTo("departmentList");
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("departmentList");
        }
    }
}
