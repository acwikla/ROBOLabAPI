using AutoMapper;
using ROBOLab.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ROBOLab.Core.DTO;
namespace ROBOLab.API.Map
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects

            //TODO: czy wszedzie potzrebne jest ReverseMap ?


            //User:
            CreateMap<User, RegisterUserDTO>().ReverseMap();
            CreateMap<User, ViewUserDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();

            //Device
            CreateMap<Device, ViewDeviceDTO>().ReverseMap();
            CreateMap<Device, AddDeviceDTO>()
                .ForMember(dest => dest.DeviceName, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            //DeviceType
            CreateMap<DeviceType, DeviceTypeDTO>().ReverseMap();
            CreateMap<DeviceType, DeviceTypeDTO>().ReverseMap();

            //DeviceJob:
            CreateMap<DeviceJob, AddDeviceJobDTO>().ReverseMap();
            CreateMap<AddDeviceJobDTO, ViewDeviceJobDTO>().ReverseMap();
            CreateMap<DeviceJob, ViewDeviceJobDTO>().ReverseMap();

            //Job:
            CreateMap<Job, JobDTO>()
                .ForMember(dest => dest.DeviceTypeName, opt => opt.MapFrom(src => src.DeviceType.Name))
                .ReverseMap();
            

            //Property:
            CreateMap<Property, AddPropertyDTO>().ReverseMap();
            CreateMap<Property, ViewPropertyDTO>().ReverseMap();
        }
    }
}