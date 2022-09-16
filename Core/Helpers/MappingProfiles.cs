using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;


namespace Infrastructure.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Ger3ahName, Ger3ahOutputDto>()
            .ForMember(x=>x.Name , o=>o.MapFrom(g => g.User.NameAR));
        }
    }
}