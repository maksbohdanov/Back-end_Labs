using AutoMapper;
using CostAccounting.Entities;
using System.Linq;
using WebApi.Entities.DTOs;

namespace WebApi
{
    public class AuthomapperProfile : Profile
    {
        public AuthomapperProfile()
        {
            CreateMap<Record, RecordDto>()
                .ReverseMap();

            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Categories, u => u.MapFrom(x => x.Categories.Select(c => c.Id)))
                .ForMember(dto => dto.Records, u => u.MapFrom(x => x.Records.Select(r => r.Id)))
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ReverseMap();
        }
    }
}
