using AutoMapper;
using SocialConnectAPI.DataAccess.Users;
using SocialConnectAPI.DTOs.Users.Get.Response;
using SocialConnectAPI.DTOs.Users.Patch.Request;
using SocialConnectAPI.DTOs.Users.Patch.Response;
using SocialConnectAPI.DTOs.Users.Post.Request;
using SocialConnectAPI.DTOs.Users.Post.Response;
using SocialConnectAPI.DTOs.Users.Put.Request;
using SocialConnectAPI.DTOs.Users.Put.Response;
using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.Services.Users;

public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public GetUserResponse GetUserById(int userId)
    {
        var user = _userRepository.GetUserById(userId);

        if (user == null)
        {
            throw new UserNotFoundException("User with id " + userId + " not found.");
        }

        return _mapper.Map<GetUserResponse>(user);
    }
    
    public GetUserResponse GetActiveUserById(int userId)
    {
        var user = _userRepository.GetActiveUserById(userId);

        if (user == null)
        {
            throw new UserNotFoundException("User with id " + userId + " not found.");
        }

        return _mapper.Map<GetUserResponse>(user);
    }
    
    public GetUserResponse GetUserByEmail(string email)
    {
        var user = _userRepository.GetUserByEmail(email);

        if (user == null)
        {
            throw new UserNotFoundException("User with email " + email + " not found.");
        }

        return _mapper.Map<GetUserResponse>(user);
    }
    
    public GetUserResponse GetActiveUserByEmail(string email)
    {
        var user = _userRepository.GetActiveUserByEmail(email);

        if (user == null)
        {
            throw new UserNotFoundException("User with email " + email + " not found.");
        }

        return _mapper.Map<GetUserResponse>(user);
    }

    public GetUserResponse GetUserByFirstAndLastName(string firstName, string lastName)
    {
        var user = _userRepository.GetUserByFirstAndLastName(firstName, lastName);

        if (user == null)
        {
            throw new UserNotFoundException("User with first and last name " + firstName + " " + lastName + " not found.");
        }

        return _mapper.Map<GetUserResponse>(user);
    }
    
    public GetUserResponse GetActiveUserByFirstAndLastName(string firstName, string lastName)
    {
        var user = _userRepository.GetActiveUserByFirstAndLastName(firstName, lastName);

        if (user == null)
        {
            throw new UserNotFoundException("User with first and last name " + firstName + " " + lastName + " not found.");
        }

        return _mapper.Map<GetUserResponse>(user);
    }
    
    public PostUserResponse CreateUser(PostUserRequest userUserRequest)
    {
        return _mapper.Map<PostUserResponse>(_userRepository.CreateUser(_mapper.Map<User>(userUserRequest)));
    }

    public PutUserResponse UpdateUser(PutUserRequest putUserRequest)
    {
        var updatedUser = _userRepository.UpdateUser(_mapper.Map<User>(putUserRequest));

        if (updatedUser == null)
        {
            throw new UserNotFoundException("User with id " + putUserRequest.Id + " not found.");
        }

        return _mapper.Map<PutUserResponse>(updatedUser);
    }

    public void DeleteUser(int userId)
    {
        var deletedUser = _userRepository.DeleteUser(userId);

        if (deletedUser == null)
        {
            throw new UserNotFoundException("User with id " + userId + " not found.");
        }
    }

    public PatchUserResponse SetInactive(int userId)
    {
        var updatedUser = _userRepository.SetInactive(userId);

        if (updatedUser == null)
        {
            throw new UserNotFoundException("User with id " + userId + " not found.");
        }

        return _mapper.Map<PatchUserResponse>(updatedUser);
    }
    
    public PatchUserResponse SetActive(int userId)
    {
        var updatedUser = _userRepository.SetActive(userId);

        if (updatedUser == null)
        {
            throw new UserNotFoundException("User with id " + userId + " not found.");
        }

        return _mapper.Map<PatchUserResponse>(updatedUser);
    }

    public PatchUserResponse FollowUser(FollowUserRequest followUserRequest)
    {
        var updatedUser = _userRepository.FollowUser(followUserRequest.FollowerId, followUserRequest.FollowedId);
        
        if (updatedUser == null)
        {
            throw new UserNotFoundException("User with id " + followUserRequest.FollowerId + " not found.");
        }

        return _mapper.Map<PatchUserResponse>(updatedUser);
    }
    
    public PatchUserResponse UnfollowUser(FollowUserRequest followUserRequest)
    {
        var updatedUser = _userRepository.UnfollowUser(followUserRequest.FollowerId, followUserRequest.FollowedId);
        
        if (updatedUser == null)
        {
            throw new UserNotFoundException("User with id " + followUserRequest.FollowerId + " not found.");
        }

        return _mapper.Map<PatchUserResponse>(updatedUser);
    }
}