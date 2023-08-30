using SocialConnectAPI.Models;

namespace SocialConnectAPI.DataAccess.Users;

public interface IUserRepository
{
    User? GetUser(int userId);
    User? GetUser(string email);
    User? GetUser(string firstName, string lastName);
    User CreateUser(User user);
    User? UpdateUser(User user);
    void DeleteUser(int userId);
}