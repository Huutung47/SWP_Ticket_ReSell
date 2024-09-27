using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Request
{
    public int IdTicket { get; set; }

    public int IdCustomer { get; set; }

    public decimal? PriceWant { get; set; }

    public string? History { get; set; }

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Ticket IdTicketNavigation { get; set; } = null!;
}
