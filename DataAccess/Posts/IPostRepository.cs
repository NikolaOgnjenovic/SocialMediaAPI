using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Posts;

public interface IPostRepository
{
    Post? GetPostById(int postId);
    Post? GetActivePostById(int postId);
    List<Post> GetPostsByUserId(int userId);
    List<Post> GetActivePostsByUserId(int userId);
    List<Post> GetPostsByTag(string tag);
    List<Post> GetActivePostsByTag(string tag);
    Post CreatePost(Post post);
    Post? UpdatePost(Post post);
    Post? DeletePost(int postId);
    Post? ArchivePost(int postId);
    // TODO: Add userId and call user service
    Post? LikePost(int postId);
    Post? DislikePost(int postId);
    void SetInactive(int userId);
    void SetActive(int userId);
}