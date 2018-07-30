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
            _uri += "crews";
        }


        public static bool Validate(Crew f)
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
