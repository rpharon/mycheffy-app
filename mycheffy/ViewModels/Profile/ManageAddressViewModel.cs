using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Windows.Input;
using mycheffy.Models.Address;
using mycheffy.Services;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Profile
{
    public class ManageAddressViewModel : NavigationViewModelBase, INavigationAware
    {
        INavigationService _navigationService;

        public AddressService addressService = new AddressService();

        private int _id;
        public int Id
        {
            get => _id;
            set => this.RaiseAndSetIfChanged(ref _id, value);
        }

        [ObservableAsProperty]
        public IEnumerable<AddressModel> AddressList { get; }

        public ManageAddressViewModel(INavigationService navigationService)
        {
            Console.WriteLine("ManageAddressViewModel Constructor");

            _navigationService = navigationService;

            GetAllUserAddress();

            Back = ReactiveCommand.CreateFromTask(ExecuteBack);
        }

        public ReactiveCommand<Unit, Unit> Back { get; set; }
        public ReactiveCommand<Unit, Unit> Delete { get; set; }

        private async Task ExecuteBack()
        {
            await _navigationService.GoBackAsync();
        }

        public async Task NavigateToAddNewAddress()
        {
            await _navigationService.NavigateAsync("AddNewAddressPage");
        }

        public async Task GetAllUserAddress()
        {
            addressService.GetAllUserAddress()
                .ToObservable()
                .ToPropertyEx(this, x => x.AddressList);
        }

        public async Task DeleteUserAddress(int id)
        {
            await addressService.DeleteUserAddress(id);
            GetAllUserAddress();
        }

        public async Task UpdateUserAddress(int id, string type, string address1, string address2)
        {
            var par = new NavigationParameters();
            par.Add("AddressId", id);
            par.Add("Type", type);
            par.Add("Address1", address1);
            par.Add("Address2", address2);

            await _navigationService.NavigateAsync("EditUserAddressPage", par);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public void OnNavigating(INavigationParameters paramaters)
        {
        }
    }
}
