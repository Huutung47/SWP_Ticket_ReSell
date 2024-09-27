using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Report
{
    public int IdCustomer { get; set; }

    public int IdOrder { get; set; }

    public string? Comment { get; set; }

    public virtual Customer IdCustomerNavigation { get; set; } = null!;

    public virtual Order IdOrderNavigation { get; set; } = null!;
}
