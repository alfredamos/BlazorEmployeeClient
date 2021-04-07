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

namespace BlazorEmployeeClient.Client.Pages.Staffs
{
    public class DeleteStaffBase : ComponentBase
    {
        [Inject]
        public IStaffService StaffService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public StaffView Staff { get; set; } = new StaffView();

        public Staff StaffDB { get; set; } = new Staff();

        public AddressView Address { get; set; } = new AddressView();

        protected ConfirmDelete DeleteConfirmation { get; set; }

        protected async override Task OnInitializedAsync()
        {
            StaffDB = await StaffService.GetById(Id);

            Mapper.Map(StaffDB, Staff);

        }

        protected void DeleteClick()
        {
            DeleteConfirmation.Show();
        }

        protected async Task DeleteStaff(bool deleteConfirmed)
        {
            Mapper.Map(Staff, StaffDB);

            if (deleteConfirmed)
            {
                await StaffService.DeleteEntity(Id);
            }

            NavigationManager.NavigateTo("staffList");
        }

        protected void EditStaff()
        {
            NavigationManager.NavigateTo($"/editStaff/{Id}");
        }


        protected void BackToList()
        {
            NavigationManager.NavigateTo("staffList");
        }
    }
}
