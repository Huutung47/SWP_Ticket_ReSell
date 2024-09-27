using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Boxchat
{
    public int IdBoxchat { get; set; }

    public int? IdTicket { get; set; }

    public int? SellerId { get; set; }

    public int? BuyerId { get; set; }

    public string? ChatContent { get; set; }

    public virtual Customer? Buyer { get; set; }

    public virtual Ticket? IdTicketNavigation { get; set; }

    public virtual Customer? Seller { get; set; }
}
