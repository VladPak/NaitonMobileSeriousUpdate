using NaitonGPS.Helpers;
using NaitonGPS.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{    
    public class PickListItemsViewModel : BaseViewModel
    {
        private readonly int _pickListId;
        private PickListItem _selectedItem;

        public ObservableCollection<PickListItem> PicklistItems { get; set; }
        public Command LoadItemsCommand { get; }
        public Command StartEditCommand { get; }

        public Command<PickListItem> ItemTapped { get; }

        public bool IsEditable { get; set; }

        public PickListItemsViewModel(int pickListId)
        {
            _pickListId = pickListId;
            Title = "Picklist";
            PicklistItems = new ObservableCollection<PickListItem>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            ItemTapped = new Command<PickListItem>(OnItemSelected);
            StartEditCommand = new Command(StartEdit);

            IsBusy = true;
            LoadItems().GetAwaiter();
            IsBusy = false;

            SelectedItem = null;
        }


        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                var pickListItems = await Task.Run(()=> DataManager.GetPickListItems(_pickListId));
                PicklistItems.Clear();
                foreach (var item in pickListItems)
                {
                    PicklistItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public PickListItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(PickListItem item)
        {
            if (item == null)
                return;

            //await Shell.Current.GoToAsync($"{nameof(PickListItems)}?{nameof(.ItemId)}={item}");            
        }

        void StartEdit()
        {
            IsEditable = false;
        }
    }
}
