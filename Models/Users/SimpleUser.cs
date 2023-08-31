using SocialConnectAPI.Models;

namespace SocialConnectAPI.DTOs.Users.Get.Response;

/// <summary>
/// Contains no complex objects. Used to prevent cycles.
/// </summary>
public class SimpleUser
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
    /// The status of the user (Active / Inactive).
    /// </summary>
    public UserStatus Status { get; set; }
}