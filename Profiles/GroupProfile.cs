using AutoMapper;
using identity_service.Models;
using identity_service.DTOs;

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