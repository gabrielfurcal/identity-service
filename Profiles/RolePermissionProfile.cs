using AutoMapper;
using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Profiles
{
    public class RolePermissionProfile : Profile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, RolePermissionDTO>();
            CreateMap<RolePermissionDTO, RolePermission>();
        }
    }
}