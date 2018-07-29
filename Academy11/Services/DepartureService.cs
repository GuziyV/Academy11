using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class DepartureService : ItemService<Departure>
    {

        public DepartureService()
        {
            PlaneService = new PlaneService();
            CrewService = new CrewService();
            FlightService = new FlightService();

            _uri += "flights";
        }
        public PlaneService PlaneService { get; set; }
        public CrewService CrewService { get; set; }
        public FlightService FlightService { get; set; }

        public override bool Validate(Departure f)
        {
            if (PlaneService.Validate(f.Plane) && CrewService.Validate(f.Crew) && FlightService.Validate(f.Flight))
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
