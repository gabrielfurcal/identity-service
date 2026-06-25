using AutoMapper;
using identity_service.Models;
using identity_service.DTOs;
using Microsoft.AspNetCore.Identity;

namespace identity_service.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Password, act => act.MapFrom(src => new PasswordHasher<User>().HashPassword(src, src.PasswordHash)));
            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.PasswordHash, act => act.MapFrom(src => src.Password));
        }
    }
}