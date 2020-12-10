using System;
using System.Collections.Generic;
using mycheffy.ViewModels.Login;
using Xamarin.Forms;

namespace mycheffy.Views.Pages.Login
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
