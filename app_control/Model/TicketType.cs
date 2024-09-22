using System;
using System.Collections.Generic;

namespace app_control.Model;

public partial class TicketType
{
    public int IdTicketType { get; set; }

    public string? TicketType1 { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
