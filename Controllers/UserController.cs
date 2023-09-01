using Microsoft.AspNetCore.Mvc;
using SocialConnectAPI.DTOs.Hateoas;
using SocialConnectAPI.DTOs.Users.Get.Response;
using SocialConnectAPI.DTOs.Users.Patch.Request;
using SocialConnectAPI.DTOs.Users.Patch.Response;
using SocialConnectAPI.DTOs.Users.Post.Request;
using SocialConnectAPI.DTOs.Users.Post.Response;
using SocialConnectAPI.DTOs.Users.Put.Request;
using SocialConnectAPI.DTOs.Users.Put.Response;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Services.Comments;
using SocialConnectAPI.Services.Posts;
using SocialConnectAPI.Services.Users;

namespace SocialConnectAPI.Controllers;

/// <summary>
/// Represents a controller for managing users in the API.
/// </summary>
[ApiController]
[Route("api/v1/users")]
[Consumes("application/json")]
[Produces("application/json", "application/xml")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly CommentService _commentService;
    private readonly PostService _postService;
    private readonly LinkGenerator _linkGenerator;
    
    private const string ControllerName = "User";
    private const string ExampleEmail = "Example@mail.com";
    private const string ExampleFirstName = "John";
    private const string ExampleLastName = "Doe";

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="userService">The service for user-related operations.</param>
    /// <param name="linkGenerator">The link generator for creating URIs.</param>
    /// <param name="commentService">The service for comment-related operations.</param>
    /// <param name="postService">The service for post-related operations.</param>
    public UserController(UserService userService, LinkGenerator linkGenerator, CommentService commentService, PostService postService)
    {
        _userService = userService;
        _linkGenerator = linkGenerator;
        _commentService = commentService;
        _postService = postService;
    }

    /// <summary>
    /// Retrieves a user by its ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <param name="isActive">A boolean indicating whether the user has to be active or not (query parameter).</param>
    /// <returns>The retrieved user.</returns>
    [HttpGet("{userId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetUserResponse> GetUserById(int userId, [FromQuery] bool isActive)
    {
        try
        {
            var user = isActive ? _userService.GetActiveUserById(userId) : _userService.GetUserById(userId);
            user.Links = GenerateUserHateoasLinks(userId);
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Retrieves a user by its email.
    /// </summary>
    /// <param name="email">The email of the user to retrieve.</param>
    /// <param name="isActive">A boolean indicating whether the user has to be active or not (query parameter).</param>
    /// <returns>The retrieved user.</returns>
    [HttpGet("{email:alpha}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetUserResponse> GetUserByEmail(string email, [FromQuery] bool isActive)
    {
        try
        {
            var user = isActive ? _userService.GetActiveUserByEmail(email) : _userService.GetUserByEmail(email);
            user.Links = GenerateUserHateoasLinks(0);
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }
    
    /// <summary>
    /// Retrieves a user by its first and last name.
    /// </summary>
    /// <param name="firstName">The first name of the user to retrieve.</param>
    /// <param name="lastName">The first name of the user to retrieve.</param>\
    /// <param name="isActive">A boolean indicating whether the user has to be active or not (query parameter).</param>
    /// <returns>The retrieved user.</returns>
    [HttpGet("{firstName:alpha}/{lastName:alpha}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GetUserResponse> GetUserByFirstAndLastName(string firstName, string lastName, [FromQuery] bool isActive)
    {
        try
        {
            var user = isActive ? _userService.GetActiveUserByFirstAndLastName(firstName, lastName) : _userService.GetUserByFirstAndLastName(firstName, lastName);
            user.Links = GenerateUserHateoasLinks(0);
            return Ok(user);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="postUserRequest">The user to create.</param>
    /// <returns>The created user.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PostUserResponse> CreateUser(PostUserRequest postUserRequest)
    {
        var createdUser = _userService.CreateUser(postUserRequest);
        var links = GenerateUserHateoasLinks(createdUser.Id);
        createdUser.Links = links;
        return Created(links[0].Href, createdUser);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <param name="putUserRequest">The user with updated information.</param>
    /// <returns>The updated user.</returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PutUserResponse> UpdateUser(PutUserRequest putUserRequest)
    {
        try
        {
            var updatedUser = _userService.UpdateUser(putUserRequest);
            updatedUser.Links = GenerateUserHateoasLinks(updatedUser.Id);
            return Ok(updatedUser);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Deletes a user by its ID.
    /// </summary>
    /// <param name="userId">The ID of the user to delete.</param>
    /// <returns>The result of the delete operation.</returns>
    [HttpDelete("{userId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult DeleteUser(int userId)
    {
        try
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }

    /// <summary>
    /// Marks a user as inactive by its ID.
    /// </summary>
    /// <param name="userId">The ID of the user to mark as inactive.</param>
    /// <returns>The updated user.</returns>
    [HttpPatch("{userId:int}/set-inactive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchUserResponse> SetInactive(int userId)
    {
        try
        {
            var inactiveUser = _userService.SetInactive(userId);
            _commentService.SetInactive(userId);
            _postService.SetInactive(userId);
            inactiveUser.Links = GenerateUserHateoasLinks(inactiveUser.Id);
            return Ok(inactiveUser);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }
    
    /// <summary>
    /// Marks a user as active by its ID.
    /// </summary>
    /// <param name="userId">The ID of the user to mark as active.</param>
    /// <returns>The updated user.</returns>
    [HttpPatch("{userId:int}/set-active")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchUserResponse> SetActive(int userId)
    {
        try
        {
            var activeUser = _userService.SetActive(userId);
            _commentService.SetActive(userId);
            _postService.SetActive(userId);
            activeUser.Links = GenerateUserHateoasLinks(activeUser.Id);
            return Ok(activeUser);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
    }
    
    /// <summary>
    /// Follows a user.
    /// </summary>
    /// <param name="followUserRequest">The follow patch request which holds the follower & followed user IDs.</param>
    /// <returns>The updated user on success, NotFound if one of the two users doesn't exist or NotModified if the follower is already following the followed user.</returns>
    [HttpPatch("follow")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchUserResponse> FollowUser([FromBody] FollowUserRequest followUserRequest)
    {
        try
        {
            var updatedUser = _userService.FollowUser(followUserRequest);
            updatedUser.Links = GenerateUserHateoasLinks(updatedUser.Id);
            return Ok(updatedUser);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
        catch (UserFollowedException)
        {
            return StatusCode(StatusCodes.Status304NotModified);
        }
    }
    
    /// <summary>
    /// Unfollows a user.
    /// </summary>
    /// <param name="followUserRequest">The follow patch request which holds the follower & followed user IDs.</param>
    /// <returns>The updated user on success, NotFound if one of the two users doesn't exist or NotModified if the follower is not following the followed user.</returns>
    [HttpPatch("unfollow")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<PatchUserResponse> UnfollowUser([FromBody] FollowUserRequest followUserRequest)
    {
        try
        {
            var updatedUser = _userService.UnfollowUser(followUserRequest);
            updatedUser.Links = GenerateUserHateoasLinks(updatedUser.Id);
            return Ok(updatedUser);
        }
        catch (UserNotFoundException)
        {
            return NotFound();
        }
        catch (UserFollowedException)
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
    
    private List<Link> GenerateUserHateoasLinks(int userId)
    {
        return new List<Link>
        {
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(GetUserById),
                    values: new { userId }), ControllerName, "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(GetUserByEmail),
                values: new { ExampleEmail }), ControllerName, "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(GetUserByFirstAndLastName),
                values: new { ExampleFirstName, ExampleLastName }), ControllerName, "GET"),
            new(_linkGenerator.GetUriByAction(HttpContext, nameof(CreateUser)), ControllerName, "POST"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(UpdateUser)), ControllerName, "PUT"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(SetInactive)), ControllerName, "PATCH"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(SetActive)), ControllerName, "PATCH"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(FollowUser)), ControllerName, "PATCH"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(UnfollowUser)), ControllerName, "PATCH"),
                new(_linkGenerator.GetUriByAction(HttpContext, nameof(DeleteUser),
                    values: new { userId }), ControllerName, "DELETE")
        };
    }
}