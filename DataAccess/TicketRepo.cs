using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess;

public class TicketRepo : ITicketStorage
{

    private SqlConnectionFactory _factory;
    public TicketRepo()
    {
        _factory = new SqlConnectionFactory();
    }

    // Implement getAllTickets
    public List<Ticket> getAllTickets()
    {
        List<Ticket> ticketlist = new();

        using SqlConnection conn = _factory.GetConnection();
        conn.Open();

        using SqlCommand command = new SqlCommand("SELECT * FROM Tickets;", conn);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                int id = (int)reader["submitted_by"];
                int ticketid = (int)reader["id"];
                decimal amount = (decimal)reader["amount_requested"];
                string desc = (string)reader["submission_desc"];
                string reviwedstatus = (string)reader["reviewed_status"];

                Ticket singleticket = new Ticket(id, ticketid, amount, desc, reviwedstatus)
                {
                    submittedBy = id,
                    tid = ticketid,
                    amountReq = Math.Round(amount, 2),
                    submitDesc = desc,
                    reviewedStat = reviwedstatus
                };

                ticketlist.Add(singleticket);
            }
        }

        return ticketlist;
    }

    // Implement getMyTickets
    public List<Ticket> getMyTickets(int iduser)
    {
        List<Ticket> ticketlist = new();

        using SqlConnection conn = _factory.GetConnection();
        conn.Open();

        using SqlCommand command = new SqlCommand("SELECT * FROM Tickets WHERE submitted_by = @iduser;", conn);
        command.Parameters.AddWithValue("@iduser", iduser);
        using SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                int id = (int)reader["submitted_by"];
                int ticketid = (int)reader["id"];
                decimal amount = (decimal)reader["amount_requested"];
                string desc = (string)reader["submission_desc"];
                string reviwedstatus = (string)reader["reviewed_status"];

                Ticket singleticket = new Ticket(id, ticketid, amount, desc, reviwedstatus)
                {
                    submittedBy = id,
                    tid = ticketid,
                    amountReq = Math.Round(amount, 2),
                    submitDesc = desc,
                    reviewedStat = reviwedstatus
                };

                ticketlist.Add(singleticket);
            }
        }

        return ticketlist;
    }

    // Implement getStatTickets
    public List<Ticket> getStatTickets(string stat)
    {
        List<Ticket> ticketlist = new();

        using SqlConnection conn = _factory.GetConnection();
        conn.Open();

        using SqlCommand command = new SqlCommand("SELECT * FROM Tickets WHERE reviewed_status = @stat;", conn);

        command.Parameters.AddWithValue("@stat", stat);

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                int id = (int)reader["submitted_by"];
                int ticketid = (int)reader["id"];
                decimal amount = (decimal)reader["amount_requested"];
                string desc = (string)reader["submission_desc"];
                string reviwedstatus = (string)reader["reviewed_status"];

                Ticket singleticket = new Ticket(id, ticketid, amount, desc, reviwedstatus)
                {
                    submittedBy = id,
                    tid = ticketid,
                    amountReq = Math.Round(amount, 2),
                    submitDesc = desc,
                    reviewedStat = reviwedstatus
                };

                ticketlist.Add(singleticket);
            }
        }

        return ticketlist;
    }

}