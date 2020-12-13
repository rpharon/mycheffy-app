using System;
using System.Collections.Generic;
using mycheffy.Services.Utilities;
using mycheffy.Views.ContentViews.CartList;
using Prism.Navigation;
using ReactiveUI;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace mycheffy.ViewModels.CartList
{
    public class CartListViewModel : NavigationViewModelBase
    {
        private readonly NavigationEventService _navigationService;

        private bool isNumberNotExist;
        public bool IsNumberNotExist
        {
            get => isNumberNotExist;
            set => this.RaiseAndSetIfChanged(ref isNumberNotExist, value);
        }

        private bool isNumberExist;
        public bool IsNumberExist
        {
            get => isNumberExist;
            set => this.RaiseAndSetIfChanged(ref isNumberExist, value);
        }

        public class Cart
        {
            public string Name { get; set; }
            public string Size { get; set; }
            public string Options { get; set; }
            public string Qty { get; set; }
            public string Price { get; set; }
        }

        public List<Cart> CartList { get; set; }

        public CartListViewModel(NavigationEventService navigationService)
        {
            CartList = new List<Cart>();
            CartList.Add(new Cart
            {
                Name = "Creamy Nachos",
                Size = "Regular",
                Options = "Customize",
                Qty = "1",
                Price = "P120"
            });

            CartList.Add(new Cart
            {
                Name = "Vegan Burger",
                Size = "Regular",
                Options = "Customize",
                Qty = "1",
                Price = "P150"
            });

            _navigationService = navigationService;

            if (Preferences.ContainsKey("PhoneNumber"))
            {
                IsNumberExist = true;
                IsNumberNotExist = false;
            }
            else
            {
                IsNumberExist = false;
                IsNumberNotExist = true;
            }
        }

        public void ProceedPayment()
        {
            _navigationService.PushToStack("PaymentsPage");
        }
    }
}
