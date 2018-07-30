using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Academy11
{

    public sealed partial class StewardessLogic : Page
    {
        public StewardessLogic()
        {
            StewardessService = new StewardessService();
            this.InitializeComponent();
        }


        public StewardessService StewardessService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await StewardessService.UpdateList();
        }

        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await StewardessService.RemoveElem(StewardessService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isNumber = int.TryParse(formCrewId.SelectedValue.ToString(), out int crewId);
            if (isNumber && formDateOfBirth.Date.HasValue)
            {
                Stewardess f = new Stewardess()
                {
                    Name = formName.Text,
                    Surname = formSurname.Text,
                    CrewId = (crewId),
                    DateOfBirth = formDateOfBirth.Date.Value.Date
                };
                if (StewardessService.Validate(f))
                {
                    Form.Visibility = Visibility.Collapsed;
                    if (FormTitle.Text == "New Stewardess")
                    {
                        if (!await StewardessService.Add(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit Stewardess")
                    {
                        if (!await StewardessService.Update(f))
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
            FormTitle.Text = "Edit Stewardess";
            formName.Text = StewardessService.SelectedItem.Name;
            formSurname.Text = StewardessService.SelectedItem.Surname.ToString();
            formCrewId.SelectedValue = StewardessService.SelectedItem.CrewId.ToString();
            formDateOfBirth.Date = StewardessService.SelectedItem.DateOfBirth;
        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New Stewardess";
            formSurname.Text = "";
            formName.Text = "";
            formDateOfBirth.Date = null;
            formCrewId.SelectedValue = "";
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (FormTitle.Text == "Edit Ticket")
                Form.Visibility = Visibility.Collapsed;
            StewardessService.SelectedItem = ((Stewardess)Stewardesss.SelectedItem);
            if (StewardessService.SelectedItem == null)
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
