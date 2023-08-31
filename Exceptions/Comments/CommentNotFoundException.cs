namespace SocialConnectAPI.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a comment is not found.
/// </summary>
public class CommentNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public CommentNotFoundException(string message) : base(message)
    {
        // The constructor is empty because it utilizes the base class constructor to set the exception message.
    }
}
