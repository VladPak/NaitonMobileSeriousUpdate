using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HeaderNavigationBar : Grid
	{
		public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
		public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
		public static bool IsBigScreen { get; } = ScreenWidth >= 480;


		public HeaderNavigationBar ()
		{
			InitializeComponent ();

			if (IsSmallScreen)
			{
				mainGrids.Margin = new Thickness(0,7,0,0);
				iconUser.HeightRequest = 25;
				iconUser.WidthRequest = 25;
				iconNotification.WidthRequest = 25;
				iconNotification.HeightRequest = 25;
			}
			else if (IsBigScreen)
			{
				mainGrids.Margin = new Thickness(0, 10, 0, 0);
				iconUser.HeightRequest = 30;
				iconUser.WidthRequest = 30;
				iconNotification.HeightRequest = 30;
				iconNotification.WidthRequest = 30;
			}
		}

        private void UserInfo(object sender, EventArgs e)
        {
			//await Navigation.PushPopupAsync(new UserInformationPage());
		}

		private void Notifications(object sender, EventArgs e)
        {
			//await Navigation.PushPopupAsync(new NotificationsPage());
		}
	}
}