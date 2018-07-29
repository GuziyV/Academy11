using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class FlightService : INotifyPropertyChanged
    {
        public ObservableCollection<Flight> Flights { get; set; } = new ObservableCollection<Flight>();

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public FlightService()
        {
            SelectedItem = new Flight();
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
            }
        }
        private HttpClient _httpClient;

        private string _uri = App.apiUrl + "flights";

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Validate(Flight f)
        {
            if (f.DepartureFrom == "" || f.Destination == "")
            {
                return false;
            }
            return true;
        }

        public async Task UpdateList()
        {
            var newCollection = new ObservableCollection<Flight>(await GetAll());
            Flights.Clear();
            foreach(var item in newCollection)
            {
                Flights.Add(item);
            }
        }

        public Flight SelectedItem { get; set; }


        public async Task<IEnumerable<Flight>> GetAll()
        {     
            string result = await _httpClient.GetStringAsync(_uri).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Flight>>(result));       
        }

        public async Task RemoveElem(Flight flight)
        {
            string uri = _uri + '/' + SelectedItem.Number;
            await _httpClient.DeleteAsync(uri);
            await UpdateList();
        }

        public async Task<bool> Add(Flight f)
        {
            var json = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_uri, json);
            if (response.IsSuccessStatusCode)
            {
                await UpdateList();
                return true;
            }
            return false;

        }

        public async Task<bool> Update(Flight f)
        {
            var json = new StringContent(JsonConvert.SerializeObject(f), Encoding.UTF8, "application/json");
            string uri = _uri + '/'+ SelectedItem.Number;
            var response = await _httpClient.PutAsync(uri, json);
            if (response.IsSuccessStatusCode)
            {
                await UpdateList();
                return true;
            }
            return false;
        }

    }
}
