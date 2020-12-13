using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using mycheffy.ViewModels.Profile;
using mycheffy.Views.Popups;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mycheffy.Views.ContentViews.Profile
{
    public partial class ProfileContentView : ContentViewBase<ProfileViewModel>
    {
        public ProfileContentView()
        {
            InitializeComponent();

            EditButton.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(async e =>
            {
                await PopupNavigation.Instance.PushAsync(new EditProfileNumberPopup(this.ViewModel.profile));
                //await Application.Current.MainPage.DisplayAlert("Profile", "Layout is still under development.", "OK");
            }).DisposeWith(ViewBindings);


            ProfileListView.Events().ItemTapped.ObserveOn(RxApp.MainThreadScheduler).Subscribe(async e =>
            {
                var item = e.Item as ProfileViewModel.Profile;

                if (item.Name == "Logout")
                {
                    ViewModel.ExecuteLogout();
                }
                else if (item.Name == "Payments")
                {
                    ViewModel.NavigateAddCardPage();
                }
                else if (item.Name == "Manage Addresses")
                {
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new ManageAddressPage());
                    ViewModel.NavigateToManageAddress();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Profile", "Layout is still under development.", "OK");
                }

            }).DisposeWith(ViewBindings);
        }
    }
}
