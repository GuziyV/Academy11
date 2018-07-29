using Academy11.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Academy11
{

    public sealed partial class DepartureLogic : Page
    {
        public DepartureLogic()
        {
            DepartureService = new DepartureService();
            this.InitializeComponent();
        }

        public DepartureService DepartureService { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await DepartureService.UpdateList();
        }
        public async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Form.Visibility = Visibility.Collapsed;
            await DepartureService.RemoveElem(DepartureService.SelectedItem);
        }

        public async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isNumber = int.TryParse(formCrewId.Text, out int crewId);
            bool isNumber2 = int.TryParse(formExperience.Text, out int experience);
            bool isNumber3 = int.TryParse(formNumberOfSeats.Text, out int numberOfSeats);
            bool isNumber4 = int.TryParse(formLoadCapacity.Text, out int loadCapacity);
            if (formReleaseDate.Date.HasValue && isNumber && isNumber2 && isNumber3 && isNumber4
                && formArrivalTime.Date.HasValue && formTimeOfDeparture.Date.HasValue && formDateOfBirth.Date.HasValue
                && formTimeOfDeparture2.Date.HasValue)
            {
                Stewardess s = new Stewardess()
                {
                    Name = formStewardessName.Text,
                    Surname = formStewardessSurname.Text,
                    CrewId = crewId,
                    DateOfBirth = formDateOfBirth.Date.Value.Date
                };

                Pilot pil = new Pilot()
                {
                    Name = formPilotName.Text,
                    Surname = formPilotSurname.Text,
                    Experience = experience
                };

                Plane p = new Plane()
                {
                    ReleaseDate = formReleaseDate.Date.Value.Date,
                    PlaneType = new PlaneType()
                    {
                        Model = formModel.Text,
                        NumberOfSeats = numberOfSeats,
                        LoadCapacity = loadCapacity
                    }
                };
                Flight f = new Flight()
                {
                    Destination = formDestination.Text,
                    DepartureFrom = formDepartureFrom.Text,
                    ArrivalTime = formArrivalTime.Date.Value.Date,
                    TimeOfDeparture = formTimeOfDeparture.Date.Value.Date
                };



                Departure d = new Departure()
                {
                    Crew = new Crew()
                    {
                        Pilot = pil,
                        Stewardesses = new List<Stewardess>()
                        {
                            s
                        }
                    },
                    Flight = f,
                    Plane = p,
                    TimeOfDeparture = formTimeOfDeparture2.Date.Value.Date
                };

                if (DepartureService.Validate(d))
                {
                    Form.Visibility = Visibility.Collapsed;
                    if (FormTitle.Text == "New Departure")
                    {
                        if (!await DepartureService.Add(d))
                        {
                            WrongInput.Visibility = Visibility.Visible;
                        }
                        WrongInput.Visibility = Visibility.Collapsed;
                        return;
                    }
                    if (FormTitle.Text == "Edit Departure")
                    {
                        if (!await DepartureService.Update(d))
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
            FormTitle.Text = "Edit Departure";

            formTimeOfDeparture2.Date = DepartureService.SelectedItem.TimeOfDeparture;

            formReleaseDate.Date = DepartureService.SelectedItem.Plane.ReleaseDate;
            formModel.Text = DepartureService.SelectedItem.Plane.PlaneType.Model;
            formNumberOfSeats.Text = DepartureService.SelectedItem.Plane.PlaneType.NumberOfSeats.ToString();
            formLoadCapacity.Text = DepartureService.SelectedItem.Plane.PlaneType.LoadCapacity.ToString();

            formArrivalTime.Date = DepartureService.SelectedItem.Flight.ArrivalTime;
            formDepartureFrom.Text = DepartureService.SelectedItem.Flight.DepartureFrom.ToString();
            formDestination.Text = DepartureService.SelectedItem.Flight.Destination.ToString();
            formTimeOfDeparture.Date = DepartureService.SelectedItem.Flight.TimeOfDeparture;

        }

        public void ShowForm_Click(object sender, RoutedEventArgs e)
        {
            WrongInput.Visibility = Visibility.Collapsed;
            Form.Visibility = Visibility.Visible;
            FormTitle.Text = "New Departure";
            formTimeOfDeparture2.Date = null;
            formReleaseDate.Date = null;
            formModel.Text = "";
            formLoadCapacity.Text = "";
            formNumberOfSeats.Text = "";

            formReleaseDate.Date = null;
            formModel.Text = "";
            formLoadCapacity.Text = "";
            formNumberOfSeats.Text = "";

            formArrivalTime.Date = null;
            formDepartureFrom.Text = "";
            formDestination.Text = "";
            formTimeOfDeparture.Date = null;
        }

        public void ShowSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (FormTitle.Text == "Edit Departure")
                Form.Visibility = Visibility.Collapsed;
            DepartureService.SelectedItem = ((Departure)Departures.SelectedItem);
            if (DepartureService.SelectedItem == null)
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
