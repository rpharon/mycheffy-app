using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using mycheffy.Services.Utilities;
using mycheffy.ViewModels.Landing;
using mycheffy.Views.ContentViews;
using mycheffy.Views.ContentViews.CartList;
using mycheffy.Views.ContentViews.FoodItem;
using mycheffy.Views.ContentViews.Landing;
using mycheffy.Views.ContentViews.Offers;
using mycheffy.Views.ContentViews.Profile;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Master
{
    public class MasterViewModel : NavigationViewModelBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private IDisposable subscription;

        public MasterViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Console.WriteLine("MasterViewModel Constructor");

            Explore = ReactiveCommand.CreateFromTask(OnExploreTapped);
            Offers = ReactiveCommand.CreateFromTask(OnOfferTapped);
            Cart = ReactiveCommand.CreateFromTask(OnCartTapped);
            Profile = ReactiveCommand.CreateFromTask(OnProfileTapped);

            //Default view
            Content = new LandingContentView();
            ExploreLabel = GridLength.Auto;
            OffersLabel = 0;
            CartLabel = 0;
            ProfileLabel = 0;

            ExploreBackgroundColor = Color.DarkRed;
            OffersBackgroundColor = Color.Transparent;
            CartBackgroundColor = Color.Transparent;
            ProfileBackgroundColor = Color.Transparent;
            ShowFooter();
        }

        public ReactiveCommand<Unit, Unit> Explore { get; set; }
        public ReactiveCommand<Unit, Unit> Offers { get; set; }
        public ReactiveCommand<Unit, Unit> Cart { get; set; }
        public ReactiveCommand<Unit, Unit> Profile { get; set; }

        private ContentView _content;
        public ContentView Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        private bool _isHeader;
        public bool IsHeader
        {
            get => _isHeader;
            set => this.RaiseAndSetIfChanged(ref _isHeader, value);
        }

        private bool _isFooter;
        public bool IsFooter
        {
            get => _isFooter;
            set => this.RaiseAndSetIfChanged(ref _isFooter, value);
        }

        private GridLength _exploreLabel;
        public GridLength ExploreLabel
        {
            get => _exploreLabel;
            set => this.RaiseAndSetIfChanged(ref _exploreLabel, value);
        }

        private GridLength _offersLabel;
        public GridLength OffersLabel
        {
            get => _offersLabel;
            set => this.RaiseAndSetIfChanged(ref _offersLabel, value);
        }

        private GridLength _cartLabel;
        public GridLength CartLabel
        {
            get => _cartLabel;
            set => this.RaiseAndSetIfChanged(ref _cartLabel, value);
        }

        private GridLength _profileLabel;
        public GridLength ProfileLabel
        {
            get => _profileLabel;
            set => this.RaiseAndSetIfChanged(ref _profileLabel, value);
        }

        private Color _exploreBackgroundColor;
        public Color ExploreBackgroundColor
        {
            get => _exploreBackgroundColor;
            set => this.RaiseAndSetIfChanged(ref _exploreBackgroundColor, value);
        }

        private Color _offersBackgroundColor;
        public Color OffersBackgroundColor
        {
            get => _offersBackgroundColor;
            set => this.RaiseAndSetIfChanged(ref _offersBackgroundColor, value);
        }

        private Color _cartBackgroundColor;
        public Color CartBackgroundColor
        {
            get => _cartBackgroundColor;
            set => this.RaiseAndSetIfChanged(ref _cartBackgroundColor, value);
        }

        private Color _profileBackgroundColor;
        public Color ProfileBackgroundColor
        {
            get => _profileBackgroundColor;
            set => this.RaiseAndSetIfChanged(ref _profileBackgroundColor, value);
        }

        public async Task OnExploreTapped()
        {
            Console.WriteLine("Task OnExploreTapped");

            Content = new LandingContentView();
            ExploreLabel = GridLength.Auto;
            OffersLabel = 0;
            CartLabel = 0;
            ProfileLabel = 0;

            ExploreBackgroundColor = Color.DarkRed;
            OffersBackgroundColor = Color.Transparent;
            CartBackgroundColor = Color.Transparent;
            ProfileBackgroundColor = Color.Transparent;
        }

        public async Task OnOfferTapped()
        {
            Console.WriteLine("Task OnRestaurantTapped");

            Content = new OffersContentView();
            ExploreLabel = 0;
            OffersLabel = GridLength.Auto;
            CartLabel = 0;
            ProfileLabel = 0;

            ExploreBackgroundColor = Color.Transparent;
            OffersBackgroundColor = Color.DarkRed;
            CartBackgroundColor = Color.Transparent;
            ProfileBackgroundColor = Color.Transparent;
        }

        public async Task OnCartTapped()
        {
            Console.WriteLine("Task OnCartTapped");

            Content = new CartListContentView();
            ExploreLabel = 0;
            OffersLabel = 0;
            CartLabel = GridLength.Auto;
            ProfileLabel = 0;

            ExploreBackgroundColor = Color.Transparent;
            OffersBackgroundColor = Color.Transparent;
            CartBackgroundColor = Color.DarkRed;
            ProfileBackgroundColor = Color.Transparent;
        }

        public async Task OnProfileTapped()
        {
            Console.WriteLine("Task OnSettingsTapped");

            Content = new ProfileContentView();
            ExploreLabel = 0;
            OffersLabel = 0;
            CartLabel = 0;
            ProfileLabel = GridLength.Auto;

            ExploreBackgroundColor = Color.Transparent;
            OffersBackgroundColor = Color.Transparent;
            CartBackgroundColor = Color.Transparent;
            ProfileBackgroundColor = Color.DarkRed;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            subscription.Dispose();
        }

        public void OnNavigatedTo(INavigationParameters parameters)

        {
            subscription = MessageBus.Current.Listen<NavigationEvent>().Subscribe(async (NavEvent) =>
            {
                if (NavEvent.Action == NavigationAction.Push)
                {
                    NavigationParameters param = new NavigationParameters();
                    if (NavEvent.Data != null)
                    {
                        param.Add("data", Convert.ChangeType(NavEvent.Data, NavEvent.DataType));
                    }
                    await _navigationService.NavigateAsync(NavEvent.PageName, param);
                }
                else if (NavEvent.Action == NavigationAction.Refresh)
                {
                    if (NavEvent.PageName == "CartListContentView")
                    {
                        Content = new CartListContentView();
                    }
                }
            });
        }

        public void OnNavigating(INavigationParameters paramaters)
        {
        }

        private void ShowFooter()
        {
            IsFooter = true;
        }

        private void HideFooter()
        {
            IsFooter = false;
        }
    }
}
