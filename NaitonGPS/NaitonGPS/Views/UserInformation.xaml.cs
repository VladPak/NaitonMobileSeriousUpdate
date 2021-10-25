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
			await Shell.Current.Navigation.PopModalAsync();
			Xamarin.Forms.Application.Current.Properties["IsLoggedIn"] = false;
			Shell.Current.Items.Clear();
			var tabbar = new TabBar();
			var shellContent = new ShellContent() { ContentTemplate = new DataTemplate(typeof(LoginPage)) };
			tabbar.Items.Add(shellContent);
			Shell.Current.Items.Add(tabbar);
		}

		private async void CloseMe(object sender, EventArgs e)
        {
			await Shell.Current.GoToAsync("..");
		}
    }
}