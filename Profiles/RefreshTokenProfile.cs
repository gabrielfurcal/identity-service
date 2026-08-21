using AutoMapper;
using identity_service.Models;
using identity_service.DTOs;

namespace identity_service.Profiles
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDTO>();
            CreateMap<RefreshTokenDTO, RefreshToken>();
        }
    }
}