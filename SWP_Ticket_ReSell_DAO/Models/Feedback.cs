using System;
using System.Collections.Generic;

namespace SWP_Ticket_ReSell_DAO.Models;

public partial class Feedback
{
    public int IdFeedback { get; set; }

    public int? IdOrder { get; set; }

    public string? Comment { get; set; }

    public int? Stars { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }
}
