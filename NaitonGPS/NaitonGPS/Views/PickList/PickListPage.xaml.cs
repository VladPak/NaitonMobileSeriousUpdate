using NaitonGPS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views.PickList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickListPage : ContentPage
    {
        PickListViewModel _viewModel;
        public PickListPage()
        {
            InitializeComponent();            
            BindingContext = _viewModel = new PickListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}