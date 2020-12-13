using System.Reactive.Disposables;
using mycheffy.ViewModels;
using Prism.Navigation;
using ReactiveUI.XamForms;

namespace mycheffy.Views.Pages
{
    public abstract class ContentPageBase<T> : ReactiveContentPage<T>, IDestructible
        where T : ViewModelBase
    {
        protected readonly CompositeDisposable ViewBindings = new CompositeDisposable();

        public void Destroy()
        {
            ViewBindings?.Dispose();
        }
    }
}
