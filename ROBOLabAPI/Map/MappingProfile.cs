using AutoMapper;
using ROBOLab.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ROBOLab.Core.DTO;
namespace ROBOLabAPI.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects

            //User:
            CreateMap<UserRegisterDTO, User>().ReverseMap();
            CreateMap<UserToViewDTO, User>().ReverseMap();
            CreateMap<UserToLoginDTO, User>().ReverseMap();

            //Device:
            CreateMap<DeviceDTO, Device>().ReverseMap();
            CreateMap<DeviceToViewDTO, Device>().ReverseMap();

            CreateMap<Device, DeviceDTO>()
            .ForMember(dest => dest.DeviceName,
            opt => opt.MapFrom(src => src.Name)).ReverseMap();

            //DeviceType
            CreateMap<DeviceTypeDTO, DeviceType>().ReverseMap();

            CreateMap<DeviceType, DeviceTypeDTO>()
            .ForMember(dest => dest.DeviceTypeName,
            opt => opt.MapFrom(src => src.Name)).ReverseMap();

        }
    }
}
