using AutoMapper;
using SocialConnectAPI.DataAccess.Comments;
using SocialConnectAPI.DTOs.Comments.Get.Response;
using SocialConnectAPI.DTOs.Comments.Patch.Response;
using SocialConnectAPI.DTOs.Comments.Post.Request;
using SocialConnectAPI.DTOs.Comments.Post.Response;
using SocialConnectAPI.DTOs.Comments.Put.Request;
using SocialConnectAPI.DTOs.Comments.Put.Response;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.Services.Comments;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }
    
    public GetCommentResponse GetCommentById(int commentId)
    {
        var comment = _commentRepository.GetCommentById(commentId);

        if (comment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return _mapper.Map<GetCommentResponse>(comment);
    }

    public List<GetCommentResponse> GetCommentsByUserId(int userId)
    {
        return _mapper.Map<List<GetCommentResponse>>(_commentRepository.GetCommentsByUserId(userId));
    }

    public PostCommentResponse CreateComment(PostCommentRequest postCommentRequest)
    {
        return _mapper.Map<PostCommentResponse>(_commentRepository.CreateComment(_mapper.Map<Comment>(postCommentRequest)));
    }

    public PutCommentResponse UpdateComment(PutCommentRequest putCommentRequest)
    {
        var updatedComment = _commentRepository.UpdateComment(_mapper.Map<Comment>(putCommentRequest));

        if (updatedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + updatedComment.Id + " not found.");
        }

        return _mapper.Map<PutCommentResponse>(updatedComment);
    }

    public void DeleteComment(int commentId)
    {
        var deletedComment = _commentRepository.DeleteComment(commentId);

        if (deletedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }
    }

    public PatchCommentResponse LikeComment(int commentId)
    {
        var likedComment = _commentRepository.LikeComment(commentId);

        if (likedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return _mapper.Map<PatchCommentResponse>(likedComment);
    }

    public PatchCommentResponse DislikeComment(int commentId)
    {
        var dislikedComment = _commentRepository.DislikeComment(commentId);

        if (dislikedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return _mapper.Map<PatchCommentResponse>(dislikedComment);
    }
}