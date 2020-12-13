using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mycheffy.Models.Profile;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace mycheffy.Views.Popups
{
    public partial class EditProfileNumberPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ProfileModel _model;
        public EditProfileNumberPopup()
        {
            InitializeComponent();
        }

        public EditProfileNumberPopup(ProfileModel initialData)
        {
            InitializeComponent();
            PhoneNumberEntry.Text = initialData.phone_number;
            EmailEntry.Text = initialData.email;
            _model = initialData;
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Pop();
        }

        public void Pop()
        {
            PopupNavigation.Instance.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            _model.phone_number = PhoneNumberEntry.Text;
            _model.email = EmailEntry.Text;
            await PopupNavigation.Instance.PopAsync();
            MessageBus.Current.SendMessage(_model);
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
