using Microsoft.AspNetCore.Mvc;
using SocialConnectAPI.DTOs.Hateoas;
using SocialConnectAPI.DTOs.Posts.Get.Response;
using SocialConnectAPI.DTOs.Posts.Patch.Request;
using SocialConnectAPI.DTOs.Posts.Patch.Response;
using SocialConnectAPI.DTOs.Posts.Post.Request;
using SocialConnectAPI.DTOs.Posts.Post.Response;
using SocialConnectAPI.DTOs.Posts.Put.Request;
using SocialConnectAPI.DTOs.Posts.Put.Response;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Services.Comments;
using SocialConnectAPI.Services.Posts;

namespace SocialConnectAPI.Controllers;

/// <summary>
/// Represents a controller for managing posts in the API.
/// </summary>
[ApiController]
[Route("api/v1/posts")]
[Consumes("application/json")]
[Produces("application/json", "application/xml")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;
    private readonly CommentService _commentService;
    private readonly LinkGenerator _linkGenerator;
    private const string ControllerName = "Post";

    /// <summary>
    /// Initializes a new instance of the <see cref="PostController"/> class.
    /// </summary>
    /// <param name="postService">The service for post-related operations.</param>
    /// <param name="linkGenerator">The link generator for creating URIs.</param>
    /// <param name="commentService">The service for comment-related operations.</param>
    public PostController(PostService postService, LinkGenerator linkGenerator, CommentService commentService)
    {
        _postService = postService;
        _linkGenerator = linkGenerator;
        _commentService = commentService;
    }

    /// <summary>
    /// Retrieves a post by its ID.
    /// </summary>
    /// <param name="postId">The ID of the post to retrieve.</param>
    /// <param name="isActive">A boolean indicating whether the post has to be active or not (query parameter).</param>
    /// <returns>The retrieved post.</returns>
    [HttpGet("{postId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetPostResponse> GetPostById(int postId, [FromQuery] bool isActive)
    {
        try
        {
            var post = isActive ? _postService.GetActivePostById(postId) : _postService.GetPostById(postId);
            post.Links = GeneratePostHateoasLinks(postId);
            return Ok(post);
        }
        catch (PostNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Retrieves posts associated with a user by their user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose posts to retrieve.</param>
    /// <param name="isActive">A boolean indicating whether the posts have to be active or not (query parameter).</param>
    /// <returns>The list of posts associated with the user.</returns>
    [HttpGet("users/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<GetPostResponse>> GetPostsByUserId(int userId, [FromQuery] bool isActive)
    {
        var posts = isActive ? _postService.GetActivePostsByUserId(userId) : _postService.GetPostsByUserId(userId);
        foreach (var post in posts)
        {
            post.Links = GeneratePostHateoasLinks(post.Id);
        }
        return Ok(posts);
    }

    /// <summary>
    /// Retrieves a list of posts that contain the given tag.
    /// </summary>
    /// <param name="tag">The tag that the posts need to contain.</param>
    /// <param name="isActive">A boolean indicating whether the posts have to be active or not (query parameter).</param>
    /// <returns>The list of posts that contain the given tag.</returns>
    [HttpGet("tags/{tag:alpha}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<GetPostResponse>> GetPostsByTag(string tag, [FromQuery] bool isActive)
    {
        var posts = isActive ? _postService.GetActivePostsByTag(tag) : _postService.GetPostsByTag(tag);
        foreach (var post in posts)
        {
            post.Links = GeneratePostHateoasLinks(post.Id);
        }
        return Ok(posts);
    }

    /// <summary>
    /// Creates a new post.
    /// </summary>
    /// <param name="postPostRequest">The post to create.</param>
    /// <returns>The created post.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PostPostResponse> CreatePost(PostPostRequest postPostRequest)
    {
        var createdPost = _postService.CreatePost(postPostRequest);
        var links = GeneratePostHateoasLinks(createdPost.Id);
        createdPost.Links = links;
        return Created(links[0].Href, createdPost);
    }

    /// <summary>
    /// Updates an existing post.
    /// </summary>
    /// <param name="putPostRequest">The post with updated information.</param>
    /// <returns>The updated post.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PutPostResponse> UpdatePost(PutPostRequest putPostRequest)
    {
        try
        {
            var updatedPost = _postService.UpdatePost(putPostRequest);
            updatedPost.Links = GeneratePostHateoasLinks(updatedPost.Id);
            return Ok(updatedPost);
        }
        catch (PostNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a post by its ID.
    /// </summary>
    /// <param name="postId">The ID of the post to delete.</param>
    /// <returns>The result of the delete operation.</returns>
    [HttpDelete("{postId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult DeletePost(int postId)
    {
        try
        {
            _postService.DeletePost(postId);
            return NoContent();
        }
        catch (PostNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    /// <summary>
    /// Archives a post by its ID.
    /// </summary>
    /// <param name="postId">The ID of the post to archive.</param>
    /// <returns>The archived post.</returns>
    [HttpPatch("{postId:int}/archive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchPostResponse> ArchivePost(int postId)
    {
        try
        {
            var archivedPost = _postService.ArchivePost(postId);
            _commentService.ArchiveByPostId(postId);
            archivedPost.Links = GeneratePostHateoasLinks(archivedPost.Id);
            return Ok(archivedPost);
        }
        catch (PostNotFoundException)
        {
            return NotFound();
        }
    }
    
    /// <summary>
    /// Likes a post by its ID.
    /// </summary>
    /// <param name="postId">The ID of the post to like.</param>
    /// <param name="likePostRequest">The like post request that contains the ID of the user liking the post.</param>
    /// <returns>The liked post.</returns>
    [HttpPatch("{postId:int}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchPostResponse> LikePost(int postId, [FromBody] LikePostRequest likePostRequest)
    {
        try
        {
            var likedPost = _postService.LikePost(postId, likePostRequest);
            likedPost.Links = GeneratePostHateoasLinks(likedPost.Id);
            return Ok(likedPost);
        }
        catch (PostNotFoundException)
        {
            return NotFound();
        }
        catch (PostLikedException)
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }
    }

    /// <summary>
    /// Dislikes a post by its ID.
    /// </summary>
    /// <param name="postId">The ID of the post to dislike.</param>
    /// <param name="likePostRequest">The like post request that contains the ID of the user disliking the post.</param>
    /// <returns>The disliked post.</returns>
    [HttpPatch("{postId:int}/dislike")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchPostResponse> DislikePost(int postId, [FromBody] LikePostRequest likePostRequest)
    {
        try
        {
            var dislikedPost = _postService.DislikePost(postId, likePostRequest);
            dislikedPost.Links = GeneratePostHateoasLinks(dislikedPost.Id);
            return Ok(dislikedPost);
        }
        catch (PostNotFoundException)
        {
            return NotFound();
        }
        catch (PostLikedException)
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }
    }

    /// <summary>
    /// Retrieves the allowed HTTP methods for the current endpoint.
    /// </summary>
    /// <returns>The allowed HTTP methods.</returns>
    [HttpOptions]
    public IActionResult GetOptions()
    {
        Response.Headers.Add("Allow", "GET, POST, PUT, PATCH, DELETE");
        return Ok();
    }

    private List<Link> GeneratePostHateoasLinks(int postId)
    {
        return new List<Link>
        {
            new(
                _linkGenerator.GetUriByAction(HttpContext, nameof(GetPostById),
                    values: new { postId }), ControllerName, "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(GetPostsByUserId), values: new { userId = 1 }),
                ControllerName, "GET"),
            new(
                _linkGenerator.GetUriByAction(HttpContext, nameof(GetPostsByTag),
                    values: new { postId }), ControllerName, "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(CreatePost)), ControllerName, "POST"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdatePost)), ControllerName, "PUT"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(LikePost)), ControllerName, "PATCH"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(ArchivePost)), ControllerName, "PATCH"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(DislikePost)), ControllerName, "PATCH"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(DeletePost),
                    values: new { postId }), ControllerName, "DELETE")
        };
    }
}