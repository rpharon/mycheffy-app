using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using mycheffy.Models.CartList;
using mycheffy.Models.FoodItem;
using mycheffy.Models.Restaurant;
using mycheffy.Models.Shared.Enums;
using mycheffy.ViewModels.Popup;
using mycheffy.ViewModels.Restaurant;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mycheffy.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomizeOrderPopup : PopUpBase<CustomizeOrderViewModel>
    {
        public float itemPrice = 0;

        private string product_id;
        public bool isUpdate = false;

        public bool isChanged = false;
        public CustomizeOrderPopup(ProductListItem item)
        {
            InitializeComponent();
            product_id = item.product.id;
            itemPrice = (float)item.product.price;
            labelPrice.Text = "₱" + item.product.price;
        }
        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            if (isChanged)
            {
                await PopupNavigation.Instance.PushAsync(new PopupActivityIndicator());
                if (!isUpdate)
                {
                    var response = await ViewModel.AddCartItem(product_id, itemPrice);
                    MessageBus.Current.SendMessage(new RestaurantEvents(EventActions.Add, response.Content));
                }
                else
                {
                    var response = await ViewModel.UpdateCartItem(product_id, itemPrice);
                    MessageBus.Current.SendMessage(new RestaurantEvents(EventActions.Update, response.Content));
                }
                await PopupNavigation.Instance.PopAsync();
                Pop();
            }

        }

        void OnQuantityChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            ViewModel.Quanitity = (int)value;
            isChanged = true;
            ViewModel.TotalPrice = (float)(value * itemPrice);
        }

        public void Pop()
        {
            PopupNavigation.Instance.PopAsync();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}
