using System.ComponentModel.DataAnnotations;

namespace SocialConnectAPI.Models;

/// <summary>
/// Represents a post created by an author.
/// </summary>
public class Post
{
    /// <summary>
    /// The unique identifier of the post.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The ID of the author who created the post.
    /// </summary>
    public int AuthorId { get; set; }
    public User Author { get; set; }
    
    /// <summary>
    /// The content of the post.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The number of likes the post has received.
    /// </summary>
    public int LikeCount { get; set; }
    
    /// <summary>
    /// The status of the post (Active / Archived).
    /// </summary>
    public PostStatus Status { get; set; }
    
    /// <summary>
    /// The list of tags associated with the post.
    /// </summary>
    public List<Tag> Tags { get; set; }
    
    public List<PostLike> UsersWhoLiked { get; set; }
}
