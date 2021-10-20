using NaitonGPS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views.PickList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(PickListId), nameof(PickListId))]
    public partial class PickListItemsPage : ContentPage
    {
        private int _pickListId;

        public int PickListId
        {
            get
            {
                return _pickListId;
            }
            set
            {
                _pickListId = value;
                LoadItems(value);
            }
        }

        public PickListItemsPage()
        {
            InitializeComponent();            
        }

        void LoadItems(int pickListId)
        {
            BindingContext = new PickListItemsViewModel(pickListId);
        }
    }
}