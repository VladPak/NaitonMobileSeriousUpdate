using NaitonGPS.Models;
using NaitonGPS.Services;
using NaitonGPS.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventoryCountDetailsPage : ContentPage
	{
		private readonly InventoryCountDetailsViewModel _viewModel;
		public InventoryCountDetailsPage(Models.InventoryCount inventoryCount, EventHandler<Models.InventoryCount> item)
		{
			InitializeComponent();
			BindingContext = _viewModel = new InventoryCountDetailsViewModel(inventoryCount, item);

			var role = _viewModel.RoleManager.Get(RoleManager.SHOW_CURRENT_QUANTITY);
			this.stockTextLabel.IsVisible = this.stockValueLabel.IsVisible = role != null && role.IsChecked;

			this.addPoductButton.IsVisible = inventoryCount.ProductId <= 0;
			this.saveButton.IsVisible = this.entCount.IsVisible = this.entDamaged.IsVisible = inventoryCount.ProductId > 0;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();

			if (entCount.IsVisible)
			{
				if (entCount.Text.Trim() == "0")
					entCount.Text = string.Empty;
				await Task.Delay(600);
				entCount.Focus();
			}
		}
		private async void ClosePopup(object sender, EventArgs e)
		{
			await Shell.Current.Navigation.PopModalAsync();
		}
	}
}