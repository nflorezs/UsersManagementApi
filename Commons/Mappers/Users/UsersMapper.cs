using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dto;
using Dto.ApiRequests;
using Entities;

namespace Mappers.Users
{
    public class UsersMapper : Profile
    {
        public UsersMapper()
        {
            CreateMap<RootDto, Root>()
                .ForMember(x => x.page, map => map.MapFrom(dto => dto.page))
                .ForMember(x => x.per_page, map => map.MapFrom(dto => dto.per_page))
                .ForMember(x => x.total, map => map.MapFrom(dto => dto.total))
                .ForMember(x => x.total_pages, map => map.MapFrom(dto => dto.total_pages))
                .ForMember(x => x.data, map => map.MapFrom(dto => dto.data))
                .ForMember(x => x.support, map => map.MapFrom(dto => dto.support))
                .ForMember(x => x.updated, map => map.MapFrom(dto => dto.updated))
                .ForMember(x => x.actual_page, map => map.MapFrom(dto => dto.actual_page))
                .ReverseMap();

            CreateMap<DatumDto, Datum>()
                .ForMember(x => x.id, map => map.MapFrom(dto => dto.id))
                .ForMember(x => x.email, map => map.MapFrom(dto => dto.email))
                .ForMember(x => x.first_name, map => map.MapFrom(dto => dto.first_name))
                .ForMember(x => x.last_name, map => map.MapFrom(dto => dto.last_name))
                .ForMember(x => x.avatar, map => map.MapFrom(dto => dto.avatar))
                .ReverseMap();
            
            CreateMap<DatumLoginDto, DatumLogin>()
                .ForMember(x => x.id, map => map.MapFrom(dto => dto.id))
                .ForMember(x => x.email, map => map.MapFrom(dto => dto.email))
                .ForMember(x => x.first_name, map => map.MapFrom(dto => dto.first_name))
                .ForMember(x => x.last_name, map => map.MapFrom(dto => dto.last_name))
                .ForMember(x => x.avatar, map => map.MapFrom(dto => dto.avatar))
                .ForMember(x => x.Username, map => map.MapFrom(dto => dto.Username))
                .ForMember(x => x.Password, map => map.MapFrom(dto => dto.Password))
                .ReverseMap();

            CreateMap<SupportDto, Support>()
                .ForMember(x => x.url, map => map.MapFrom(dto => dto.url))
                .ForMember(x => x.text, map => map.MapFrom(dto => dto.text))
                .ReverseMap();
        }
    }
}
