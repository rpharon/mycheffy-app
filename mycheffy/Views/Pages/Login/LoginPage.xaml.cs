using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using mycheffy.ViewModels.Login;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace mycheffy.Views.Pages.Login
{
    public partial class LoginPage : ContentPageBase<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.SignIn, x => x.SignInButton)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.SignUp, x => x.SignUpLabel)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.SignAs, x => x.SignInAsButton)
                .DisposeWith(ViewBindings);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
        }
    }
}
