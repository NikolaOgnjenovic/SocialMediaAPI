namespace SocialConnectAPI.Exceptions;

public class PostNotFoundException: Exception
{
    public PostNotFoundException(string message) : base(message)
    {
            
    }
}