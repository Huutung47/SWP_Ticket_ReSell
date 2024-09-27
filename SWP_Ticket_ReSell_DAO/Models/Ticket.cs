using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Ticket
{
    public int IdTicket { get; set; }

    public int? IdCustomer { get; set; }

    public decimal? Price { get; set; }

    public string? TicketCategory { get; set; }

    public bool? TicketType { get; set; }

    public string? Buyer { get; set; }

    public int? Quantity { get; set; }

    public string? TicketHistory { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Boxchat> Boxchats { get; set; } = new List<Boxchat>();

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
