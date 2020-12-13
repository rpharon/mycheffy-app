using mycheffy.Models.Category;
using mycheffy.Models.FoodItem;
using mycheffy.Models.Restaurant;
using mycheffy.ViewModels.Landing;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mycheffy.Views.ContentViews.Landing
{
    public partial class LandingPageDetailsContentView : ContentViewBase<LandingPageDetailsViewModel>
    {

        public LandingPageDetailsContentView()
        {
            InitializeComponent();

            TopCatogoriesView.Events().SelectionChanged.ObserveOn(RxApp.MainThreadScheduler).Where(e => e.CurrentSelection.FirstOrDefault() != null).Subscribe(async e =>
            {
                var item = e.CurrentSelection.FirstOrDefault() as CategoryModel;

                Console.WriteLine("Category Item: " + item.name);

                await Application.Current.MainPage.DisplayAlert("Explore", "Layout not yet available", "OK");
            }).DisposeWith(ViewBindings);

            PopularItemsView.Events().SelectionChanged.ObserveOn(RxApp.MainThreadScheduler).Where(e => e.CurrentSelection.FirstOrDefault() != null).Subscribe(e =>
            {
                var item = e.CurrentSelection.FirstOrDefault() as FoodItemModel;

                Console.WriteLine("Popular Item: " + item.restaurant.name);
                Console.WriteLine("Food Item: " + item.name);
                ViewModel.NavigateToRestaurantAsync(item.restaurant.slug, typeof(string));
                PopularItemsView.SelectedItem = null;

            }).DisposeWith(ViewBindings);

            NearbyDealsView.Events().SelectionChanged.ObserveOn(RxApp.MainThreadScheduler).Where(e => e.CurrentSelection.FirstOrDefault() != null).Subscribe(e =>
            {
                var item = e.CurrentSelection.FirstOrDefault() as RestaurantModel;
                ViewModel.NavigateToRestaurantAsync(item.slug, typeof(string));
                NearbyDealsView.SelectedItem = null;
            }).DisposeWith(ViewBindings);
        }
    }
}