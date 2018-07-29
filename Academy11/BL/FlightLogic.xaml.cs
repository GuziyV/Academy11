using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Academy11
{

    public sealed partial class FlightLogic : Page
    {
        public FlightLogic()
        {
            FlightService = new FlightService();
            this.InitializeComponent();
        }


        public FlightService FlightService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await FlightService.UpdateList();
        }

        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await FlightService.RemoveElem(FlightService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (formArrivalTime.Date.HasValue && formTimeOfDeparture.Date.HasValue)
            {
                Flight f = new Flight()
                {
                    Destination = formDestination.Text,
                    DepartureFrom = formDepartureFrom.Text,
                    ArrivalTime = formArrivalTime.Date.Value.Date,
                    TimeOfDeparture = formTimeOfDeparture.Date.Value.Date
                };
                if (FlightService.Validate(f))
                {
                    Form.Visibility = Visibility.Collapsed;
                    if (FormTitle.Text == "New Flight")
                    {
                        if (!await FlightService.Add(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit Flight")
                    {
                        if (!await FlightService.Update(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                }
            }
                WrongInput.Visibility = Visibility.Visible;
        }

        public void ShowUpdateForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "Edit Flight";
            formArrivalTime.Date = FlightService.SelectedItem.ArrivalTime;
            formDepartureFrom.Text = FlightService.SelectedItem.DepartureFrom.ToString();
            formDestination.Text = FlightService.SelectedItem.Destination.ToString();
            formTimeOfDeparture.Date = FlightService.SelectedItem.TimeOfDeparture;
        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New Flight";
            formArrivalTime.Date = null;
            formDepartureFrom.Text = "";
            formDestination.Text = "";
            formTimeOfDeparture.Date = null;
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (FormTitle.Text == "Edit Ticket")
                Form.Visibility = Visibility.Collapsed;
            FlightService.SelectedItem = ((Flight)Flights.SelectedItem);
            if (FlightService.SelectedItem == null)
            {
                Detail.Visibility = Visibility.Collapsed;
                return;
            }
            Detail.Visibility = Visibility.Visible;
        }

        private void ShowFlights(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FlightLogic));
        }
        private void ShowPilots(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PilotLogic));
        }

        private void ShowPlanes(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlaneLogic));
        }

        private void ShowPlaneTypes(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlaneTypeLogic));
        }

        private void ShowStewardesses(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StewardessLogic));
        }

        private void ShowTickets(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TicketLogic));
        }

        private void ShowDepartures(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(DepartureLogic));
        }

        private void ShowCrews(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CrewLogic));
        }

    }
}
