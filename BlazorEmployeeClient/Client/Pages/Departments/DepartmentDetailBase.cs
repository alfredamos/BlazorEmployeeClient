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

namespace BlazorEmployeeClient.Client.Pages.Departments
{
    public class DepartmentDetailBase : ComponentBase
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

        protected ConfirmDelete DeleteConfirmation { get; set; }


        protected async override Task OnInitializedAsync()
        {
            DepartnentDB = await DepartmentService.GetById(Id);

            Mapper.Map(DepartnentDB, Department);
        }

        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
        }

        protected async Task DeleteDepartment(bool deleteConfirmed)
        {
            Mapper.Map(Department, DepartnentDB);

            if (deleteConfirmed)
            {
                await DepartmentService.DeleteEntity(Id);
            }

            NavigationManager.NavigateTo("departmentList");
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("departmentList");
        }

    }
}
