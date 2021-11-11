using NaitonGPS.Models;
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
		public InventoryCountDetailsPage(InventoryCount inventoryCount, EventHandler<InventoryCount> item)
		{
			InitializeComponent();
			BindingContext = new InventoryCountDetailsViewModel(inventoryCount, item);
			entCount.Focused += EntCount_Focused;
			entCount.Unfocused += EntCount_Unfocused;
		}

		private void EntCount_Unfocused(object sender, FocusEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(entCount.Text))
				entCount.Text = "0";
		}

		private void EntCount_Focused(object sender, FocusEventArgs e)
		{
			if (entCount.Text.Trim() == "0")
				entCount.Text = string.Empty;
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
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