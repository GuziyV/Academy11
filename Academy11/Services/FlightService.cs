using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace Academy11.Services
{
    public class FlightService
    {
        public ObservableCollection<Flight> Flights;

        public FlightService()
        {
            SelectedItem = new Flight();
            if(_httpClient == null)
            {
                HttpClientHandler hch = new HttpClientHandler();
                hch.Proxy = null;
                hch.UseProxy = false;
                _httpClient = new HttpClient(hch);
            }
            if (Flights == null)
            {
                Flights = new ObservableCollection<Flight>(GetAll().Result);
            }
        }
        private HttpClient _httpClient;

        private string _uri = App.apiUrl + "flights";

        public Flight SelectedItem { get; set; } 


        public async Task<IEnumerable<Flight>> GetAll()
        {     
            string result = await _httpClient.GetStringAsync(_uri).ConfigureAwait(false);
            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Flight>>(result));       
        }

    }
}
