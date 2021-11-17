using NaitonGPS.Services;
using NaitonGPS.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventoryCountListPage : ContentPage
	{
		public static double ScreenWidth { get; } = DeviceDisplay.MainDisplayInfo.Width;
		public static bool IsSmallScreen { get; } = ScreenWidth <= 480;
		public static bool IsBigScreen { get; } = ScreenWidth >= 480;

		InventoryCountListViewModel _viewModel;

		public InventoryCountListPage()
		{
			InitializeComponent();

			BindingContext = _viewModel = new InventoryCountListViewModel();

			var role = _viewModel.RoleManager.Get(RoleManager.ADD_RANDOM_RACK);
			this.addRandomRackButton.IsVisible = role != null && role.IsChecked;

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

		async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			if (e.Item == null)
				return;

			await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

			//Deselect Item
			((ListView)sender).SelectedItem = null;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}
		private async void UserInformationPopUp(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new UserInformation());
		}
	}
}
