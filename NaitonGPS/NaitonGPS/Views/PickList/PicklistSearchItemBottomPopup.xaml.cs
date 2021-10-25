using NaitonGPS.Models;
using NaitonGPS.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistSearchItemBottomPopup : ContentPage
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        public PicklistSearchItemBottomPopup(PickListItem pickListItem, EventHandler<Rack> callBack)
        {
            InitializeComponent();
            BindingContext = new RacksViewModel(pickListItem,callBack);

            //if (IsSmallScreen)
            //{
            //    lblScanToHide.IsVisible = false;
            //}
            //else if (IsBigScreen)
            //{
            //    lblScanToHide.IsVisible = true;
            //}
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            searchText.Focus();
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}