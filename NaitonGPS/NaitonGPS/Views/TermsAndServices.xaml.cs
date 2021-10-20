using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsAndServices : ContentPage
    {
        public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
        public static bool IsSmallScreen { get; } = ScreenWidth <= 480;

        public static bool IsBigScreen { get; } = ScreenWidth >= 480;
        public TermsAndServices()
        {
            InitializeComponent();
            if (IsSmallScreen)
            {
                mainGrid.Margin = new Thickness(10, 10, 10, 0);
                rowToChange.Height = new GridLength(1, GridUnitType.Star);
                imgArrowLeft.HeightRequest = 25;
                imgArrowLeft.WidthRequest = 25;

                imgQuestion.HeightRequest = 65;
                imgQuestion.WidthRequest = 65;
                lblNeedHelp.FontSize = 18;
            }
            else if (IsBigScreen)
            {
                mainGrid.Margin = new Thickness(10, 40, 10, 0);
                rowToChange.Height = new GridLength(3, GridUnitType.Star);
                imgArrowLeft.HeightRequest = 30;
                imgArrowLeft.WidthRequest = 30;

                imgQuestion.HeightRequest = 80;
                imgQuestion.WidthRequest = 80;
                lblNeedHelp.FontSize = 22;

            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}