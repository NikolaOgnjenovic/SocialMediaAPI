using System.ComponentModel.DataAnnotations;

namespace SocialConnectAPI.Models;

public class Tag
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// The text content of the tag.
    /// </summary>
    [MaxLength(25)]
    [Required]
    public string content { get; set; }
}