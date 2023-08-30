using SocialConnectAPI.DTOs.Hateoas;

namespace SocialConnectAPI.DTOs.Comments.Post.Request;

public class PostCommentRequest
{
    /// <summary>
    /// The ID of the author who wrote the comment.
    /// </summary>
    public int AuthorId { get; set; }
    
    /// <summary>
    /// The content of the comment.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The number of likes the comment has received.
    /// </summary>
    public int LikeCount { get; set; }
}