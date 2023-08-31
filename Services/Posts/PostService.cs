using AutoMapper;
using SocialConnectAPI.DataAccess.Posts;
using SocialConnectAPI.DTOs.Posts.Get.Response;
using SocialConnectAPI.DTOs.Posts.Patch.Response;
using SocialConnectAPI.DTOs.Posts.Post.Request;
using SocialConnectAPI.DTOs.Posts.Post.Response;
using SocialConnectAPI.DTOs.Posts.Put.Request;
using SocialConnectAPI.DTOs.Posts.Put.Response;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.Services.Posts;

public class PostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostService(IPostRepository postRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }
    
    public GetPostResponse GetPostById(int postId)
    {
        var post = _postRepository.GetPostById(postId);

        if (post == null)
        {
            throw new PostNotFoundException("Post with id " + postId + " not found.");
        }

        return _mapper.Map<GetPostResponse>(post);
    }
    
    public GetPostResponse GetActivePostById(int postId)
    {
        var post = _postRepository.GetActivePostById(postId);

        if (post == null)
        {
            throw new PostNotFoundException("Post with id " + postId + " not found.");
        }

        return _mapper.Map<GetPostResponse>(post);
    }

    public List<GetPostResponse> GetPostsByUserId(int userId)
    {
        return _mapper.Map<List<GetPostResponse>>(_postRepository.GetPostsByUserId(userId));
    }
    
    public List<GetPostResponse> GetActivePostsByUserId(int userId)
    {
        return _mapper.Map<List<GetPostResponse>>(_postRepository.GetActivePostsByUserId(userId));
    }
    
    public List<GetPostResponse> GetPostsByTag(string tag)
    {
        return _mapper.Map<List<GetPostResponse>>(_postRepository.GetPostsByTag(tag));
    }
    
    public List<GetPostResponse> GetActivePostsByTag(string tag)
    {
        return _mapper.Map<List<GetPostResponse>>(_postRepository.GetActivePostsByTag(tag));
    }

    public PostPostResponse CreatePost(PostPostRequest postPostRequest)
    {
        return _mapper.Map<PostPostResponse>(_postRepository.CreatePost(_mapper.Map<Post>(postPostRequest)));
    }

    public PutPostResponse UpdatePost(PutPostRequest putPostRequest)
    {
        var updatedPost = _postRepository.UpdatePost(_mapper.Map<Post>(putPostRequest));

        if (updatedPost == null)
        {
            throw new PostNotFoundException("Post with id " + putPostRequest.Id + " not found.");
        }

        return _mapper.Map<PutPostResponse>(updatedPost);
    }

    public void DeletePost(int postId)
    {
        var deletedPost = _postRepository.DeletePost(postId);

        if (deletedPost == null)
        {
            throw new PostNotFoundException("Post with id " + postId + " not found.");
        }
    }

    public PatchPostResponse ArchivePost(int postId)
    {
        var archivedPost = _postRepository.ArchivePost(postId);

        if (archivedPost == null)
        {
            throw new PostNotFoundException("Post with id " + postId + " not found.");
        }

        return _mapper.Map<PatchPostResponse>(archivedPost);
    }
    
    public PatchPostResponse LikePost(int postId)
    {
        var likedPost = _postRepository.LikePost(postId);

        if (likedPost == null)
        {
            throw new PostNotFoundException("Post with id " + postId + " not found.");
        }

        return _mapper.Map<PatchPostResponse>(likedPost);
    }

    public PatchPostResponse DislikePost(int postId)
    {
        var dislikedPost = _postRepository.DislikePost(postId);

        if (dislikedPost == null)
        {
            throw new PostNotFoundException("Post with id " + postId + " not found.");
        }

        return _mapper.Map<PatchPostResponse>(dislikedPost);
    }

    public void SetInactive(int userId)
    {
        _postRepository.SetInactive(userId);
    }
    
    public void SetActive(int userId)
    {
        _postRepository.SetActive(userId);
    }
}