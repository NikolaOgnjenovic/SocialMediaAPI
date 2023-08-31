using System.ComponentModel.DataAnnotations;

namespace SocialConnectAPI.Models;

/// <summary>
/// Represents a comment made by an author.
/// </summary>
public class Comment
{
    /// <summary>
    /// The unique identifier of the comment.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The ID of the author who wrote the comment.
    /// </summary>
    public int AuthorId { get; set; }
    public User Author { get; set; }
    
    /// <summary>
    /// The ID of the post that the comment is on.
    /// </summary>
    public int PostId { get; set; }
    public Post Post { get; set; }
    
    /// <summary>
    /// The content of the comment.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The number of likes the comment has received.
    /// </summary>
    public int LikeCount { get; set; }
    
    public List<CommentLike> UsersWhoLiked { get; set; }
    
    public CommentStatus Status { get; set; }
}
