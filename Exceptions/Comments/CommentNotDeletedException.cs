namespace SocialConnectAPI.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a comment doesn't get deleted because of the user permissions.
/// </summary>
public class CommentNotDeletedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentNotDeletedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public CommentNotDeletedException(string message) : base(message)
    {
        // The constructor is empty because it utilizes the base class constructor to set the exception message.
    }
}
