using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class FlightService : ItemService<Flight>
    {

        public FlightService()
        {
            _uri += "flights";
            TicketService = new TicketService();
        }

        public TicketService TicketService { get; set; }

        public override bool Validate(Flight f)
        {
            if (f.DepartureFrom == "" || f.Destination == "")
            {
                return false;
            }
            foreach (Ticket s in f.Tickets)
            {
                if (!TicketService.Validate(s))
                {
                    return false;
                }
            }
            return true;
        }

        protected override int GetSelectedId()
        {
            return SelectedItem.Number;
        }

    }
}
