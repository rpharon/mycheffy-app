using Prism.Navigation;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace mycheffy.ViewModels.Payments
{
    public class PaymentsContentViewModel : NavigationViewModelBase
    {
        private INavigationService _navigationService;

        public ReactiveCommand<Unit, Unit> Back { get; set; }

        public PaymentsContentViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Back = ReactiveCommand.CreateFromTask(ExecuteBack);
        }
        private async Task ExecuteBack()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
