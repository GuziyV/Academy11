using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy11
{
    public class Crew 
    {
        public int Id { get; set; }

        public  Pilot Pilot { get; set; }

        public  List<Stewardess> Stewardesses { get; set; }
    }
}
