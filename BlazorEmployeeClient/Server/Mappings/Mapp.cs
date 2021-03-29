﻿using AutoMapper;
using BlazorEmployeeClient.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorEmployeeClient.Server.Mappings
{
    public class Mapp : Profile
    {
        public Mapp()
        {
            CreateMap<Department, Department>();
            CreateMap<Department, Department>();
            CreateMap<Employee, Employee>();
        }
    }
}
