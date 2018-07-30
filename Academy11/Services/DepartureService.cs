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


            _uri += "departures";
        }

        public static bool Validate(Departure f)
        {
            if (PlaneService.Validate(f.Plane) && CrewService.Validate(f.Crew) && FlightService.Validate(f.Flight))
            {
                return true;
            }
            return false;
        }

        protected override int GetSelectedId()
        {
            return SelectedItem.Id;
        }

    }
}
