using NaitonGPS.Models;
using NaitonGPS.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistSearchItemBottomPopup : ContentPage
    {
        public PicklistSearchItemBottomPopup(PickListItem pickListItem, EventHandler<Rack> callBack)
        {
            InitializeComponent();
            BindingContext = new RacksViewModel(pickListItem,callBack);
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }
    }
}