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

        private void ShowFlights_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FlightLogic));
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            FlightService.SelectedItem = ((Flight)Flights.SelectedItem);
            if(FlightService.SelectedItem == null)
            {
                Detail.Visibility = Visibility.Collapsed;
                return;
            }
            Number.Text = FlightService.SelectedItem.Number.ToString();
            ArrivalTime.Text = FlightService.SelectedItem.ArrivalTime.ToString();
            DepartureFrom.Text = FlightService.SelectedItem.DepartureFrom.ToString();
            Destination.Text = FlightService.SelectedItem.Destination.ToString();
            TimeOfDeparture.Text = FlightService.SelectedItem.TimeOfDeparture.ToString();
            Detail.Visibility = Visibility.Visible;
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            FlightService.RemoveElem(FlightService.SelectedItem);
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Visible;
        }

    }
}
