using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Role
{
    public int IdRole { get; set; }

    public string? NameRole { get; set; }

    public string? Role1 { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
