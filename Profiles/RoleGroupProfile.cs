using AutoMapper;
using identity_service.Models;

namespace identity_service.Profiles
{
    public class RoleGroupProfile : Profile
    {
        public RoleGroupProfile()
        {
            CreateMap<RoleGroup, RoleGroupDTO>();
            CreateMap<RoleGroupDTO, RoleGroup>();
        }
    }
}