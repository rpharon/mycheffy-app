using System;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Navigation;
using ReactiveUI;

namespace mycheffy.ViewModels.Profile
{
    public class AddCardViewModel : NavigationViewModelBase
    {
        private readonly INavigationService _navigationService;

        public AddCardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            BackCommand = ReactiveCommand.CreateFromTask(ExecuteBackCommand);
        }

        public ReactiveCommand<Unit, Unit> BackCommand { get; set; }

        public async Task ExecuteBackCommand()
        {
            await _navigationService.GoBackAsync();
        }
    }
}
