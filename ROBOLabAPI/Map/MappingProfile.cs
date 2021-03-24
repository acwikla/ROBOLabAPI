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
            CreateMap<UserRegisterDTO, User>();
            CreateMap<User, UserRegisterDTO>();

            CreateMap<UserToViewDTO, User>();
            CreateMap<User, UserToViewDTO>();

            CreateMap<UserToLoginDTO, User>();
            CreateMap<User, UserToLoginDTO>();

            //Device:
            CreateMap<DeviceDTO, Device>();
            CreateMap<Device, DeviceDTO>();

            CreateMap<DeviceToViewDTO, Device>();
            CreateMap<Device, DeviceToViewDTO>();
        }
    }
}
