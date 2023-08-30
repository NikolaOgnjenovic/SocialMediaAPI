using Microsoft.AspNetCore.Mvc;
using SocialConnectAPI.DataAccess.Comments;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Models;
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
    private readonly ICommentRepository _commentRepository;
    private readonly CommentService _commentService;
    private readonly LinkGenerator _linkGenerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommentController"/> class.
    /// </summary>
    /// <param name="commentRepository">The repository for comments.</param>
    /// <param name="commentService">The service for comment-related operations.</param>
    /// <param name="linkGenerator">The link generator for creating URIs.</param>
    public CommentController(ICommentRepository commentRepository, CommentService commentService,
        LinkGenerator linkGenerator)
    {
        _commentRepository = commentRepository;
        _commentService = commentService;
        _linkGenerator = linkGenerator;
    }

    /// <summary>
    /// Retrieves a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to retrieve.</param>
    /// <returns>The retrieved comment.</returns>
    [HttpGet("{commentId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Comment> GetCommentById(int commentId)
    {
        try
        {
            return Ok(_commentService.GetCommentById(commentId));
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
    /// <returns>The list of comments associated with the user.</returns>
    [HttpGet("/users/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Comment>> GetCommentsByUserId(int userId)
    {
        return Ok(_commentRepository.GetCommentsByUserId(userId));
    }

    /// <summary>
    /// Creates a new comment.
    /// </summary>
    /// <param name="comment">The comment to create.</param>
    /// <returns>The created comment.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Comment> CreateComment(Comment comment)
    {
        var link = _linkGenerator.GetUriByAction(HttpContext, "GetCommentById", "Comment",
            new { commentId = comment.Id });
        return Created(link, _commentService.CreateComment(comment));
    }

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="comment">The comment with updated information.</param>
    /// <returns>The updated comment.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Comment> UpdateComment(Comment comment)
    {
        try
        {
            return Ok(_commentService.UpdateComment(comment));
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
    /// <returns>The result of the delete operation.</returns>
    [HttpDelete("{commentId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Comment> DeleteComment(int commentId)
    {
        try
        {
            _commentService.DeleteComment(commentId);
            return NoContent();
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    // [HttpPatch("{commentId}/archive")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public ActionResult<Comment> ArchiveComment(int commentId)
    // {
    //     try
    //     {
    //         return Ok(_commentService.ArchiveComment(commentId));
    //     }
    //     catch (CommentNotFoundException)
    //     {
    //         return NotFound();
    //     }
    // }

    /// <summary>
    /// Likes a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to like.</param>
    /// <returns>The liked comment.</returns>
    [HttpPatch("{commentId}/like")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Comment> LikeComment(int commentId)
    {
        try
        {
            return Ok(_commentService.LikeComment(commentId));
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Dislikes a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to dislike.</param>
    /// <returns>The disliked comment.</returns>
    [HttpPatch("{commentId}/dislike")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Comment> DislikeComment(int commentId)
    {
        try
        {
            return Ok(_commentService.DislikeComment(commentId));
        }
        catch (CommentNotFoundException)
        {
            return NotFound();
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
}