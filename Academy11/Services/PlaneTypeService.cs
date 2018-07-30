using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy11;

namespace Academy11.Services
{
    public class PlaneTypeService : ItemService<PlaneType>
    {
        public PlaneTypeService()
        {
            _uri += "planetypes";
        }
        public static bool Validate(PlaneType Item)
        {
            if(Item.Model == null)
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
