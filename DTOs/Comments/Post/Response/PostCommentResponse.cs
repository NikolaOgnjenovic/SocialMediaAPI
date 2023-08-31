using SocialConnectAPI.DTOs.Hateoas;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.DTOs.Comments.Post.Response;

public class PostCommentResponse : LinkCollection
{
    /// <summary>
    /// The unique identifier of the comment.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The ID of the author who wrote the comment.
    /// </summary>
    public int AuthorId { get; set; }
    
    /// <summary>
    /// The ID of the post that the comment is on.
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// The content of the comment.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The number of likes the comment has received.
    /// </summary>
    public int LikeCount { get; set; }
    
    public List<SimpleCommentLike> UsersWhoLiked { get; set; }
    
    public CommentStatus Status { get; set; }
}