using mycheffy.ViewModels.Vendor.Master;
using ReactiveUI;
using System.Reactive.Disposables;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace mycheffy.Views.Pages.Vendor.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VendorMasterPage : ContentPageBase<VendorMasterViewModel>
    {
        public VendorMasterPage()
        {
            InitializeComponent();
            this.BindCommand(ViewModel, x => x.SwitchCommand, x => x.HomeTap, x => x.HomeLabel)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.SwitchCommand, x => x.MessagesTap, x => x.MessagesLabel)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.SwitchCommand, x => x.WalletTap, x => x.WalletLabel)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.SwitchCommand, x => x.NotificationsTap, x => x.NotificationLabel)
                .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.SwitchCommand, x => x.ProfileTap, x => x.ProfileLabel)
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