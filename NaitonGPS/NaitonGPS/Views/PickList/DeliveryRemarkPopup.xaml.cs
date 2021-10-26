using NaitonGPS.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views.PickList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeliveryRemarkPopup : ContentPage
    {
        public DeliveryRemarkPopup(string deliveryRemark)
        {
            InitializeComponent();
            BindingContext = new DeliveryRemarkViewModel(deliveryRemark);
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync(true);
        }
    }
}