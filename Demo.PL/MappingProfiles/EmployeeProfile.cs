using AutoMapper;
using Demo.DataAccessLayer.Entities;
using Demo.PL.ViewModels;
using System.Collections;
using System.Collections.Generic;

namespace Demo.PL.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
