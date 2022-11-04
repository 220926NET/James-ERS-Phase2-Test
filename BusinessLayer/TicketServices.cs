using Models;
using DataAccess;
using Microsoft.Data.SqlClient;

namespace BusinessLayer;
public class TicketService
{
    private TicketRepo _repo;
    public TicketService()
    { _repo = new TicketRepo(); }

    /// <summary>
    /// Returns tickets based on various criteria
    /// </summary>

    public List<Ticket> getAllTickets()

    {
        List<Ticket> allTickets = _repo.getAllTickets();

        return allTickets;
    }

    public List<Ticket> getMyTickets(int iduser)

    {
        List<Ticket> allTickets = _repo.getMyTickets(iduser);

        return allTickets;
    }

    public List<Ticket> getStatTickets(string stat)

    {
        List<Ticket> allTickets = _repo.getStatTickets(stat);

        return allTickets;
    }

}