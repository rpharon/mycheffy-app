using System;
using mycheffy.ViewModels.Landing;
using mycheffy.ViewModels.Master;
using mycheffy.ViewModels.Restaurant;
using mycheffy.Views.ContentViews.Landing;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using mycheffy.Views.Pages.Master;
using mycheffy.Views.Pages.Login;
using mycheffy.ViewModels.Login;
using mycheffy.Views.Pages.Category;
using mycheffy.ViewModels.Category;
using mycheffy.Views.ContentViews.CartList;
using mycheffy.ViewModels.CartList;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using mycheffy.Views.Pages.Register;
using mycheffy.ViewModels.Register;
using Prism.Mvvm;
using mycheffy.Services.Interfaces;
using mycheffy.Services;
using mycheffy.Views.Pages.Restaurant;
using mycheffy.Views.Popups;
using mycheffy.Views.ContentViews.Profile;
using mycheffy.ViewModels.Profile;
using mycheffy.Views.ContentViews.Offers;
using mycheffy.ViewModels.Offers;
using mycheffy.Views.Pages.Profile;
using mycheffy.Views.Pages.Payments;
using mycheffy.ViewModels.Payments;
using Prism.Navigation;
using mycheffy.Views.Pages.Vendor.Master;
using mycheffy.ViewModels.Vendor.Master;
using mycheffy.Views.ContentViews.Vendor;
using mycheffy.ViewModels.Vendor.Home;
using mycheffy.Views.ContentViews.Vendor.Home;
using Xamarin.Essentials;
using mycheffy.Views.ContentViews.Payments;
using mycheffy.Service;
using mycheffy.Models.Shared.Enums;
using mycheffy.Views;
using mycheffy.ViewModels;
using mycheffy.Services.Utilities;
using mycheffy.ViewModels.Popup;

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

            //GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            Device.SetFlags(new[] {
                "Expander_Experimental",
                "RadioButton_Experimental"

            });
            ThemeManager.LoadTheme();

            Prism.Navigation.INavigationResult result;

            if (Preferences.ContainsKey("HasCreatedAccount"))
            {
                bool hasCreatedAccount = (Boolean)Preferences.Get("HasCreatedAccount", false);

                if (!hasCreatedAccount) //For first activation of app....
                {
                    //Proceed to RegisterPage
                    var par = new NavigationParameters();
                    par.Add("RegisterAs", 0);
                    par.Add("IsPreRegister", 1);

                    result = await NavigationService.NavigateAsync("RegisterPage", par);
                }
                else
                {
                    //if (Preferences.ContainsKey(PreferencesKey.Email.ToString()) && Preferences.ContainsKey(PreferencesKey.Password.ToString()))
                    if (Preferences.ContainsKey("Email") && Preferences.ContainsKey("Password"))
                    {
                        //If an account is existing, proceed to MasterPage
                        result = await NavigationService.NavigateAsync("MasterPage");
                    }
                    else
                    {
                        //If an account doesn't exist, proceed to LoginPage
                        var par = new NavigationParameters();
                        par.Add("LoginAs", 0); //Login as Customer;

                        result = await NavigationService.NavigateAsync("LoginPage", par);
                    }
                }
            }
            else
            {
                var par = new NavigationParameters();
                par.Add("RegisterAs", 0);
                par.Add("IsPreRegister", 1);

                result = await NavigationService.NavigateAsync("RegisterPage", par);
            }

            if (!result.Success)
            {
                Exception ex = new InvalidNavigationException();
                if (result.Exception != null)
                    ex = result.Exception;
                Console.WriteLine("IResult Exception: " + ex);
            }

            // Preferences.Set("scope", "Vendor");
            // //var result = await NavigationService.NavigateAsync("LoginPage");
            // var result = await NavigationService.NavigateAsync("VendorMasterPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<CategoryPage, CategoryViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterViewModel>();
            containerRegistry.RegisterForNavigation<RestaurantPage, RestaurantViewModel>();
            containerRegistry.RegisterForNavigation<AddCardPage, AddCardViewModel>();
            containerRegistry.RegisterForNavigation<ManageAddressPage, ManageAddressViewModel>();
            containerRegistry.RegisterForNavigation<AddNewAddressPage, AddNewAddressViewModel>();
            containerRegistry.RegisterForNavigation<EditUserAddressPage, EditUserAddressViewModel>();
            containerRegistry.RegisterForNavigation<PaymentsPage, PaymentsViewModel>();
            containerRegistry.RegisterForNavigation<VendorMasterPage, VendorMasterViewModel>();

            //containerRegistry.RegisterForNavigation<MapsPage, MapsViewModel>();

            // Register Services
            containerRegistry.RegisterSingleton<ProfileService>();
            containerRegistry.RegisterSingleton<NavigationEventService>();
            containerRegistry.RegisterSingleton<ICategoryServices, CategoryService>();
            containerRegistry.RegisterSingleton<IFoodServices, FoodService>();
            containerRegistry.RegisterSingleton<IRestaurantServices, RestaurantService>();
            containerRegistry.RegisterSingleton<ILoginServices, LoginService>();
            containerRegistry.RegisterSingleton<ICartService, CartService>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            // Register View Models to ContentViews
            ViewModelLocationProvider.Register<LandingContentView, LandingViewModel>();
            ViewModelLocationProvider.Register<OffersContentView, OffersViewModel>();
            ViewModelLocationProvider.Register<OffersRestaurantTabContentView, OffersRestaurantTabViewModel>();
            ViewModelLocationProvider.Register<OffersPromoTabContentView, OffersPromoTabViewModel>();
            ViewModelLocationProvider.Register<CartListContentView, CartListViewModel>();
            ViewModelLocationProvider.Register<ProfileContentView, ProfileViewModel>();
            ViewModelLocationProvider.Register<SearchListContentView, SearchListViewModel>();
            ViewModelLocationProvider.Register<LandingPageDetailsContentView, LandingPageDetailsViewModel>();
            ViewModelLocationProvider.Register<PaymentsContentView, PaymentsContentViewModel>();
            ViewModelLocationProvider.Register<CustomizeOrderPopup, CustomizeOrderViewModel>();

            //Vendor Content views
            ViewModelLocationProvider.Register<VendorHomeContentView, VendorHomeViewModel>();
        }
    }
}