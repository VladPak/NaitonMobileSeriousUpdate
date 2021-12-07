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
	public partial class AddRackPage : ContentPage
	{
		private readonly AddRackViewModel _viewModel;
		public AddRackPage(EventHandler<Rack> callback)
		{
			InitializeComponent();
			BindingContext = _viewModel = new AddRackViewModel(callback);
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