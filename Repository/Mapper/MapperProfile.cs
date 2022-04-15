using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Entities;

namespace Repository.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<UserFriendRequests, UserFriendRequestDto>().ReverseMap();
            CreateMap<UserFriends, UserFriendDto>().ReverseMap();
        }
    }
}
