using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class CrewService : ItemService<Crew>
    {

        public CrewService()
        {
            PilotService = new PilotService();
            StewardessService = new StewardessService();
            _uri += "crews";
        }


        public PilotService PilotService { get; set; }
        public StewardessService StewardessService { get; set; }

        public override bool Validate(Crew f)
        {
            if (f.Pilot == null || f.Stewardesses == null || !PilotService.Validate(f.Pilot))
            {
                return false;
            }
            if (f.Stewardesses == null) return false; 
            foreach(Stewardess s in f.Stewardesses)
            {
                if(!StewardessService.Validate(s))
                {
                    return false;
                }
            }
            return true;
        }

        protected override int GetSelectedId()
        {
            return SelectedItem.Id;
        }

    }
}
