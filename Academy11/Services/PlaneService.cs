using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class PlaneService : ItemService<Plane>
    {
        public PlaneService()
        {
            _uri += "planes";
        }

        public override bool Validate(Plane f)
        {
            if (f.PlaneType == null )
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
