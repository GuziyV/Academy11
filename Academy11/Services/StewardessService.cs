using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            CrewService = new CrewService();

            var querry = (CrewService.GetAll()).Result.Select(f => f.Id);

            CrewIds = new ObservableCollection<int>(querry);
        }

        public CrewService CrewService { get; set; }

        public static bool Validate(Stewardess Item)
        {
            if(Item.Name == "" || Item.Surname == "")
            {
                return false;
            }
            return true;
        }
        public ObservableCollection<int> CrewIds { get; set; }


        protected override int GetSelectedId()
        {
            return SelectedItem.Id;
        }
    }
}
