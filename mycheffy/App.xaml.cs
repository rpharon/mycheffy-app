using System;
using mycheffy.ViewModels.Login;
using mycheffy.Views.Pages.Login;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mycheffy
{
    public partial class App
    {
        /*
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor.
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public App() : this(null)
        {
        }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Prism.Navigation.INavigationResult result;
            result = await NavigationService.NavigateAsync("LoginPage");

            if (!result.Success)
            {
                Exception ex = new InvalidNavigationException();
                if (result.Exception != null)
                    ex = result.Exception;
                Console.WriteLine("IResult Exception: " + ex);
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();

            // Register Services

        }

        //protected override void ConfigureViewModelLocator()
        //{
        //    base.ConfigureViewModelLocator();

        //    // Register View Models to ContentViews

        //    //Vendor Content views

        //}
    }
}
