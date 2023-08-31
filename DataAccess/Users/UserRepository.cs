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

    public User? GetUserByEmail(string email)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.Email == email);
    }

    public User? GetUserByFirstAndLastName(string firstName, string lastName)
    {
        return _databaseContext.Users.FirstOrDefault(user => user.LastName == lastName && user.FirstName == firstName);
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
}