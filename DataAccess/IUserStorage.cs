using Models;

namespace DataAccess;

public interface IUserStorage
{
    /// <summary>
    /// Returns all users
    /// </summary>
    /// <returns>List of users from User database</returns>
    List<User> getAllUsers();
}