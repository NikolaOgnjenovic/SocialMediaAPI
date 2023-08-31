namespace SocialConnectAPI.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a user is not found.
/// </summary>
public class UserNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public UserNotFoundException(string message) : base(message)
    {
        // The constructor is empty because it utilizes the base class constructor to set the exception message.
    }
}
