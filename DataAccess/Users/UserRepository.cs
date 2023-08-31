using SocialConnectAPI.Exceptions;
using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Users;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public User? GetUserById(int userId)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.Id == userId);
    }

    public User? GetActiveUserById(int userId)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.Id == userId && user.Status == UserStatus.Active);
    }
    
    public User? GetUserByEmail(string email)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.Email == email);
    }
    
    public User? GetActiveUserByEmail(string email)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.Email == email && user.Status == UserStatus.Active);
    }
    
    public User? GetUserByFirstAndLastName(string firstName, string lastName)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.LastName == lastName && user.FirstName == firstName);
    }

    public User? GetActiveUserByFirstAndLastName(string firstName, string lastName)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.LastName == lastName && user.FirstName == firstName && user.Status == UserStatus.Active);
    }
    
    public User CreateUser(User user)
    {
        var createdUser = _databaseContext.Users.Add(user);
        _databaseContext.SaveChanges();
        return createdUser.Entity;
    }

    public User? UpdateUser(User user)
    {
        var userInDatabase = GetUserById(user.Id);
        if (userInDatabase == null)
        {
            return null;
        }

        userInDatabase = user;
        _databaseContext.SaveChanges();
        return userInDatabase;
    }

    public User? DeleteUser(int userId)
    {
        var userInDatabase = GetUserById(userId);
        if (userInDatabase == null)
        {
            return null;
        }
        
        var deletedUser = _databaseContext.Users.Remove(userInDatabase);
        _databaseContext.SaveChanges();
        return deletedUser.Entity;
    }

    public User? SetInactive(int userId)
    {
        var userInDatabase = GetUserById(userId);
        if (userInDatabase == null)
        {
            return null;
        }

        userInDatabase.Status = UserStatus.Inactive;
        _databaseContext.SaveChanges();
        return userInDatabase;
    }
    
    public User? SetActive(int userId)
    {
        var userInDatabase = GetUserById(userId);
        if (userInDatabase == null)
        {
            return null;
        }

        userInDatabase.Status = UserStatus.Active;
        _databaseContext.SaveChanges();
        return userInDatabase;
    }

    public User? FollowUser(int followerId, int followedId)
    {
        var userInDatabase = GetUserById(followerId);
        if (userInDatabase == null)
        {
            return null;
        }

        var followingUserInDatabase = GetUserById(followedId);
        if (followingUserInDatabase == null)
        {
            return null;
        }
        
        if (userInDatabase.Following.Contains(followingUserInDatabase))
        {
            throw new UserFollowedException("User with id " + userInDatabase + " already follows user with id " +
                                            followedId);
        }
        
        userInDatabase.Following.Add(followingUserInDatabase);
        followingUserInDatabase.Followers.Add(userInDatabase);
        _databaseContext.SaveChanges();
        return userInDatabase;
    }
    
    public User? UnfollowUser(int followerId, int followedId)
    {
        var userInDatabase = GetUserById(followerId);
        if (userInDatabase == null)
        {
            return null;
        }

        var followingUserInDatabase = GetUserById(followedId);
        if (followingUserInDatabase == null)
        {
            return null;
        }

        if (!userInDatabase.Following.Contains(followingUserInDatabase))
        {
            throw new UserFollowedException("User with id " + userInDatabase + " does not follow user with id " +
                                            followedId);
        }
        
        userInDatabase.Following.Remove(followingUserInDatabase);
        followingUserInDatabase.Followers.Remove(userInDatabase);
        _databaseContext.SaveChanges();
        return userInDatabase;
    }
}