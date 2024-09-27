using System;
using System.Collections.Generic;
using System.Globalization;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public string? Email { get; set; }

    public decimal? AverageFeedback { get; set; }

    public int? IdRole { get; set; }

    public int? IdPackage { get; set; }

    public DateTime? PackageExpirationDate { get; set; }

    public virtual ICollection<Boxchat> BoxchatBuyers { get; set; } = new List<Boxchat>();

    public virtual ICollection<Boxchat> BoxchatSellers { get; set; } = new List<Boxchat>();

    public virtual Package? IdPackageNavigation { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

}
