using System.ComponentModel.DataAnnotations;

namespace SocialConnectAPI.Models;

/// <summary>
/// Represents a post created by an author.
/// </summary>
public class Post
{
    /// <summary>
    /// Gets or sets the unique identifier of the post.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the author who created the post.
    /// </summary>
    public int AuthorId { get; set; }
    
    /// <summary>
    /// Gets or sets the content of the post.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Gets or sets the number of likes the post has received.
    /// </summary>
    public int LikeCount { get; set; }
    
    /// <summary>
    /// Gets or sets the status of the post (e.g., Draft, Published, etc.).
    /// </summary>
    public PostStatus Status { get; set; }
    
    // TODO: Uncomment and implement the Tags property
    // /// <summary>
    // /// Gets or sets the list of tags associated with the post.
    // /// </summary>
    // public List<string> Tags { get; set; }
}
