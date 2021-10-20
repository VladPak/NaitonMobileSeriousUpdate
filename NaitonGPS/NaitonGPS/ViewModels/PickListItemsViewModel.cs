using NaitonGPS.Helpers;
using NaitonGPS.Models;
using NaitonGPS.Views;
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
        public Command<PickListItem> ChangeQuantityCommand { get; }
        public Command<PickListItem> ChangeRackCommand { get; }

        public bool IsEditable { get; set; }
        public bool IsViewable { get; set; }

        public PickListItemsViewModel(int pickListId)
        {
            _pickListId = pickListId;
            Title = "Picklist";
            PicklistItems = new ObservableCollection<PickListItem>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            ItemTapped = new Command<PickListItem>(OnItemSelected);
            ChangeQuantityCommand = new Command<PickListItem>(ChangeQuantity);
            ChangeRackCommand = new Command<PickListItem>(ChangeRack);
            StartEditCommand = new Command(StartEdit);

            IsBusy = true;
            LoadItems().GetAwaiter();
            IsBusy = false;
            IsViewable = true;            
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

        async void ChangeQuantity(PickListItem item)
        {
            if (item == null) return;
            await Shell.Current.Navigation.PushModalAsync(new PicklistQuantityBottomPopup(), true);
        }

        async void ChangeRack(PickListItem item)
        {
            if (item == null) return;
            await Shell.Current.Navigation.PushModalAsync(new PicklistSearchItemBottomPopup(), true);
        }

        void StartEdit()
        {            
            IsEditable = true;
            IsViewable = false;
            OnPropertyChanged(nameof(IsViewable));
            OnPropertyChanged(nameof(IsEditable));            
        }
    }
}
