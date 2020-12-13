using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using mycheffy.ViewModels.Profile;
using ReactiveUI;
using Xamarin.Forms;

namespace mycheffy.Views.Pages.Profile
{
    public partial class AddCardPage : ContentPageBase<AddCardViewModel>
    {
        public AddCardPage()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.BackCommand, x => x.BackButton)
                .DisposeWith(ViewBindings);

            //activate entry on startup
            entryCardNumber.Focus();
        }
    }
}
