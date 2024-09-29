using System;
using System.Collections.Generic;

namespace app_control.Model;

public partial class Ticket
{
    public int IdTickets { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int? IdTours { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? IdTicketType { get; set; }

    public virtual TicketType? IdTicketTypeNavigation { get; set; }

    public virtual Tour? IdToursNavigation { get; set; }

}
