using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Package
{
    public int IdPackage { get; set; }

    public string? NamePackage { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
