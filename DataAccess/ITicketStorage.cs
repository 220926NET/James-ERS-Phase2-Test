using Models;

namespace DataAccess;

public interface ITicketStorage
{
    /// <summary>
    /// Returns all tickets
    /// </summary>
    /// <returns>List of Tickets object</returns>
    List<Ticket> getAllTickets();

    /// <summary>
    /// Returns only User's tickets
    /// <summary>
    /// <returns>List of Tickets object</returns>
    List<Ticket> getMyTickets(int iduser);

    /// <summary>
    /// Returns only Pending tickets
    /// <summary>
    /// <returns>List of Tickets object</returns
    List<Ticket> getStatTickets(string stat);

}