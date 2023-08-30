using AutoMapper;
using SocialConnectAPI.DTOs.Posts.Get.Response;
using SocialConnectAPI.DTOs.Posts.Patch;
using SocialConnectAPI.DTOs.Posts.Post.Request.PostPostRequest;
using SocialConnectAPI.DTOs.Posts.Post.Response.PostPostResponse;
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

        CreateMap<PostPostRequest, Post>();
        CreateMap<PutPostRequest, Post>();
    }
}