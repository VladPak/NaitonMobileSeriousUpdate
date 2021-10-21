using NaitonGPS.Models;
using NaitonGPS.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistQuantityBottomPopup : ContentPage
    {   
        public PicklistQuantityBottomPopup(PickListItem item, EventHandler<PickListItem> callBack)
        {
            InitializeComponent();            
            BindingContext = new PickListQuantityViewModel(item, callBack);
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync(true);
        }
    }
}