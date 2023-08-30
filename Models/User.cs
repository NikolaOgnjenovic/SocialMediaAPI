using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SocialConnectAPI.Models;

/// <summary>
/// Represents a user with all attributes.
/// </summary>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [MaxLength(25)]
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [MaxLength(50)]
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user. This property is required.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the password of the user. This property is required and is not exposed in JSON responses.
    /// </summary>
    [Required]
    [JsonIgnore]
    public string Password { get; set; }
    
    // TODO: Uncomment and implement the FollowingUserIds, LikedPostIds, and LikedCommentIds properties
    // /// <summary>
    // /// Gets or sets the list of IDs of users that this user is following.
    // /// </summary>
    // public List<int> FollowingUserIds { get; set; }
    
    // /// <summary>
    // /// Gets or sets the list of IDs of posts that this user has liked.
    // /// </summary>
    // public List<int> LikedPostIds { get; set; }
    
    // /// <summary>
    // /// Gets or sets the list of IDs of comments that this user has liked.
    // /// </summary>
    // public List<int> LikedCommentIds { get; set; }
}
