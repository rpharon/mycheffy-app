using System;
using Prism.Navigation;

namespace mycheffy.ViewModels
{
    public class NavigationViewModelBase : ViewModelBase, INavigationAware
    {
        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }
    }
}
