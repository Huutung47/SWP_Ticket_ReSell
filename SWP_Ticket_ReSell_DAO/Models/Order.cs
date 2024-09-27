using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int? IdTicket { get; set; }

    public string? PaymentMethod { get; set; }

    public string? Seller { get; set; }

    public string? Buyer { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public DateTime? ShippingTime { get; set; }

    public DateTime? OrderTime { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Ticket? IdTicketNavigation { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
