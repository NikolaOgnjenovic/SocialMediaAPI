using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Posts;

public interface IPostRepository
{
    Post? GetPostById(int postId);
    List<Post>? GetPostsByUserId(int userId);
    List<Post>? GetPostsByTag(string tag);
    Post CreatePost(Post post);
    Post? UpdatePost(Post post);
    void DeletePost(int postId);
    Post? ArchivePost(int postId);
    Post? LikePost(int postId, int userId);
    
    bool SaveChanges();
}