using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ImTools;
using mycheffy.ViewModels.Landing;
using mycheffy.Views.Pages;
using ReactiveUI;
using Xamarin.Forms;

namespace mycheffy.Views.ContentViews.Landing
{
    public partial class LandingContentView : ContentViewBase<LandingViewModel>
    {
        public LandingContentView()
        {
            InitializeComponent();
            this.BindCommand(ViewModel, x => x.LeftButtonCommand, x => x.LeftButton)
                 .DisposeWith(ViewBindings);
        }

    }
}