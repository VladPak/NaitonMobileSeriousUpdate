using NaitonGPS.Models;
using NaitonGPS.Services;
using NaitonGPS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventoryCountDetailsPage : ContentPage
	{

		private readonly InventoryCountDetailsViewModel _viewModel;
		public InventoryCountDetailsPage(InventoryCount inventoryCount, EventHandler<InventoryCount> item)
		{
			InitializeComponent();
			BindingContext = _viewModel = new InventoryCountDetailsViewModel(inventoryCount, item);

			var role = _viewModel.RoleManager.Get(RoleManager.SHOW_CURRENT_QUANTITY);
			this.stockTextLabel.IsVisible = this.stockValueLabel.IsVisible = role != null && role.IsChecked;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			if (entCount.Text.Trim() == "0")
				entCount.Text = string.Empty;
			await Task.Delay(600);
			entCount.Focus();
		}
		private async void ClosePopup(object sender, EventArgs e)
		{
			await Shell.Current.Navigation.PopModalAsync();
		}
	}
}