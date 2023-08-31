namespace SocialConnectAPI.Exceptions;


/// <summary>
/// Represents an exception that is thrown when a user is or isn't followed.
/// </summary>
public class UserFollowedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserFollowedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public UserFollowedException(string message) : base(message)
    {
        // The constructor is empty because it utilizes the base class constructor to set the exception message.
    }
}
