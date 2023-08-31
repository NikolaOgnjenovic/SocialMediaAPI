namespace SocialConnectAPI.DTOs.Users.Post.Request;

public class PostUserRequest
{
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
    /// The password of the user. This property is required and is not exposed in JSON responses.
    /// </summary>
    public string Password { get; set; }
}