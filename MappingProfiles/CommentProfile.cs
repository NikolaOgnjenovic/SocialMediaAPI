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

        CreateMap<PostCommentRequest, Comment>().ForMember(comment => comment.Status,  opt => opt.MapFrom(src => CommentStatus.Active));
        CreateMap<PostCommentRequest, Comment>().ForMember(comment => comment.UsersWhoLiked,  opt => opt.MapFrom(src => new List<CommentLike>()));
        CreateMap<PostCommentRequest, Comment>().ForMember(comment => comment.LikeCount,  opt => opt.MapFrom(src => 0));
        CreateMap<PutCommentRequest, Comment>();
    }
}