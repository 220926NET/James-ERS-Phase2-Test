using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

public class DatabaseAccess
{
    // This code provides the database connection via the sqlconnectionfactory method
    private SqlConnectionFactory factory;

    public DatabaseAccess()
    {
        factory = new SqlConnectionFactory();
    }

    //This code adds the user to the database
    public void AddUser(User user)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO Users (user_name, first_name, last_name, email, secret, is_manager) VALUES (@username, @firstname, @lastname, @email, @password, @ismanager );", conn);
        cmd.Parameters.AddWithValue("@username", user.username);
        cmd.Parameters.AddWithValue("@firstname", user.fname);
        cmd.Parameters.AddWithValue("@lastname", user.lname);
        cmd.Parameters.AddWithValue("@email", user.email);
        cmd.Parameters.AddWithValue("@password", user.password);
        cmd.Parameters.AddWithValue("@ismanager", 0);
        cmd.ExecuteNonQuery();

    }

    //This code retrieves the user from the database
    public User? GetUser(string username, string password)
    {
        User? user = null;

        SqlConnection conn = factory.GetConnection();
        conn.Open();

        SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Users.user_name = @username and Users.secret = @password;", conn);

        command.Parameters.AddWithValue("@username", username);
        command.Parameters.AddWithValue("@password", password);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                int idGet = (int)reader["id"];
                string usernameGet = (string)reader["user_name"];
                string firstnameGet = (string)reader["first_name"];
                string lastnameGet = (string)reader["last_name"];
                string emailGet = (string)reader["email"];
                string passwordGet = (string)reader["secret"];
                int managerGet = (int)reader["is_manager"];

                user = new User
                (
                     idGet,
                     usernameGet,
                     firstnameGet,
                     lastnameGet,
                     emailGet,
                     passwordGet,
                     managerGet
                );

            }
        }

        conn.Close();
        return user;

    }

    public bool TestPassword(string username, string secret)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        // select * from userdatabase where user_name = @username and secret = '@password'
        // this counts if there are any matches in the database. If it's more than 0, it returns true.
        // need to make a method to keep someone from entering a new user which matches an old one
        string sql = "SELECT COUNT(*) FROM Users WHERE (user_name = @user_name and secret = @secret)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@user_name", username);
        cmd.Parameters.AddWithValue("@secret", secret);

        int count = (int)cmd.ExecuteScalar();

        conn.Close();

        if (count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Check if the username already exists within the database so that no duplicates are created (which already almost happened once)
    public bool TestUsername(string username)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        // select * from userdatabase where user_name = @username 
        // this counts if there are any matches in the database. If it's more than 0, it returns true.
        string sql = "SELECT COUNT(*) FROM Users WHERE (user_name = @user_name)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@user_name", username);

        int count = (int)cmd.ExecuteScalar();

        conn.Close();

        if (count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TestPending(int tid)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        // SELECT COUNT(*) FROM Tickets WHERE (id = @tid AND reviewed_status != 'Pending')"
        // this counts if there are any matches in the database. If it's more than 0, it returns true.
        string sql = "SELECT COUNT(*) FROM Tickets WHERE (id = @tid AND reviewed_status != 'Pending')";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@tid", tid);

        int count = (int)cmd.ExecuteScalar();

        conn.Close();

        if (count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // tests to see if a user in the database is already a manager
    public bool TestManager(int uid)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        // select uid from userdatabase where user_name = @username
        // this counts if there are any matches in the database. If it's more than 0, it returns true.
        string sql = "SELECT COUNT(*) FROM Users WHERE (id = @uid AND is_manager = 1)";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@uid", uid);

        int count = (int)cmd.ExecuteScalar();

        conn.Close();

        if (count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    // Add tickets - user entered data
    public void AddTicket(Ticket ticket)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("INSERT INTO Tickets (submitted_by, amount_requested, submission_desc, reviewed_status) VALUES (@submittedBy, @amountRequest, @submitDesc, @reviewedStat);", conn);
        cmd.Parameters.AddWithValue("@submittedBy", ticket.submittedBy);
        cmd.Parameters.AddWithValue("@amountRequest", ticket.amountReq);
        cmd.Parameters.AddWithValue("@submitDesc", ticket.submitDesc);
        cmd.Parameters.AddWithValue("@reviewedStat", ticket.reviewedStat);
        cmd.ExecuteNonQuery();

    }

    // Update tickets - this is just for managers to update the status from pending
    public void UpdateTicket(Ticket ticket)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE Tickets SET reviewed_status = @reviewedStat WHERE id = @ticketid;", conn);
        cmd.Parameters.AddWithValue("@ticketid", ticket.tid);
        cmd.Parameters.AddWithValue("@reviewedStat", ticket.reviewedStat);
        cmd.ExecuteNonQuery();

    }

    // Update Role - this is just for managers to promote an employee account to a manager account
    public void UpdateRole(User user)
    {
        using SqlConnection conn = factory.GetConnection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("UPDATE Users SET is_manager = @promoteUser WHERE id = @userid;", conn);
        cmd.Parameters.AddWithValue("@userid", user.id);
        cmd.Parameters.AddWithValue("@promoteUser", 1);
        cmd.ExecuteNonQuery();

    }

}