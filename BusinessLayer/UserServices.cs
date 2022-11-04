using Models;
using DataAccess;
using Microsoft.Data.SqlClient;

namespace BusinessLayer;
public class UserService
{
    private UserRepo _repo;
    public UserService()
    { _repo = new UserRepo(); }

    /// <summary>
    /// Returns users based on various criteria
    /// </summary>

    public List<User> getAllUsers()

    {
        List<User> allUsers = _repo.getAllUsers();

        return allUsers;
    }

}