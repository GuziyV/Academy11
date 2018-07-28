using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;

namespace Academy11
{

    public sealed partial class FlightLogic : Page
    {
        public FlightLogic()
        {
            FlightService = new FlightService();
            this.InitializeComponent();
        }
        private Flight SelectedFlight;


        public FlightService FlightService { get; set; }

        private void ShowFlights(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FlightLogic));
        }

        public void ShowSelectedItem(object sender, RoutedEventArgs e)
        {

            Number.Text = ((Flight)Flights.SelectedItem).Number.ToString();
            ArrivalTime.Text = ((Flight)Flights.SelectedItem).ArrivalTime.ToString();
            DepartureFrom.Text = ((Flight)Flights.SelectedItem).DepartureFrom.ToString();
            Destination.Text = ((Flight)Flights.SelectedItem).Destination.ToString();
            TimeOfDeparture.Text = ((Flight)Flights.SelectedItem).TimeOfDeparture.ToString();
            Detail.Visibility = Visibility.Visible;
        }

    }
}
