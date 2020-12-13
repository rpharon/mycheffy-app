using mycheffy.Models.FoodItem;
using mycheffy.ViewModels.Landing;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mycheffy.Views.ContentViews.Landing
{
    public partial class SearchListContentView : ContentViewBase<SearchListViewModel>
    {
        public SearchListContentView()
        {
            InitializeComponent();

            SearchItems.Events().ItemTapped.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e =>
            {
                var item = e.Item as FoodItemModel;

                Console.WriteLine("Search Result: " + item.name);

                ViewModel.NavigateToRestaurantAsync(item.restaurant.slug);
            });
        }
    }
}