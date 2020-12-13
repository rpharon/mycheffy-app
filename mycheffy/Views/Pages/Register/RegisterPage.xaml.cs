using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using mycheffy.ViewModels.Register;
using ReactiveUI;
using Xamarin.Forms;

namespace mycheffy.Views.Pages.Register
{
    public partial class RegisterPage : ContentPageBase<RegisterViewModel>
    {
        public RegisterPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Proceed, x => x.ProceedButton)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.BacktoSignIn, x => x.BacktoSignInButton)
                .DisposeWith(ViewBindings);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage.DisplayAlert("Welcome", "Action is still under development.", "OK");
        }
    }
}
