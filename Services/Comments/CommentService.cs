using SocialConnectAPI.DataAccess.Comments;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.Services.Comments;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public Comment GetCommentById(int commentId)
    {
        var comment = _commentRepository.GetCommentById(commentId);

        if (comment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return comment;
    }

    public List<Comment> GetCommentsByUserId(int userId)
    {
        return _commentRepository.GetCommentsByUserId(userId);
    }

    public Comment CreateComment(Comment comment)
    {
        return _commentRepository.CreateComment(comment);
    }

    public Comment UpdateComment(Comment comment)
    {
        var updatedComment = _commentRepository.UpdateComment(comment);

        if (updatedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + comment.Id + " not found.");
        }

        return updatedComment;
    }

    public Comment DeleteComment(int commentId)
    {
        var deletedComment = _commentRepository.DeleteComment(commentId);

        if (deletedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return deletedComment;
    }

    // public Comment ArchiveComment(int commentId)
    // {
    //     var archivedComment = _commentRepository.ArchiveComment(commentId);
    //
    //     if (archivedComment == null)
    //     {
    //         throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
    //     }
    //
    //     return archivedComment;
    // }

    public Comment LikeComment(int commentId)
    {
        var likedComment = _commentRepository.LikeComment(commentId);

        if (likedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return likedComment;
    }

    public Comment DislikeComment(int commentId)
    {
        var dislikedComment = _commentRepository.DislikeComment(commentId);

        if (dislikedComment == null)
        {
            throw new CommentNotFoundException("Comment with id " + commentId + " not found.");
        }

        return dislikedComment;
    }
}