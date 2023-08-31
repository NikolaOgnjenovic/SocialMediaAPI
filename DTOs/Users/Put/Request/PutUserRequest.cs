using SocialConnectAPI.Models;

namespace SocialConnectAPI.DTOs.Users.Put.Request;

public class PutUserRequest
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// The email address of the user. This property is required.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The list of Follower objects that represent the relationship between a follower's id and the user's id.
    /// </summary>
    // public List<Followers> Followers { get; set; }
    
    /// <summary>
    /// The list of PostLike objects that contain the relationship between a liked post's id and the user's id.
    /// </summary>
    public List<PostLike> PostLikes { get; set; }
    
    /// <summary>
    /// The list of CommentLike objects that contain the relationship between a liked comment's id and the user's id.
    /// </summary>
    public List<CommentLike> CommentLikes { get; set; }
    
    /// <summary>
    /// The status of the user (Active / Inactive).
    /// </summary>
    public UserStatus Status { get; set; }
}