using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Users;

public interface IUserRepository
{
    User? GetUserById(int userId);
    User? GetUserByEmail(string email);
    User? GetUserByFirstAndLastName(string firstName, string lastName);
    User CreateUser(User user);
    User? UpdateUser(User user);
    User? DeleteUser(int userId);
    User? SetInactive(int userId);
    // TODO User? FollowUser(int followerId, int followingId);
}