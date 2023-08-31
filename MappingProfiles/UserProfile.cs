using AutoMapper;
using SocialConnectAPI.DTOs.Users.Get.Response;
using SocialConnectAPI.DTOs.Users.Patch.Response;
using SocialConnectAPI.DTOs.Users.Post.Request;
using SocialConnectAPI.DTOs.Users.Post.Response;
using SocialConnectAPI.DTOs.Users.Put.Request;
using SocialConnectAPI.DTOs.Users.Put.Response;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, GetUserResponse>();
        CreateMap<User, PostUserResponse>();
        CreateMap<User, PutUserResponse>();
        CreateMap<User, PatchUserResponse>();

        // Mark user as active & initialise followers, post & comment likes when mapping from a post request to a user
        CreateMap<PostUserRequest, User>().ForMember(user => user.Status,  opt => opt.MapFrom(src => UserStatus.Active));
        CreateMap<PostUserRequest, User>().ForMember(user => user.Followers,  opt => opt.MapFrom(src => new List<User>()));
        CreateMap<PostUserRequest, User>().ForMember(user => user.Following,  opt => opt.MapFrom(src => new List<User>()));
        CreateMap<PostUserRequest, User>().ForMember(user => user.PostLikes,  opt => opt.MapFrom(src => new List<PostLike>()));
        CreateMap<PostUserRequest, User>().ForMember(user => user.CommentLikes,  opt => opt.MapFrom(src => new List<CommentLike>()));

        CreateMap<PutUserRequest, User>();
    }
}