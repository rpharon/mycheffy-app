using System;
using System.Reactive.Disposables;
using mycheffy.ViewModels;
using ReactiveUI.XamForms;

namespace mycheffy.Views.ContentViews
{
    public class ContentViewBase<TViewModel> : ReactiveContentView<TViewModel>
        where TViewModel : ViewModelBase
    {
        protected readonly CompositeDisposable ViewBindings = new CompositeDisposable();
    }
}
