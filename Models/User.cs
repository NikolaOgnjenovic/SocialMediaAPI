using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SocialConnectAPI.Models;

/// <summary>
/// Represents a user with all attributes.
/// </summary>
public class User
{
    public int Id { get; set; }

    [MaxLength(25)]
    public string? FirstName { get; set; }

    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(256)]
    public string Email { get; set; }

    [Required]
    [JsonIgnore]
    public string Password { get; set; }
    
    // Workaround: List<User>...
    // public List<int> FollowingUserIds { get; set; }
    // public List<int> LikedPostIds { get; set; }
    // public List<int> LikedCommentIds { get; set; }
}