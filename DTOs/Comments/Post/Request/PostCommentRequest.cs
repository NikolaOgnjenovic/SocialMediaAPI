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
    /// The ID of the post that the comment is on.
    /// </summary>
    public int PostId { get; set; }
}