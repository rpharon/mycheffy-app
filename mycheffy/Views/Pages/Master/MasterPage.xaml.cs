using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using mycheffy.ViewModels.Master;
using Prism.Navigation;
using ReactiveUI;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace mycheffy.Views.Pages.Master
{
    public partial class MasterPage : ContentPageBase<MasterViewModel>
    {
        public MasterPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Explore, x => x.ExploreTap)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Offers, x => x.OffersTap)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Cart, x => x.CartTap)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Profile, x => x.ProfileTap)
                .DisposeWith(ViewBindings);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeInsets = On<iOS>().SafeAreaInsets();
            safeInsets.Bottom = 0;
            Padding = safeInsets;
        }
    }
}
