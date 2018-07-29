using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Academy11
{

    public sealed partial class TicketLogic : Page
    {
        public TicketLogic()
        {
            TicketService = new TicketService();
            this.InitializeComponent();
        }


        public TicketService TicketService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await TicketService.UpdateList();
        }

        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await TicketService.RemoveElem(TicketService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            bool isNumber = int.TryParse(formPrice.Text, out int price);
            bool isNumber2 = int.TryParse(formFlightNumber.Text, out int flightNumber);
            if (isNumber && isNumber2)
            {
                Ticket f = new Ticket()
                {
                    FlightNumber = flightNumber,
                    Price = price
                };
                if (TicketService.Validate(f))
                {
                    Form.Visibility = Visibility.Collapsed;
                    if (FormTitle.Text == "New Ticket")
                    {
                        if (!await TicketService.Add(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit Ticket")
                    {
                        if (!await TicketService.Update(f))
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
            FormTitle.Text = "Edit Ticket";
            formPrice.Text = TicketService.SelectedItem.Price.ToString();
            formFlightNumber.Text = TicketService.SelectedItem.FlightNumber.ToString();
        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New Ticket";
            formPrice.Text = "";
            formFlightNumber.Text = "";
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (FormTitle.Text == "Edit Ticket")
                Form.Visibility = Visibility.Collapsed;
            TicketService.SelectedItem = ((Ticket)Tickets.SelectedItem);
            if (TicketService.SelectedItem == null)
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
