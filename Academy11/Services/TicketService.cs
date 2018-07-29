using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class TicketService : ItemService<Ticket>
    {
        public TicketService()
        {
            _uri += "tickets";
        }
        public override bool Validate(Ticket Item)
        {
            if (Item.FlightNumber <= 0)
            {
                return false;
            }
            return true;
        }

        protected override int GetSelectedId()
        {
            return SelectedItem.Id;
        }
    }
}
