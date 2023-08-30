using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Posts;

public interface IPostRepository
{
    Post? GetPostById(int postId);
    List<Post>? GetPostsByUserId(int userId);
    List<Post>? GetPostsByTag(string tag);
    Post CreatePost(Post post);
    Post? UpdatePost(Post post);
    Post? DeletePost(int postId);
    Post? ArchivePost(int postId);
    // TODO: Add userId and call user service
    Post? LikePost(int postId);
    Post? DislikePost(int postId);
}