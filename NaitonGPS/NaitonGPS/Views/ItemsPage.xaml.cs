using NaitonGPS.Models;
using NaitonGPS.ViewModels;
using NaitonGPS.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    public partial class ItemsPage : ContentPage
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
        public static bool IsBigScreen { get; } = ScreenWidth >= 480;

        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();

            if (IsSmallScreen)
            {
                imgUser.HeightRequest = 25;
                imgUser.WidthRequest = 25;
                imgNotification.HeightRequest = 25;
                imgNotification.WidthRequest = 25;
            }
            else if (IsBigScreen)
            {
                imgUser.HeightRequest = 30;
                imgUser.WidthRequest = 30;                
                imgNotification.HeightRequest = 30;
                imgNotification.WidthRequest = 30;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}