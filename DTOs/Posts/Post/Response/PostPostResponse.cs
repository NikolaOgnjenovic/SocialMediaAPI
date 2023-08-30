using SocialConnectAPI.DTOs.Hateoas;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.DTOs.Posts.Post.Response.PostPostResponse;

public class PostPostResponse : LinkCollection
{
    /// <summary>
    /// The unique identifier of the post.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The ID of the author who created the post.
    /// </summary>
    public int AuthorId { get; set; }
    
    /// <summary>
    /// The content of the post.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// The number of likes the post has received.
    /// </summary>
    public int LikeCount { get; set; }
    
    /// <summary>
    /// The status of the post (e.g., Draft, Published, etc.).
    /// </summary>
    public PostStatus Status { get; set; }
    
    // TODO: Uncomment and implement the Tags property
    // /// <summary>
    // /// The list of tags associated with the post.
    // /// </summary>
    // public List<string> Tags { get; set; }
}