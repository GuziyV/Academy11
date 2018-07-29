using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Academy11
{

    public sealed partial class PlaneLogic : Page
    {
        public PlaneLogic()
        {
            PlaneService = new PlaneService();
            this.InitializeComponent();
        }

        public PlaneService PlaneService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await PlaneService.UpdateList();
        }
        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await PlaneService.RemoveElem(PlaneService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isNumber = int.TryParse(formNumberOfSeats.Text, out int numberOfSeats);
            bool isNumber2 = int.TryParse(formLoadCapacity.Text, out int loadCapacity);
            if (formReleaseDate.Date.HasValue && isNumber && isNumber2)
            {
                Plane f = new Plane()
                {
                    ReleaseDate = formReleaseDate.Date.Value.Date,
                    PlaneType = new PlaneType()
                    {
                        Model = formModel.Text,
                        NumberOfSeats = numberOfSeats,
                        LoadCapacity = loadCapacity
                    }
                };
                if (PlaneService.Validate(f))
                {
                    Form.Visibility = Visibility.Collapsed;
                    if (FormTitle.Text == "New Plane")
                    {
                        if (!await PlaneService.Add(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit Plane")
                    {
                        if (!await PlaneService.Update(f))
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
            FormTitle.Text = "Edit Plane";
            formReleaseDate.Date = PlaneService.SelectedItem.ReleaseDate;
            formModel.Text = PlaneService.SelectedItem.PlaneType.Model;
            formNumberOfSeats.Text = PlaneService.SelectedItem.PlaneType.NumberOfSeats.ToString();
            formLoadCapacity.Text = PlaneService.SelectedItem.PlaneType.LoadCapacity.ToString();
        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New Plane";
            formReleaseDate.Date = null;
            formModel.Text = "";
            formLoadCapacity.Text = "";
            formNumberOfSeats.Text = "";
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (FormTitle.Text == "Edit Ticket")
                Form.Visibility = Visibility.Collapsed;
            PlaneService.SelectedItem = ((Plane)Planes.SelectedItem);
            if (PlaneService.SelectedItem == null)
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
