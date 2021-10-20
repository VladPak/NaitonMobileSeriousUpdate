using NaitonGPS.Helpers;
using NaitonGPS.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistSearchItemBottomPopup : ContentPage
    {
        public PicklistSearchItemBottomPopup()
        {
            InitializeComponent();

            var rackList = DataManager.GetPickRacks(0,0);
            BindingContext = new RacksViewModel(rackList);
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}