using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Users;

public interface IUserRepository
{
    User? GetUserById(int userId);
    User? GetActiveUserById(int userId);
    User? GetUserByEmail(string email);
    User? GetActiveUserByEmail(string email);
    User? GetUserByFirstAndLastName(string firstName, string lastName);
    User? GetActiveUserByFirstAndLastName(string firstName, string lastName);
    User CreateUser(User user);
    User? UpdateUser(User user);
    User? DeleteUser(int userId);
    User? SetInactive(int userId);

    User? SetActive(int userId);
    User? FollowUser(int followerId, int followedId);
    User? UnfollowUser(int followerId, int followedId);
}