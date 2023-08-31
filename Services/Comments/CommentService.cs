using AutoMapper;
using SocialConnectAPI.DataAccess.CommentLike;
using SocialConnectAPI.DataAccess.Comments;
using SocialConnectAPI.DTOs.Comments.Get.Response;
using SocialConnectAPI.DTOs.Comments.Patch.Request;
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
    private readonly ICommentLikeRepository _commentLikeRepository;

    public CommentService(ICommentRepository commentRepository, IMapper mapper, ICommentLikeRepository commentLikeRepository)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
        _commentLikeRepository = commentLikeRepository;
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
    
    public GetCommentResponse GetActiveCommentById(int commentId)
    {
        var comment = _commentRepository.GetActiveCommentById(commentId);

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
    
    public List<GetCommentResponse> GetActiveCommentsByUserId(int userId)
    {
        return _mapper.Map<List<GetCommentResponse>>(_commentRepository.GetActiveCommentsByUserId(userId));
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
            throw new CommentNotFoundException("Comment with id " + putCommentRequest.Id + " not found.");
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

    public PatchCommentResponse LikeComment(int commentId, LikeCommentRequest likeCommentRequest)
    {
        if (_commentLikeRepository.CommentIsLiked(commentId,likeCommentRequest.UserId))
        {
            throw new CommentLikedException("Comment with id " + commentId + " is already liked by user with id " +
                                            likeCommentRequest.UserId);
        }
        
        var likedComment = _commentRepository.LikeComment(commentId);

        if (likedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return _mapper.Map<PatchCommentResponse>(likedComment);
    }

    public PatchCommentResponse DislikeComment(int commentId, LikeCommentRequest likeCommentRequest)
    {
        if (!_commentLikeRepository.CommentIsLiked(commentId, likeCommentRequest.UserId))
        {
            throw new CommentLikedException("Comment with id " + commentId + " is not liked by user with id " +
                                            likeCommentRequest.UserId);
        }
        
        var dislikedComment = _commentRepository.DislikeComment(commentId);

        if (dislikedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return _mapper.Map<PatchCommentResponse>(dislikedComment);
    }

    public void SetInactive(int userId)
    {
        _commentRepository.SetInactive(userId);
    }
    
    public void SetActive(int userId)
    {
        _commentRepository.SetActive(userId);
    }

    public void ArchiveByPostId(int postId)
    {
        _commentRepository.ArchiveByPostId(postId);
    }
}