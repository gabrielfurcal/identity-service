using AutoMapper;
using identity_service.Models;

namespace identity_service.Profiles
{
    public class UserGroupProfile : Profile
    {
        public UserGroupProfile()
        {
            CreateMap<UserGroup, UserGroupDTO>();
            CreateMap<UserGroupDTO, UserGroup>();
        }
    }
}