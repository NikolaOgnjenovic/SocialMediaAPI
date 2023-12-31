using Microsoft.AspNetCore.Mvc;
using SocialConnectAPI.DTOs.Comments.Delete.Request;
using SocialConnectAPI.DTOs.Comments.Get.Response;
using SocialConnectAPI.DTOs.Comments.Patch.Request;
using SocialConnectAPI.DTOs.Comments.Patch.Response;
using SocialConnectAPI.DTOs.Comments.Post.Request;
using SocialConnectAPI.DTOs.Comments.Post.Response;
using SocialConnectAPI.DTOs.Comments.Put.Request;
using SocialConnectAPI.DTOs.Comments.Put.Response;
using SocialConnectAPI.DTOs.Hateoas;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Services.Comments;

namespace SocialConnectAPI.Controllers;

/// <summary>
/// Represents a controller for managing comments in the API.
/// </summary>
[ApiController]
[Route("api/v1/comments")]
[Consumes("application/json")]
[Produces("application/json", "application/xml")]
public class CommentController : ControllerBase
{
    private readonly CommentService _commentService;
    private readonly LinkGenerator _linkGenerator;
    private const string ControllerName = "Comment";

    /// <summary>
    /// Initializes a new instance of the <see cref="CommentController"/> class.
    /// </summary>
    /// <param name="commentService">The service for comment-related operations.</param>
    /// <param name="linkGenerator">The link generator for creating URIs.</param>
    public CommentController(CommentService commentService, LinkGenerator linkGenerator)
    {
        _commentService = commentService;
        _linkGenerator = linkGenerator;
    }

    /// <summary>
    /// Retrieves a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to retrieve.</param>
    /// <param name="isActive">A boolean indicating whether the comment has to be active or not (query parameter).</param>
    /// <returns>The retrieved comment.</returns>
    [HttpGet("{commentId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetCommentResponse> GetCommentById(int commentId, [FromQuery] bool isActive)
    {
        try
        {
            var comment = isActive
                ? _commentService.GetActiveCommentById(commentId)
                : _commentService.GetCommentById(commentId);
            comment.Links = GenerateCommentHateoasLinks(commentId);
            return Ok(comment);
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Retrieves comments associated with a user by their user ID.
    /// </summary>
    /// <param name="userId">The ID of the user whose comments to retrieve.</param>
    /// <param name="isActive">A boolean indicating whether the comment has to be active or not (query parameter).</param>
    /// <returns>The list of comments associated with the user.</returns>
    [HttpGet("users/{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<GetCommentResponse>> GetCommentsByUserId(int userId, [FromQuery] bool isActive)
    {
        var comments = isActive ? _commentService.GetActiveCommentsByUserId(userId) : _commentService.GetCommentsByUserId(userId);
        foreach (var comment in comments)
        {
            comment.Links = GenerateCommentHateoasLinks(comment.Id);
        }

        return Ok(comments);
    }

    /// <summary>
    /// Creates a new comment.
    /// </summary>
    /// <param name="postCommentRequest">The comment to create.</param>
    /// <returns>The created comment.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PostCommentResponse> CreateComment(PostCommentRequest postCommentRequest)
    {
        var createdComment = _commentService.CreateComment(postCommentRequest);
        var links = GenerateCommentHateoasLinks(createdComment.Id);
        createdComment.Links = links;
        return Created(links[0].Href, createdComment);
    }

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="putCommentRequest">The comment with updated information.</param>
    /// <returns>The updated comment.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PutCommentResponse> UpdateComment(PutCommentRequest putCommentRequest)
    {
        try
        {
            var updatedComment = _commentService.UpdateComment(putCommentRequest);
            updatedComment.Links = GenerateCommentHateoasLinks(updatedComment.Id);
            return Ok(updatedComment);
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to delete.</param>
    /// <param name="deleteCommentRequest">The delete comment request that contains the ID of the user deleting the comment.</param>
    /// <returns>The result of the delete operation.</returns>
    [HttpDelete("{commentId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult DeleteComment(int commentId, [FromBody] DeleteCommentRequest deleteCommentRequest)
    {
        try
        {
            _commentService.DeleteComment(commentId, deleteCommentRequest);
            return NoContent();
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
        catch (CommentNotDeletedException)
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    /// <summary>
    /// Likes a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to like.</param>
    /// <param name="likeCommentRequest">The like comment request that contains the ID of the user liking the comment.</param>
    /// <returns>The liked comment.</returns>
    [HttpPatch("{commentId:int}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchCommentResponse> LikeComment(int commentId,
        [FromBody] LikeCommentRequest likeCommentRequest)
    {
        try
        {
            var likedComment = _commentService.LikeComment(commentId, likeCommentRequest);
            likedComment.Links = GenerateCommentHateoasLinks(likedComment.Id);
            return Ok(likedComment);
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
        catch (CommentLikedException)
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }
    }

    /// <summary>
    /// Dislikes a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to dislike.</param>
    /// <param name="likeCommentRequest">The like comment request that contains the ID of the user disliking the comment.</param>
    /// <returns>The disliked comment.</returns>
    [HttpPatch("{commentId:int}/dislike")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchCommentResponse> DislikeComment(int commentId,
        [FromBody] LikeCommentRequest likeCommentRequest)
    {
        try
        {
            var dislikedComment = _commentService.DislikeComment(commentId, likeCommentRequest);
            dislikedComment.Links = GenerateCommentHateoasLinks(dislikedComment.Id);
            return Ok(dislikedComment);
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
        catch (CommentLikedException)
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

    private List<Link> GenerateCommentHateoasLinks(int commentId)
    {
        return new List<Link>
        {
            new(
                _linkGenerator.GetUriByAction(HttpContext, nameof(GetCommentById),
                    values: new { commentId }), ControllerName, "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(GetCommentsByUserId), values: new { userId = 1 }),
                ControllerName, "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateComment)), ControllerName, "POST"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateComment)), ControllerName, "PUT"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(LikeComment)), ControllerName, "PATCH"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(DislikeComment)), ControllerName, "PATCH"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteComment),
                values: new { commentId }), ControllerName, "DELETE")
        };
    }
}