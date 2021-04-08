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
            CreateMap<User, UserRegisterDTO>().ReverseMap();
            CreateMap<User, UserToViewDTO>().ReverseMap();
            CreateMap<User, UserToLoginDTO>().ReverseMap();

            //Device:
            CreateMap<Device, DeviceAddDTO> ().ReverseMap();
            CreateMap<Device, DeviceToViewDTO>().ReverseMap();

            CreateMap<Device, DeviceAddDTO>()
            .ForMember(dest => dest.DeviceName,
            opt => opt.MapFrom(src => src.Name)).ReverseMap();

            //DeviceType
            CreateMap<DeviceType, DeviceTypeDTO>().ReverseMap();

            CreateMap<DeviceType, DeviceTypeDTO>()
            .ForMember(dest => dest.DeviceTypeName,
            opt => opt.MapFrom(src => src.Name)).ReverseMap();

            //DeviceJob:
            CreateMap<DeviceJob, DeviceJobAddDTO>().ReverseMap();
            CreateMap<DeviceJobAddDTO, DeviceJobToViewDTO>().ReverseMap();
            CreateMap<DeviceJob, DeviceJobToViewDTO>().ReverseMap();

            //Job:
            CreateMap<Job, JobDTO>().ReverseMap();

            //Property:
            CreateMap<Property, PropertyAddDTO>().ReverseMap();
            CreateMap<Property, PropertyToViewDTO>().ReverseMap();
        }
    }
}
