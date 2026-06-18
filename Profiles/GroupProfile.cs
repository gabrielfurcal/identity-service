using AutoMapper;
using identity_service.Models;

namespace identity_service.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDTO>();
            CreateMap<GroupDTO, Group>();
        }
    }
}