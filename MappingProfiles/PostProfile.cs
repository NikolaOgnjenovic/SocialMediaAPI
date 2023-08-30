using AutoMapper;
using SocialConnectAPI.DTOs.Posts.Get.Response;
using SocialConnectAPI.DTOs.Posts.Patch.Response;
using SocialConnectAPI.DTOs.Posts.Post.Request;
using SocialConnectAPI.DTOs.Posts.Post.Response;
using SocialConnectAPI.DTOs.Posts.Put.Request;
using SocialConnectAPI.DTOs.Posts.Put.Response;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.MappingProfiles;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<Post, GetPostResponse>();
        CreateMap<Post, PostPostResponse>();
        CreateMap<Post, PutPostResponse>();
        CreateMap<Post, PatchPostResponse>();

        // Mark post as active when posting without a status
        CreateMap<PostPostRequest, Post>().ForMember(post => post.Status,  opt => opt.MapFrom(src => PostStatus.Active));
        CreateMap<PutPostRequest, Post>();
    }
}