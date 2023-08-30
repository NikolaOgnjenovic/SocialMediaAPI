using AutoMapper;
using SocialConnectAPI.DTOs.Comments.Get.Response;
using SocialConnectAPI.DTOs.Comments.Patch.Response;
using SocialConnectAPI.DTOs.Comments.Post.Request;
using SocialConnectAPI.DTOs.Comments.Post.Response;
using SocialConnectAPI.DTOs.Comments.Put.Request;
using SocialConnectAPI.DTOs.Comments.Put.Response;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.MappingProfiles;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, GetCommentResponse>();
        CreateMap<Comment, PostCommentResponse>();
        CreateMap<Comment, PutCommentResponse>();
        CreateMap<Comment, PatchCommentResponse>();

        CreateMap<PostCommentRequest, Comment>();
        CreateMap<PutCommentRequest, Comment>();
    }
}