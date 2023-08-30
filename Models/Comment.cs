using System.ComponentModel.DataAnnotations;

namespace SocialConnectAPI.Models;

public class Comment
{
    [Key]
    public int Id { get; set; }

    public int AuthorId { get; set; }
    
    public string Content { get; set; }
    
    public int LikeCount { get; set; }
}