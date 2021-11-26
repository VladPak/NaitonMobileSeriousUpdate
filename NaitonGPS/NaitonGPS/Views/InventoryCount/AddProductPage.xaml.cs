using NaitonGPS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views.InventoryCount
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddProductPage : ContentPage
	{
		private readonly AddProductViewModel _viewModel;
		public AddProductPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new AddProductViewModel();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			_viewModel.OnAppearing();
		}

		private async void ClosePopup(object sender, EventArgs e)
		{
			await Shell.Current.Navigation.PopModalAsync();
		}
	}
}