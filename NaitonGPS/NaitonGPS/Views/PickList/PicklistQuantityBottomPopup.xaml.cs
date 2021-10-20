using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PicklistQuantityBottomPopup : ContentPage
    {        
        public PicklistQuantityBottomPopup()
        {
            InitializeComponent();            
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync(true);
        }
    }
}