using NaitonGPS.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views.PickList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickListPage : ContentPage
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        PickListViewModel _viewModel;
        public PickListPage()
        {
            InitializeComponent();            
            BindingContext = _viewModel = new PickListViewModel();

            if (IsSmallScreen)
            {
                imgNotification.HeightRequest = 25;
                imgNotification.WidthRequest = 25;
                imgUser.HeightRequest = 25;
                imgUser.WidthRequest = 25;
            }
            else if (IsBigScreen)
            {
                imgNotification.HeightRequest = 30;
                imgNotification.WidthRequest = 30;
                imgUser.HeightRequest = 30;
                imgUser.WidthRequest = 30;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {            
            await DisplayAlert("", "Scanner button is clicked", "Ok");
        }

        private async void UserInformationPopUp(object sender, System.EventArgs e)
        {
            await Navigation.PushModalAsync(new UserInformation());
        }
    }
}