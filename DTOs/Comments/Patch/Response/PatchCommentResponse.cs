using SocialConnectAPI.DTOs.Hateoas;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.DTOs.Comments.Patch.Response;

/// <summary>
/// Represents the response after patching (updating) a comment.
/// </summary>
public class PatchCommentResponse : LinkCollection
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
    
    public List<SimpleCommentLike> UsersWhoLiked { get; set; }
    
    public CommentStatus Status { get; set; }
}
