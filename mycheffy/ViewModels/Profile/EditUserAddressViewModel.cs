using System;
using System.Reactive;
using System.Threading.Tasks;
using mycheffy.Services;
using mycheffy.Views.Popups;
using Newtonsoft.Json.Linq;
using Prism.Navigation;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Profile
{
    public class EditUserAddressViewModel : NavigationViewModelBase, INavigationAware
    {
        INavigationService _navigationService;

        public AddressService addressServices = new AddressService();

        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

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

        public EditUserAddressViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Back = ReactiveCommand.CreateFromTask(ExecuteBack);
            Save = ReactiveCommand.CreateFromTask(ExecuteSave);
        }

        public ReactiveCommand<Unit, Unit> Back { get; set; }
        public ReactiveCommand<Unit, Unit> Save { get; set; }

        private async Task ExecuteBack()
        {
            await _navigationService.GoBackAsync();
        }

        private async Task ExecuteSave()
        {
            SaveNewAddress(Id);
        }

        private async void SaveNewAddress(int id)
        {
            PopupActivityIndicator popupActivityIndicator;
            popupActivityIndicator = new PopupActivityIndicator();
            await PopupNavigation.PushAsync(popupActivityIndicator, false);

            Name = Preferences.Get("FullName", "Harn");
            Latitude = 1.0;
            Longitude = 2.0;
            PhoneNumber = "123456789";
            IsPrimary = 1;

            var response = await addressServices.UpdateUserAddress(id,
                                                    Type,
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
                await Application.Current.MainPage.DisplayAlert("New Address", "Address updated successfully.", "OK");
                await _navigationService.NavigateAsync("ManageAddressPage");
            }
            else
            {
                var errorObj = JObject.Parse(response.Error.Content);

#if DEBUG
                Console.WriteLine("Error Obj: \n" + errorObj["error"]);
#endif

                await App.Current.MainPage.DisplayAlert("Update Address Error", "Failed to update address", "OK");
            }

            popupActivityIndicator.Pop();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var addressId = (int)parameters["AddressId"];
            var type = (string)parameters["Type"];
            var address1 = (string)parameters["Address1"];
            var address2 = (string)parameters["Address2"];

            Id = addressId;
            Type = type;
            CompleteAddress = address1;
            FullGoogleAddress = address2;

            Console.WriteLine("Address Id: " + Id);
            Console.WriteLine("Type: " + Type);
            Console.WriteLine("Address1: " + CompleteAddress);
            Console.WriteLine("Address2: " + FullGoogleAddress);
        }

        public void OnNavigating(INavigationParameters paramaters)
        {
        }
    }
}
