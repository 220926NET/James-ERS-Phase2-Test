using System.Text.Json.Serialization;

namespace Models;


public class Ticket
{
    public int submittedBy
    { get; set; }

    public int tid
    { get; set; }

    public decimal amountReq
    { get; set; }

    public string? reviewedStat
    { get; set; }

    public string? submitDesc
    { get; set; }

    public Ticket(int submittedBy, int tid, decimal amountReq, string submitDesc, string reviewedStat)
    {
        this.submittedBy = submittedBy;
        this.tid = tid;
        this.amountReq = amountReq;
        this.submitDesc = submitDesc;
        this.reviewedStat = reviewedStat;
    }
    public Ticket(int submittedBy, decimal amountReq, string submitDesc, string reviewedStat)
    {
        this.submittedBy = submittedBy;
        this.amountReq = amountReq;
        this.submitDesc = submitDesc;
        this.reviewedStat = reviewedStat;
    }

    public Ticket(int tid, int submittedBy, string reviewedStat)
    {
        this.tid = tid;
        this.submittedBy = submittedBy;
        this.reviewedStat = reviewedStat;
    }

    public Ticket(int tid, string reviewedStat)
    {
        this.tid = tid;
        this.reviewedStat = reviewedStat;
    }
    [JsonConstructor]
    public Ticket()
    {

    }

}