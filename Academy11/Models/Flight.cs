using System;
using System.Collections.Generic;

namespace Academy11
{
    public class Flight
    {
        public int Number { get; set; }

        public string DepartureFrom { get; set; }

        public virtual DateTime TimeOfDeparture { get; set; }

        public string Destination { get; set; }

        public DateTime ArrivalTime { get; set; }

        public List<Ticket> Tickets { get; set; }

    }
}
