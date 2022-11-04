using Models;
using DataAccess;
using System.Net.Http.Headers;

namespace BusinessLayer;

public class Services
{
    public static bool isLoggedIn = false;
    public static bool manager = false;

    // Login Code

    //API Login Method
    public static bool LoginAPI(string username, string password)
    {
        bool isValid = false;
        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            isValid = new DatabaseAccess().TestPassword(username, password);
        }
        if (isValid == false) { return isValid; }
        else { 
            User? user = new DatabaseAccess().GetUser(username, password);
            isLoggedIn = true;
            if (user.isManager == 1) { manager = true; }

            return isValid; }

    }

    // Register a user - remember, you have to instantiate the containing class before using this method

    public static bool RegisterAPI(User user)
    {
        bool isAvailable = false;
        isAvailable = new DatabaseAccess().TestUsername(user.username);
        if (isAvailable == false)
        {
            return isAvailable;
        }
        else
        {
            new DataAccess.DatabaseAccess().AddUser(user);
            isAvailable = true;
            return isAvailable;
        }

    }

    // Create a Ticket - remember, you have to instantiate the containing class before using this method

    public static bool CreateTicketAPI(Ticket makeTicket)
    {
        bool notMissing = false;
        if (makeTicket.amountReq == 0) 
        { 
            return notMissing; 
        }
        else if(makeTicket.submitDesc == "" || makeTicket.submitDesc == "string" || makeTicket.submitDesc == null)
        {
            return notMissing;
        }
        else{
            makeTicket.reviewedStat = "Pending";
            new DataAccess.DatabaseAccess().AddTicket(makeTicket); 
            notMissing = true; 
            return notMissing; 
        }   

    }


    // Create a Ticket - remember, you have to instantiate the containing class before using this method

    public static bool SetTicketAPI(bool manager, int tid, string updatedStat)
    {
        bool areYouManager = false;
        if(manager == false) { return areYouManager; }
        else {
        Ticket reviseTicket = new Ticket(tid, updatedStat);
        new DataAccess.DatabaseAccess().UpdateTicket(reviseTicket);
            areYouManager = true;
        return areYouManager;
        }

    }

}
