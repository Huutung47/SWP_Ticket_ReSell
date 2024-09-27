using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Notification
{
    public int IdNotification { get; set; }

    public string? Title { get; set; }

    public string? Event { get; set; }

    public DateTime? OrganizationDay { get; set; }

    public TimeSpan? OrganizingTime { get; set; }

    public int? IdTicket { get; set; }

    public virtual Ticket? IdTicketNavigation { get; set; }
}
