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

        public PicklistSearchItemBottomPopup(PickListItem pickListItem, EventHandler<Rack> callBack)
        {
            InitializeComponent();
            BindingContext = new RacksViewModel(pickListItem,callBack, ChangeInput);
        }

        void ChangeInput(object sender, string scanText)
        {
            searchText.Focus();
            searchText.Text = scanText.TrimEnd();
        }
    }
}