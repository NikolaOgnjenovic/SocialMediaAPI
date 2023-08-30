using SocialConnectAPI.DTOs.Hateoas;

namespace SocialConnectAPI.DTOs.Comments.Post.Request;

public class PostCommentRequest
{
    /// <summary>
    /// Gets or sets the ID of the author who wrote the comment.
    /// </summary>
    public int AuthorId { get; set; }
    
    /// <summary>
    /// Gets or sets the content of the comment.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the number of likes the comment has received.
    /// </summary>
    public int LikeCount { get; set; }
}