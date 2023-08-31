using SocialConnectAPI.Models;

namespace SocialConnectAPI.DTOs.Posts.Post.Request;

public class PostPostRequest
{
    /// <summary>
    /// The ID of the author who created the post.
    /// </summary>
    public int AuthorId { get; set; }

    /// <summary>
    /// The content of the post.
    /// </summary>
    public string Content { get; set; }
}