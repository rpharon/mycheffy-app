using System;
using mycheffy.Views.Pages.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mycheffy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
