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
    public class ListStaffBase : ComponentBase
    {
        [Inject]
        public IStaffService StaffService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<StaffView> Staffs { get; set; } = new List<StaffView>();

        public List<Staff> StaffsDB { get; set; } = new List<Staff>();

        public List<HeadCount> HeadCounts { get; set; } = new List<HeadCount>();

        public string SearchDept { get; set; }

        protected async override Task OnInitializedAsync()
        {
            StaffsDB = (await StaffService.GetAll()).ToList();

            Mapper.Map(StaffsDB, Staffs);

            HeadCounts = (await StaffService.Searcher()).ToList();

        }

        protected async Task HandleSearch(string searchKey)
        {
            StaffsDB = (!string.IsNullOrWhiteSpace(searchKey)) ? (await StaffService.Search(searchKey)).ToList() :
                                                                                      (await StaffService.GetAll()).ToList();
            Mapper.Map(StaffsDB, Staffs);
        }
    }
}
