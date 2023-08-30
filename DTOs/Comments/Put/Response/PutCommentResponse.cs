using SocialConnectAPI.DTOs.Hateoas;

namespace SocialConnectAPI.DTOs.Comments.Put.Response;

public class PutCommentResponse : LinkCollection
{
    /// <summary>
    /// Gets or sets the unique identifier of the comment.
    /// </summary>
    public int Id { get; set; }

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