using System;

namespace Academy11
{
    public class Departure
    {
        public int Id { get; set; }

        public  Flight Flight { get; set; }
        
        public  DateTime TimeOfDeparture { get; set; }

        public  Crew Crew { get; set; }

        public  Plane Plane { get; set; }
    }
}
