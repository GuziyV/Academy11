using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System;

namespace Academy11
{

    public sealed partial class PilotLogic : Page
    {
        public PilotLogic()
        {
            PilotService = new PilotService();
            this.InitializeComponent();
        }


        public PilotService PilotService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await PilotService.UpdateList();
        }

        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await PilotService.RemoveElem(PilotService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            bool isNumber = int.TryParse(formExperience.Text, out int n);
            if (isNumber)
            {
                Pilot f = new Pilot()
                {
                    Name = formName.Text,
                    Surname = formSurname.Text,
                    Experience = Int32.Parse(formExperience.Text)
                };
                if (PilotService.Validate(f))
                {
                    Form.Visibility = Visibility.Collapsed;
                    if (FormTitle.Text == "New Pilot")
                    {
                        if (!await PilotService.Add(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit Pilot")
                    {
                        if (!await PilotService.Update(f))
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
            FormTitle.Text = "Edit Pilot";
            formName.Text = PilotService.SelectedItem.Name;
            formSurname.Text = PilotService.SelectedItem.Surname.ToString();
            formExperience.Text = PilotService.SelectedItem.Experience.ToString();
        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New Pilot";
            formSurname.Text = "";
            formName.Text = "";
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (FormTitle.Text == "Edit Ticket")
                Form.Visibility = Visibility.Collapsed;
            PilotService.SelectedItem = ((Pilot)Pilots.SelectedItem);
            if (PilotService.SelectedItem == null)
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
