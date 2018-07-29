using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class StewardessService : ItemService<Stewardess>
    {
        public StewardessService()
        {
            _uri += "stewardesses";
        }
        public override bool Validate(Stewardess Item)
        {
            if(Item.Name == "" || Item.Surname == "")
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
