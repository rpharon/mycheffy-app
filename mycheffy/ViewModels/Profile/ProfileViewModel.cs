using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using mycheffy.Models.Profile;
using mycheffy.Models.Shared.Enums;
using mycheffy.Service;
using mycheffy.Services.Utilities;
using mycheffy.Views.Popups;
using Newtonsoft.Json.Linq;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Profile
{
    public class ProfileViewModel : NavigationViewModelBase
    {
        private readonly NavigationEventService _navigationService;

        public class Profile
        {
            public string Icon { get; set; }
            public string Name { get; set; }
        }

        readonly List<Profile> customerProfileList = new List<Profile>()
        {
          new Profile
            {
                Icon = "orders.png",
                Name = "My Orders"
            },
            new Profile
            {
                Icon = "home.png",
                Name = "Manage Addresses"
            },
            new Profile
            {
                Icon = "payments.png",
                Name = "Payments"
            },
            new Profile
            {
                Icon = "favourite.png",
                Name = "Favourites"
            },
            new Profile
            {
                Icon = "help.png",
                Name = "Help"
            },
            new Profile
            {
                Icon = "logout.png",
                Name = "Logout"
            }
        };

        readonly List<Profile> vendorProfileList = new List<Profile>()
        {
          new Profile
            {
                Icon = "orders.png",
                Name = "Track Orders"
            },
            new Profile
            {
                Icon = "home.png",
                Name = "Manage Address"
            },
            new Profile
            {
                Icon = "payments.png",
                Name = "Payments"
            },
            new Profile
            {
                Icon = "favourite.png",
                Name = "Manage Delivery"
            },
            new Profile
            {
                Icon = "help.png",
                Name = "Help"
            },
            new Profile
            {
                Icon = "logout.png",
                Name = "Logout"
            }
        };

        public List<Profile> ProfileList { get; set; }

        [Reactive]
        public String FullName { get; set; }
        [Reactive]
        public String Number { get; set; }
        [Reactive]
        public String Email { get; set; }


        public ProfileModel profile;
        public ProfileViewModel(NavigationEventService navigationService, ProfileService profileService)
        {
            Console.WriteLine("ProfileViewModel Constructor");

            if (Preferences.Get("scope", "Customer").Equals("Vendor"))
            {
                ProfileList = vendorProfileList;
            }
            else
            {
                ProfileList = customerProfileList;
            }

            profile = new ProfileModel();
            profile.first_name = Preferences.Get(PreferencesKey.FirstName.ToString(), "");
            profile.last_name = Preferences.Get(PreferencesKey.LastName.ToString(), "");
            profile.email = Preferences.Get(PreferencesKey.Email.ToString(), "");
            profile.phone_number = Preferences.Get(PreferencesKey.PhoneNumber.ToString(), "09xxxxxxxxx");

            SetProfileValues(profile);

            MessageBus.Current.Listen<ProfileModel>().Subscribe(async (model) =>
            {
                //PopupActivityIndicator popupActivityIndicator;
                //popupActivityIndicator = new PopupActivityIndicator();
                //await PopupNavigation.Instance.PushAsync(popupActivityIndicator, false);

                var updateProfileResponse = await profileService.UpdateCustomerProfile(model);

                if (updateProfileResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Update Profile Success");

                    Preferences.Set(PreferencesKey.FirstName.ToString(), updateProfileResponse.Content.first_name);
                    Preferences.Set(PreferencesKey.LastName.ToString(), updateProfileResponse.Content.last_name);
                    Preferences.Set(PreferencesKey.Email.ToString(), updateProfileResponse.Content.email);
                    Preferences.Set(PreferencesKey.PhoneNumber.ToString(), updateProfileResponse.Content.phone_number);

                    profile = model;
                    SetProfileValues(profile);
                }
                else
                {
                    Console.WriteLine("Update Profile Failed");

                    var errorObj = JObject.Parse(updateProfileResponse.Error.Content);

#if DEBUG
                    Console.WriteLine("Error Obj: \n" + errorObj["error"]);
#endif
                    var errorMsg = errorObj["error"];

                    await App.Current.MainPage.DisplayAlert("Update Profile Error", (string)errorMsg, "OK");
                }

                //popupActivityIndicator.Pop();
            });

            _navigationService = navigationService;

            Logout = ReactiveCommand.Create(ExecuteLogout);
        }

        public ReactiveCommand<Unit, Unit> Logout { get; set; }

        private void SetProfileValues(ProfileModel model)
        {
            Number = model.phone_number;
            Email = model.email;
            FullName = model.first_name + " " + model.last_name;
        }

        public void ExecuteLogout()
        {
#if DEBUG
            Console.WriteLine("ExecuteLogout");
#endif
            ClearPreferences();
            CrossFacebookClient.Current.Logout();
            CrossGoogleClient.Current.Logout();
            NavigatetoLogin();
        }

        public void ClearPreferences()
        {
            Preferences.Remove(PreferencesKey.Email.ToString());
            Preferences.Remove(PreferencesKey.Password.ToString());
            //SecureStorage.Remove(SecureStorageKey.Password.ToString());

            Preferences.Remove(PreferencesKey.FirstName.ToString());
            Preferences.Remove(PreferencesKey.LastName.ToString());
            Preferences.Remove(PreferencesKey.ProfilePhoto.ToString());
            Preferences.Remove(PreferencesKey.IsVendor.ToString());
            Preferences.Remove(PreferencesKey.PhoneNumber.ToString());

            Preferences.Remove(PreferencesKey.AccessToken.ToString());
            //SecureStorage.Remove(SecureStorageKey.OAuthToken.ToString());
        }

        public void NavigatetoLogin()
        {
            _navigationService.PushToStack("LoginPage");
        }

        public void NavigateToPayments()
        {
            _navigationService.PushToStack("PaymentsPage");
        }
        public void NavigateAddCardPage()
        {
            _navigationService.PushToStack("AddCardPage");
        }

        public void NavigateToManageAddress()
        {
            _navigationService.PushToStack("ManageAddressPage");
        }
    }
}
