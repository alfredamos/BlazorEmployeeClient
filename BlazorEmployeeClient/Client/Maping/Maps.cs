using AutoMapper;
using BlazorEmployeeClient.Client.ViewModels;
using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Client.Maping
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<Address, AddressView>().ReverseMap();
            CreateMap<Department, DepartmentView>().ReverseMap();
            CreateMap<Employee, EmployeeView>().ReverseMap();
        }
    }
}
