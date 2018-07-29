using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Academy11
{

    public sealed partial class PlaneTypeLogic : Page
    {
        public PlaneTypeLogic()
        {
            PlaneTypeService = new PlaneTypeService();
            this.InitializeComponent();
        }


        public PlaneTypeService PlaneTypeService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await PlaneTypeService.UpdateList();
        }

        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await PlaneTypeService.RemoveElem(PlaneTypeService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isNumber = int.TryParse(formNumberOfSeats.Text, out int numberOfSeats);
            bool isNumber2 = int.TryParse(formLoadCapacity.Text, out int loadCapacity);
            if (isNumber && isNumber2)
            {
                PlaneType f = new PlaneType()
                {
                    Model = formModel.Text,
                    NumberOfSeats = numberOfSeats,
                    LoadCapacity = loadCapacity
                };
                if (PlaneTypeService.Validate(f))
                {
                    if (FormTitle.Text == "New PlaneType")
                    {
                        if (!await PlaneTypeService.Add(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit PlaneType")
                    {
                        if (!await PlaneTypeService.Update(f))
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
            FormTitle.Text = "Edit PlaneType";
            formModel.Text = PlaneTypeService.SelectedItem.Model;
            formNumberOfSeats.Text = PlaneTypeService.SelectedItem.NumberOfSeats.ToString();
            formLoadCapacity.Text = PlaneTypeService.SelectedItem.LoadCapacity.ToString();
        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New PlaneType";
            formModel.Text = "";
            formLoadCapacity.Text = "";
            formNumberOfSeats.Text = "";
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            PlaneTypeService.SelectedItem = ((PlaneType)PlaneTypes.SelectedItem);
            if (PlaneTypeService.SelectedItem == null)
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
        private void ShowPlaneTypes(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlaneTypeLogic));
        }

        private void ShowPlanes(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlaneLogic));
        }

        private void ShowPilots(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PilotLogic));
        }

        private void ShowStewardesses(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StewardessLogic));
        }

        private void ShowTickets(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TicketLogic));
        }


    }
}
