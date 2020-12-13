using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Prism.Navigation;

namespace mycheffy.ViewModels
{
    public class MapsViewModel : NavigationViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        public MapsViewModel(INavigationService navigationService)
        {
            Console.WriteLine("MapsViewModel Constructor");

            _navigationService = navigationService;

            Task.Run(() => this.StartListening()).GetAwaiter();
        }

        async Task StartListening()
        {
            if (CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(5), 10, true);

            CrossGeolocator.Current.PositionChanged += PositionChanged;
            CrossGeolocator.Current.PositionError += PositionError;
        }

        private void PositionChanged(object sender, PositionEventArgs e)
        {
            //If updating the UI, ensure you invoke on main thread
            var position = e.Position;
            var output = "Full: Lat: " + position.Latitude + " Long: " + position.Longitude;
            output += "\n" + $"Time: {position.Timestamp}";
            output += "\n" + $"Heading: {position.Heading}";
            output += "\n" + $"Speed: {position.Speed}";
            output += "\n" + $"Accuracy: {position.Accuracy}";
            output += "\n" + $"Altitude: {position.Altitude}";
            output += "\n" + $"Altitude Accuracy: {position.AltitudeAccuracy}";

#if DEBUG
            Console.WriteLine(output);
#endif
        }

        private void PositionError(object sender, PositionErrorEventArgs e)
        {
#if DEBUG
                Console.WriteLine(e.Error);
#endif
            //Handle event here for errors
        }

        async Task StopListening()
        {
            if (!CrossGeolocator.Current.IsListening)
                return;

            await CrossGeolocator.Current.StopListeningAsync();

            CrossGeolocator.Current.PositionChanged -= PositionChanged;
            CrossGeolocator.Current.PositionError -= PositionError;
        }
    }
}
