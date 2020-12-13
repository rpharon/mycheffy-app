using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using mycheffy.ViewModels.Profile;
using ReactiveUI;
using Xamarin.Forms;

namespace mycheffy.Views.Pages.Profile
{
    public partial class EditUserAddressPage : ContentPageBase<EditUserAddressViewModel>
    {
        public EditUserAddressPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Back, x => x.BackButton)
                    .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Save, x => x.SaveButton)
                    .DisposeWith(ViewBindings);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Console.WriteLine("Open Maps");
            //Application.Current.MainPage.Navigation.PushModalAsync(new MapsPage());
        }
    }
}
