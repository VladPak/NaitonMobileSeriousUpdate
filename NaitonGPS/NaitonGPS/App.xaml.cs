using NaitonGPS.Services;
using NaitonGPS.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS
{
    public partial class App : Application
    {        
        public App()
        {            
            InitializeComponent();
        }

        protected override void OnStart()
        {
            bool isLoggedIn = Current.Properties.ContainsKey("IsLoggedIn") && Convert.ToBoolean(Current.Properties["IsLoggedIn"]);

            DependencyService.Register<MockDataStore>();
            if (isLoggedIn)
                MainPage = new AppShell();
            else
                MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
