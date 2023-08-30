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

    /// <summary>
    /// The number of likes the post has received.
    /// </summary>
    public int LikeCount { get; set; }

    // TODO: Uncomment and implement the Tags property
    // /// <summary>
    // /// The list of tags associated with the post.
    // /// </summary>
    // public List<string> Tags { get; set; }
}