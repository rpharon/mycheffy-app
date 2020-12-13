using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using mycheffy.Models.Address;
using mycheffy.ViewModels.Profile;
using ReactiveUI;
using Xamarin.Forms;

namespace mycheffy.Views.Pages.Profile
{
    public partial class ManageAddressPage : ContentPageBase<ManageAddressViewModel>
    {
        public ManageAddressPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Back, x => x.BackButton)
                .DisposeWith(ViewBindings);
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var btn = (Button)sender;

            if (btn.Text == "EDIT")
            {
                AddressModel item = (from itm in ViewModel.AddressList
                                     where itm.complete_address == btn.CommandParameter.ToString()
                                     select itm).FirstOrDefault<AddressModel>();

                await ViewModel.UpdateUserAddress(item.id, item.type, item.complete_address, item.full_google_address);
            }
            else if (btn.Text == "DELETE")
            {
                AddressModel item = (from itm in ViewModel.AddressList
                                     where itm.complete_address == btn.CommandParameter.ToString()
                                     select itm).FirstOrDefault<AddressModel>();

                await ViewModel.DeleteUserAddress(item.id);
                await Application.Current.MainPage.DisplayAlert("Manage Address", item.complete_address + " Address deleted.", "OK");
            }
            else if (btn.Text == "ADD NEW")
            {
                await ViewModel.NavigateToAddNewAddress();
            }
        }
    }
}
