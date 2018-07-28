using System;

namespace Academy11
    {
    public class Stewardess 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual int CrewId { get; set; }
    }
}
