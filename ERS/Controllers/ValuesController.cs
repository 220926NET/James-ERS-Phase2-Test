using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net.Sockets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERS.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ERSController : ControllerBase
    {
        // logger to catch errors (had to add it myself this time)
        private readonly ILogger<ERSController> _logger;
        public ERSController(ILogger<ERSController> logger)
        {
            _logger = logger;
        }

        // None of this is working

        //POST Register Unavailable Username
        //POST Register New User
        //POST Register New User api/register/
        [HttpPost("register/")]
        public ActionResult PostRegister([FromBody] User user)
        {
            bool usernameAvailable = false;
            usernameAvailable = Services.RegisterAPI(user);
            if (usernameAvailable == false) { return BadRequest("Username already taken."); }
            else { return Created("User registered successfully.", user); }

        }


        //POST Login - Wrong Credentials
        //POST Login - Right Credentials
        // POST api/login/
        [HttpPost("login/")]
        public ActionResult PostLogin([FromBody] User user)
        {
            bool authenticationSuccessful = false;
            authenticationSuccessful = Services.LoginAPI(user.username, user.password);
            if (authenticationSuccessful == false) { return Unauthorized("Wrong username and or password."); }
            else { return Accepted("Login successful."); }

        }


        //POST Submit Ticket - Missing Amount
        //POST Submit Ticket - Missing Description
        //POST Submit Ticket - Correct
        // POST api/submitticket/
        [HttpPost("submitticket/")]
        public ActionResult PostSubmitTicket([FromBody] Ticket ticket)
        {
            bool submitSuccessful = false;
            submitSuccessful = Services.CreateTicketAPI(ticket);
            if (submitSuccessful == false) { return BadRequest("Submission failed."); }
            else { return Created("Ticket registered successfully.", ticket); }
        }


        //GET View Pending Tickets
        // GET: api/pending
        [HttpGet("tickets/")]
        public ActionResult<List<Ticket>> GetPendingTickets(string status = "Pending")
        {
            List<Ticket> pendingTickets = new TicketService().getStatTickets(status);
            return Ok(pendingTickets);
        }


        //GET View My Previous Tickets
        // GET: api/mytickets/
        [HttpGet("mytickets/{id}")]
        public ActionResult<List<Ticket>> GetUserTickets(int id)
        {
           List<Ticket> usersTickets = new TicketService().getMyTickets(id);
           return Ok(usersTickets);
        }


        // PUT api/processticket/5
        [HttpPut("processticket/{tid}")]
        public ActionResult PutTicket(bool manager, int tid, string value)
        {
            bool updateSuccessful = false;
            updateSuccessful = Services.SetTicketAPI(manager, tid, value);
            if (updateSuccessful == false) { return BadRequest("Update failed."); }
            else { return Ok("Ticket updated successfully."); }

        }

    }

}
