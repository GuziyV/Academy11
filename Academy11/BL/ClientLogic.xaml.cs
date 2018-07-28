using Academy11.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace Academy11
{

    public sealed partial class ClientLogic : Page
    {
        public ClientLogic()
        {
            this.InitializeComponent();
        }

        private void ShowFlights(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(FlightLogic));
        }

    }
}
