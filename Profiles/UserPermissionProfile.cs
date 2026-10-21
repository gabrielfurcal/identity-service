using AutoMapper;
using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Profiles
{
    public class UserPermissionProfile : Profile
    {
        public UserPermissionProfile()
        {
            CreateMap<UserPermission, UserPermissionDTO>();
            CreateMap<UserPermissionDTO, UserPermission>();
        }
    }
}