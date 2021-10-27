using NaitonGPS.Services;
using NaitonGPS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NaitonGPS.Views.PickList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(PickListId), nameof(PickListId))]
    public partial class PickListItemsEditPage : ContentPage
    {
        private int _pickListId;
        private IDataManager DataManager => DependencyService.Get<IDataManager>();

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
        public PickListItemsEditPage()
        {
            InitializeComponent();
        }

        async void LoadItems(int pickListId)
        {
            var item = await DataManager.GetPickLists(pickListId);
            BindingContext = new PickListItemsViewModel(item.FirstOrDefault());
        }
    }
}