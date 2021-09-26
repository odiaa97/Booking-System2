using AutoMapper;
using DAL.DTOs;
using DAL.Entities;


namespace DAL.Services
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<LoginDto, AppUser>();

        }
    }
}
