using System;

namespace Academy11
{
    public class Plane 
    {
        public int Id { get; set; }

        public PlaneType PlaneType { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual TimeSpan LifeTime
        {
            get
            {
                return DateTime.Now - ReleaseDate;
            }
        }
    }
}
