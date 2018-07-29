using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Academy11
{

    public sealed partial class CrewLogic : Page
    {
        public CrewLogic()
        {
            CrewService = new CrewService();
            this.InitializeComponent();
        }

        public CrewService CrewService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await CrewService.UpdateList();
        }
        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await CrewService.RemoveElem(CrewService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            bool isNumber = int.TryParse(formCrewId.Text, out int crewId);
            bool isNumber2 = int.TryParse(formExperience.Text, out int experience);
            if (isNumber && isNumber2 && formDateOfBirth.Date.HasValue)
            {
                Stewardess s = new Stewardess()
                {
                    Name = formStewardessName.Text,
                    Surname = formStewardessSurname.Text,
                    CrewId = crewId,
                    DateOfBirth = formDateOfBirth.Date.Value.Date
                };
                Pilot p = new Pilot()
                {
                    Name = formPilotName.Text,
                    Surname = formPilotSurname.Text,
                    Experience = experience
                };
                Crew f = new Crew()
                {
                    Pilot = p,
                    Stewardesses = new List<Stewardess>()
                    {
                        s
                    }
                };
                if (CrewService.Validate(f))
                {
                    Form.Visibility = Visibility.Collapsed;
                    if (FormTitle.Text == "New Crew")
                    {
                        if (!await CrewService.Add(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit Crew")
                    {
                        if (!await CrewService.Update(f))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                }
                WrongInput.Visibility = Visibility.Visible;
            }
        }

        public void ShowUpdateForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "Edit Crew";
            formPilotSurname.Text = CrewService.SelectedItem.Pilot.Surname;
            formPilotName.Text = CrewService.SelectedItem.Pilot.Name;
            formExperience.Text = CrewService.SelectedItem.Pilot.Experience.ToString() ;
            formDateOfBirth.Date = CrewService.SelectedItem.Stewardesses[0].DateOfBirth;
            formStewardessName.Text = CrewService.SelectedItem.Stewardesses[0].Name;
            formStewardessSurname.Text = CrewService.SelectedItem.Stewardesses[0].Surname;
            formCrewId.Text = CrewService.SelectedItem.Stewardesses[0].CrewId.ToString();
        }


        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New Crew";
            formPilotSurname.Text = "";
            formPilotName.Text = "";
            formExperience.Text = "";
            formDateOfBirth.Date = null;
            formStewardessName.Text = "";
            formStewardessSurname.Text = "";
            formCrewId.Text = "";
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (FormTitle.Text == "Edit Ticket")
                Form.Visibility = Visibility.Collapsed;
            CrewService.SelectedItem = ((Crew)Crews.SelectedItem);
            if (CrewService.SelectedItem == null)
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
