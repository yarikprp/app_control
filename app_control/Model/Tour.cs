using System;
using System.Collections.Generic;

namespace app_control.Model;

public partial class Tour
{
    public int IdTours { get; set; }

    public DateOnly TourDate { get; set; }

    public TimeOnly TourTime { get; set; }

    public int? MaxCapacity { get; set; }

    public int? BookedTickets { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
