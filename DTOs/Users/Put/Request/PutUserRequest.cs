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
}