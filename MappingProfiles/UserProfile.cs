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

        // Mark user as active when usering without a status
        // TODO: Initialise FollowingUserIds, LikedPostIds, and LikedCommentIds properties
        CreateMap<PostUserRequest, User>().ForMember(user => user.Status,  opt => opt.MapFrom(src => UserStatus.Active));
        CreateMap<PutUserRequest, User>();
    }
}