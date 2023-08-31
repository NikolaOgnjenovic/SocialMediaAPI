namespace SocialConnectAPI.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a comment has already been liked when liking or hasn't been liked when disliking.
/// </summary>
public class CommentLikedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CommentLikedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public CommentLikedException(string message) : base(message)
    {
        // The constructor is empty because it utilizes the base class constructor to set the exception message.
    }
}
