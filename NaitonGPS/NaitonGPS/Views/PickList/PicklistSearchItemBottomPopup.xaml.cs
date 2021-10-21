using NaitonGPS.Helpers;
using NaitonGPS.Models;
using NaitonGPS.ViewModels;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistSearchItemBottomPopup : ContentPage
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        public PicklistSearchItemBottomPopup()
        {
            InitializeComponent();

            if (IsSmallScreen)
            {
                slChange.Margin = new Thickness(0);
                slChange.Padding = new Thickness(-5);
                imgScan.WidthRequest = 20;
                lblScanToHIde.IsVisible = false;
                imgCloseP.HeightRequest = 25;
                imgCloseP.WidthRequest = 25;
                lblRacSelect.FontSize = 22;
                entSearch.FontSize = 15;
                lblScanToHIde.IsVisible = false;
                columnToAlter.Width = new GridLength(1, GridUnitType.Star);
            }
            else if (IsBigScreen)
            {
                slChange.Margin = new Thickness(-5, 0, -5, 0);
                slChange.Padding = new Thickness(0);
                imgScan.WidthRequest = 20;
                lblScanToHIde.IsVisible = true;
                imgCloseP.HeightRequest = 30;
                imgCloseP.WidthRequest = 30;
                lblRacSelect.FontSize = 28;
                entSearch.FontSize = 20;
                lblScanToHIde.IsVisible = true;
                columnToAlter.Width = new GridLength(1.5, GridUnitType.Star);
            }

            var rackList = DataManager.GetPickRacks(0,0);
            BindingContext = new RacksViewModel(rackList);
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}