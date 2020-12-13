using mycheffy.ViewModels.Payments;
using ReactiveUI;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace mycheffy.Views.ContentViews.Payments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentsContentView : ContentViewBase<PaymentsContentViewModel>
    {
        public PaymentsContentView()
        {
            InitializeComponent();

            this.BindCommand(ViewModel, x => x.Back, x => x.BackButton)
               .DisposeWith(ViewBindings);
        }
    }
}