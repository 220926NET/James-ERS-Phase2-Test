using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

public class UserRepo : IUserStorage
{

    private SqlConnectionFactory _factory;
    public UserRepo()
    {
        _factory = new SqlConnectionFactory();
    }

    // Implement getAllUsers
    public List<User> getAllUsers()
    {
        List<User> userlist = new();

        using SqlConnection conn = _factory.GetConnection();
        conn.Open();

        using SqlCommand command = new SqlCommand("SELECT * FROM Users;", conn);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                int id = (int)reader["id"];
                string? username = (string)reader["user_name"];
                string? fname = (string)reader["first_name"];
                string? lname = (string)reader["last_name"];
                string? email = (string)reader["email"];
                string? secret = (string)reader["secret"];
                int ismanager = (int)reader["is_manager"];

                User user = new User(id, username, fname, lname, email, secret, ismanager)
                {
                    id = id,
                    username = username,
                    fname = fname,
                    lname = lname,
                    email = email,
                    password = secret,
                    isManager = ismanager
                };

                userlist.Add(user);
            }
        }

        return userlist;
    }
}