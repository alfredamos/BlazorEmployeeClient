using AutoMapper;
using BlazorEmployeeClient.Client.Contracts;
using BlazorEmployeeClient.Client.ViewModels;
using BlazorEmployeeClient.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Pages.Staffs
{
    public class EditStaffBase : ComponentBase
    {
        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public IStaffService StaffService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public int Id { get; set; }

        public List<DepartmentView> Departments { get; set; } = new List<DepartmentView>();

        public List<Department> DepartmentsDB { get; set; } = new List<Department>();

        public StaffView Staff { get; set; } = new StaffView();

        public Staff StaffDB { get; set; } = new Staff();

        public AbodeView Address { get; set; } = new AbodeView();

        public AbodeView AddressOld { get; set; } = new AbodeView();

        public Abode AddressDB { get; set; } = new Abode();

        public List<AbodeView> Addresses { get; set; } = new List<AbodeView>();

        public bool ShowAddress { get; set; } = false;

        protected async override Task OnInitializedAsync()
        {
            DepartmentsDB = (await DepartmentService.GetAll()).ToList();

            StaffDB = await StaffService.GetById(Id);

            Mapper.Map(DepartmentsDB, Departments);
            Mapper.Map(StaffDB, Staff);

            Addresses = Staff.Addresses;            
            Address = Addresses.FirstOrDefault(x => x.StaffID == Id);
            AddressOld = Address;

        }

        protected void InsertAddress()
        {
            ShowAddress = true;
        }

        protected async Task UpdateStaff()
        {
            Addresses[Addresses.FindIndex(ind => ind.Equals(AddressOld))] = Address;

            Staff.Addresses = Addresses;

            Mapper.Map(Staff, StaffDB);

            var employee = await StaffService.UpdateEntity(StaffDB);

            //Addresses.Clear();

            if (employee != null)
            {
                NavigationManager.NavigateTo("staffList");
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("staffList");
        }
    }
}
