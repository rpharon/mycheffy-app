using System;
using System.Reactive;
using System.Threading.Tasks;
using mycheffy.Services;
using mycheffy.Views.Pages.Profile;
using mycheffy.Views.Popups;
using Newtonsoft.Json.Linq;
using Prism.Navigation;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Profile
{
    public class AddNewAddressViewModel : NavigationViewModelBase, INavigationAware
    {
        INavigationService _navigationService;

        public AddressService addressServices = new AddressService();

        private string _type;
        public string Type
        {
            get => _type;
            set => this.RaiseAndSetIfChanged(ref _type, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _completeAddress;
        public string CompleteAddress
        {
            get => _completeAddress;
            set => this.RaiseAndSetIfChanged(ref _completeAddress, value);
        }

        private string _fullGoogleAddress;
        public string FullGoogleAddress
        {
            get => _fullGoogleAddress;
            set => this.RaiseAndSetIfChanged(ref _fullGoogleAddress, value);
        }

        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set => this.RaiseAndSetIfChanged(ref _latitude, value);
        }

        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set => this.RaiseAndSetIfChanged(ref _longitude, value);
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
        }

        private int _isPrimary;
        public int IsPrimary
        {
            get => _isPrimary;
            set => this.RaiseAndSetIfChanged(ref _isPrimary, value);
        }

        public AddNewAddressViewModel(INavigationService navigationService)
        {
            Console.WriteLine("AddNewAddressViewModel Constructor");

            _navigationService = navigationService;

            _type = string.Empty;
            _name = string.Empty;
            _completeAddress = string.Empty;
            _fullGoogleAddress = string.Empty;
            _latitude = 0;
            _longitude = 0;
            _phoneNumber = string.Empty;
            _isPrimary = 1;

            Back = ReactiveCommand.CreateFromTask(ExecuteBack);
            Save = ReactiveCommand.CreateFromTask(ExecuteSave);
            ViewMap = ReactiveCommand.CreateFromTask(ExecuteViewMap);
        }

        public ReactiveCommand<Unit, Unit> Back { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }
        public ReactiveCommand<Unit, Unit> ViewMap { get; set; }

        private async Task ExecuteBack()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task ExecuteSave()
        {
            SaveNewAddress();
        }

        public async Task ExecuteViewMap()
        {
            _navigationService.NavigateAsync("MapsPage");
        }

        private async void SaveNewAddress()
        {
            PopupActivityIndicator popupActivityIndicator;
            popupActivityIndicator = new PopupActivityIndicator();
            await PopupNavigation.PushAsync(popupActivityIndicator, false);

            Name = Preferences.Get("FullName", "Harn");
            Latitude = 1.0;
            Longitude = 2.0;
            PhoneNumber = "123456789";
            IsPrimary = 1;

            var response = await addressServices.AddNewUserAddress(Type,
                                                    Name,
                                                    CompleteAddress,
                                                    FullGoogleAddress,
                                                    Latitude,
                                                    Longitude,
                                                    PhoneNumber,
                                                    IsPrimary);
            Console.WriteLine("Type: " + Type);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("CompleteAddress: " + CompleteAddress);
            Console.WriteLine("FullGoogleAddress: " + FullGoogleAddress);
            Console.WriteLine("Latitude: " + Latitude);
            Console.WriteLine("Longitude: " + Longitude);
            Console.WriteLine("PhoneNumber: " + PhoneNumber);
            Console.WriteLine("IsPrimary: " + IsPrimary);

            if (response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("New Address", "Address added successfully.", "OK");
                await _navigationService.NavigateAsync("ManageAddressPage");
            }
            else
            {
                var errorObj = JObject.Parse(response.Error.Content);

#if DEBUG
                Console.WriteLine("Error Obj: \n" + errorObj["error"]);
#endif

                await App.Current.MainPage.DisplayAlert("Add New Address Error", "Failed to add new address", "OK");
            }

            popupActivityIndicator.Pop();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var address1 = parameters["Address2"] as string;
            FullGoogleAddress = address1;
        }

        public void OnNavigating(INavigationParameters paramaters)
        {
        }
    }
}
