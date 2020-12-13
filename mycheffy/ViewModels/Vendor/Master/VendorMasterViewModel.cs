
using mycheffy.Models;
using mycheffy.Views.ContentViews.Payments;
using mycheffy.Views.ContentViews.Profile;
using mycheffy.Views.ContentViews.Vendor.Home;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Vendor.Master
{
    public class VendorMasterViewModel : NavigationViewModelBase, INavigationAware
    {
        [Reactive]
        public ContentView Content { get; set; }
        [Reactive]
        public ReactiveCommand<string, Unit> SwitchCommand { get; set; }
        [Reactive]
        public string HomeColor { get; set; }
        [Reactive]
        public string MessagesColor { get; set; }
        [Reactive]
        public string WalletColor { get; set; }
        [Reactive]
        public string NotificationColor { get; set; }
        [Reactive]
        public string ProfileColor { get; set; }

        [Reactive]
        public string HomeLabel { get; set; }
        [Reactive]
        public string MessagesLabel { get; set; }
        [Reactive]
        public string WalletLabel { get; set; }
        [Reactive]
        public string NotificationLabel { get; set; }
        [Reactive]
        public string ProfileLabel { get; set; }

        private string defaultColor = "#87859A";
        private string selectedColor = "#D80027";

        private INavigationService _navigationService;


        public VendorMasterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Content = new VendorHomeContentView();
            HomeColor = selectedColor;
            MessagesColor = defaultColor;
            WalletColor = defaultColor;
            NotificationColor = defaultColor;
            ProfileColor = defaultColor;

            HomeLabel = "Home";
            MessagesLabel = "Messages";
            WalletLabel = "Wallet";
            NotificationLabel = "Notifications";
            ProfileLabel = "Profile";

            ReactiveCommand<string, Unit> footerCommand = ReactiveCommand.Create<string>((itemSelected) =>
            {
                SwitchView(itemSelected);
            });
            SwitchCommand = footerCommand;
        }

        public void SwitchView(string itemSelected)
        {
            ResetColor();
            if (String.Equals(itemSelected, "Home"))
            {
                Content = new VendorHomeContentView();
                HomeColor = selectedColor;
            }
            else if (String.Equals(itemSelected, "Messages"))
            {
                Content = new VendorHomeContentView();
                MessagesColor = selectedColor;
            }
            else if (String.Equals(itemSelected, "Wallet"))
            {
                Content = new PaymentsContentView();
                WalletColor = selectedColor;
            }
            else if (String.Equals(itemSelected, "Notifications"))
            {
                Content = new VendorHomeContentView();
                NotificationColor = selectedColor;
            }
            else if (String.Equals(itemSelected, "Profile"))
            {
                Content = new ProfileContentView();
                ProfileColor = selectedColor;
            }
        }

        private void ResetColor()
        {
            HomeColor = defaultColor;
            MessagesColor = defaultColor;
            WalletColor = defaultColor;
            NotificationColor = defaultColor;
            ProfileColor = defaultColor;
        }
    }
}
