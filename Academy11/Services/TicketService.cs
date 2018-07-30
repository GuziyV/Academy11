using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            FlightService = new FlightService();

            var querry = (FlightService.GetAll()).Result.Select(f => f.Number);

            FlightsIds = new ObservableCollection<int>(querry);
        }

        public FlightService FlightService { get; set; }

        public ObservableCollection<int> FlightsIds { get; set; }


        public static bool Validate(Ticket Item)
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
