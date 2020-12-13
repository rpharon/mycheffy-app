using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using mycheffy.ViewModels.Profile;
using ReactiveUI;
using Xamarin.Forms;

namespace mycheffy.Views.Pages.Profile
{
    public partial class AddNewAddressPage : ContentPageBase<AddNewAddressViewModel>
    {
        public AddNewAddressPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Back, x => x.BackButton)
                    .DisposeWith(ViewBindings);

            this.BindCommand(ViewModel, x => x.Save, x => x.SaveButton)
                    .DisposeWith(ViewBindings);

            ImageTap.Events().Tapped.ObserveOn(RxApp.MainThreadScheduler).Subscribe(async e =>
            {
                await ViewModel.ExecuteViewMap();
            });
        }
    }
}
