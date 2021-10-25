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
	public partial class UserInformation : ContentPage
	{
		public UserInformation ()
		{
			InitializeComponent ();
		}

        private async void Logout(object sender, EventArgs e)
        {
			Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = bool.FalseString;
			App.Current.MainPage = new NavigationPage(new LoginPage());
		}

		private async void CloseMe(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync("..");
		}
    }
}