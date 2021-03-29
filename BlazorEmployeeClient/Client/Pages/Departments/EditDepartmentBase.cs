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
    public class EditDepartmentBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public DepartmentView Department { get; set; } = new DepartmentView();

        public Department DepartnentDB { get; set; } = new Department();

        protected async override Task OnInitializedAsync()
        {
            DepartnentDB = await DepartmentService.GetById(Id);

            Mapper.Map(DepartnentDB, Department);
        }

        protected async Task UpdateDepartment()
        {
            Mapper.Map(Department, DepartnentDB);

            var department = await DepartmentService.UpdateEntity(DepartnentDB);

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
