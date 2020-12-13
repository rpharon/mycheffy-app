using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using mycheffy.ViewModels.CartList;
using mycheffy.Views.Popups;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mycheffy.Views.ContentViews.CartList
{
    public partial class CartListContentView : ContentViewBase<CartListViewModel>
    {
        public CartListContentView() 
        {
            InitializeComponent();

            //EditButton.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e =>
            //{
            //    CartListDisplayAlert();
            //});

            //DiscountButton.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e =>
            //{
            //    CartListDisplayAlert();
            //});

            //ApplyButton.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e =>
            //{
            //    CartListDisplayAlert();
            //});

            //ChangeButton.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e =>
            //{
            //    CartListDisplayAlert();
            //});

            //ProceedButton.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(async e =>
            //{
            //    await PopupNavigation.Instance.PushAsync(new CartLoginPopup());
            //});

            //PaymentButton.Events().Clicked.ObserveOn(RxApp.MainThreadScheduler).Subscribe(e =>
            //{
            //    ViewModel.ProceedPayment();
            //});
        }

        private async void CartListDisplayAlert()
        {
            await Application.Current.MainPage.DisplayAlert("Cart", "Layout is still under development.", "OK");
        }
    }
}
