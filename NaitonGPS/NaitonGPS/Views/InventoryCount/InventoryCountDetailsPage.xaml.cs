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
		}


		private async void ClosePopup(object sender, EventArgs e)
		{
			await Shell.Current.Navigation.PopModalAsync();
		}
	}
}