﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Boxchat
{
    public int ID_Boxchat { get; set; }

    public int? ID_Ticket { get; set; }

    public int? Seller_ID { get; set; }

    public int? Buyer_ID { get; set; }

    public string Chat_content { get; set; }

    public virtual Ticket ID_TicketNavigation { get; set; }
}