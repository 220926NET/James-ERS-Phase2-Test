using System.Text.Json.Serialization;

namespace Models;

public class User
{
    // Properties for User
    public int id
    { get; set; }

    public string? username
    { get; set; } = string.Empty;

    public string? fname
    { get; set; } = string.Empty;

    public string? lname
    { get; set; } = string.Empty;

    public string? email
    { get; set; } = string.Empty;

    public string? password
    { get; set; } = string.Empty;

    public int isManager
    { get; set; } = 0;

    //Class overloaders for User
    public User(int id)
    {
        this.id = id;
        this.username = username;
        this.fname = fname;
        this.lname = lname;
        this.email = email;
        this.password = password;
        this.isManager = isManager;
    }

    public User(int id, string fname, string lname, string email, int isManager)
    {
        this.id = id;
        this.username = username;
        this.fname = fname;
        this.lname = lname;
        this.email = email;
        this.password = password;
        this.isManager = isManager;
    }

    public User(string username, string fname, string lname, string email, string password, int isManager)
    {
        this.id = id;
        this.username = username;
        this.fname = fname;
        this.lname = lname;
        this.email = email;
        this.password = password;
        this.isManager = isManager;
    }

    public User(int id, string username, string fname, string lname, string email, string password, int isManager)
    {
        this.id = id;
        this.username = username;
        this.fname = fname;
        this.lname = lname;
        this.email = email;
        this.password = password;
        this.isManager = isManager;
    }
    [JsonConstructor]
    public User()
    {

    }

}