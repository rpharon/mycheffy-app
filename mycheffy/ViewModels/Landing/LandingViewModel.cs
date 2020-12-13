using mycheffy.Models.FoodItem;
using mycheffy.Views.ContentViews;
using mycheffy.Views.ContentViews.Landing;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Landing
{
    public class LandingViewModel : NavigationViewModelBase
    {
        private LandingPageDetailsContentView pageDetails = new LandingPageDetailsContentView();
        private SearchListContentView searchList = new SearchListContentView();

        public Type CurrentTypeViewModel;
        public LandingViewModel()
        {
            Content = pageDetails;
            LeftButtonIcon = "location_pin_icon";
            this.WhenAnyValue(x => x.SearchText)
              .Throttle(TimeSpan.FromMilliseconds(800))
              .Select(term => term?.Trim())
              .DistinctUntilChanged()
              .ObserveOn(RxApp.MainThreadScheduler).Subscribe(text =>
              {
                  if (string.IsNullOrEmpty(text))
                  {
                      LeftButtonIcon = "location_pin_icon";

                      Content = pageDetails;
                  }
                  else
                  {
                      LeftButtonIcon = "back_icon";

                      Content = searchList;
                  }
                  MessageBus.Current.SendMessage<string>(SearchText, "search_text");
              });
            SearchCommand = ReactiveCommand.CreateFromTask(async (e) =>
            {
                MessageBus.Current.SendMessage<string>(SearchText, "search_text");
            });
            LeftButtonCommand = ReactiveCommand.CreateFromTask(ExecuteLeftButton);
        }

        [Reactive] public string SearchText { get; set; }
        [Reactive] public string LeftButtonIcon { get; set; }
        [Reactive] public ContentView Content { get; set; }

        public ReactiveCommand<Unit, Unit> LeftButtonCommand { get; set; }

        public ICommand SearchCommand { get; }

        private async Task ExecuteLeftButton()
        {
            if (LeftButtonIcon.Equals("back_icon"))
            {
                LeftButtonIcon = "location_pin_icon";
                Content = pageDetails;
                SearchText = null;
            }
        }

    }
}