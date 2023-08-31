using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SocialConnectAPI.Models;

/// <summary>
/// Represents a user with all attributes.
/// </summary>
public class User
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The first name of the user.
    /// </summary>
    [MaxLength(25)]
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the user.
    /// </summary>
    [MaxLength(50)]
    public string? LastName { get; set; }

    /// <summary>
    /// The email address of the user. This property is required.
    /// </summary>
    [Required]
    [MaxLength(256)]
    public string Email { get; set; }

    /// <summary>
    /// The password of the user. This property is required and is not exposed in JSON responses.
    /// </summary>
    [Required]
    [JsonIgnore]
    public string Password { get; set; }

    public List<PostLike> PostLikes { get; set; }
    /// <summary>
    /// The list of CommentLike objects that contain the relationship between a liked comment's id and the user's id.
    /// </summary>
    public List<CommentLike> CommentLikes { get; set; }
    
    public List<User> Followers { get; set; }
    public List<User> Following { get; set; }
    
    /// <summary>
    /// The status of the user (Active / Inactive).
    /// </summary>
    public UserStatus Status { get; set; }
}
