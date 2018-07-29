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
            PlaneTypeService = new PlaneTypeService();
        }

        private PlaneTypeService PlaneTypeService { get; set; }

        public override bool Validate(Plane f)
        {
            if (f.PlaneType == null || !PlaneTypeService.Validate(f.PlaneType) )
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
