using mycheffy.Views.ContentViews.Payments;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace mycheffy.ViewModels.Payments
{
    public class PaymentsViewModel : NavigationViewModelBase
    {
        private INavigationService _navigationService;

        [Reactive]
        public ContentView Content { get; set; }

        public PaymentsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            Content = new PaymentsContentView();
        }

    }
}
