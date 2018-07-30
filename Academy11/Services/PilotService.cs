using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class PilotService : ItemService<Pilot>
    {
        public PilotService()
        {
            _uri += "pilots";
        }

        public static bool Validate(Pilot f)
        {
            if (f.Name == "" || f.Surname =="")
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
