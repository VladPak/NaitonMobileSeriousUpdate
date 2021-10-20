using NaitonGPS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NaitonGPS.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}