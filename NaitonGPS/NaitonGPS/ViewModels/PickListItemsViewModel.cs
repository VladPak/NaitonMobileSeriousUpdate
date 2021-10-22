using NaitonGPS.Helpers;
using NaitonGPS.Models;
using NaitonGPS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
    public class PickListItemsViewModel : BaseViewModel
    {
        private readonly int _pickListId;
        private PickListItem _selectedItem;
        private List<PickListItem> _oldPickListItems;

        public ObservableCollection<PickListItem> PicklistItems { get; set; }
        public Command LoadItemsCommand { get; }
        public Command StartEditCommand { get; }

        public Command<PickListItem> ItemTapped { get; }
        public Command<PickListItem> ChangeQuantityCommand { get; }
        public Command<PickListItem> ChangeRackCommand { get; }
        public Command SaveToBaseCommand { get; set; }
        
        public bool IsEditable { get; set; }
        public bool IsViewable { get; set; }
        public bool IsChanged { get; set; }

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
            SaveToBaseCommand = new Command(SaveToBase);
            
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
                if (!IsChanged)
                {
                    var pickListItems = await Task.Run(() => DataManager.GetPickListItems(_pickListId));
                    _oldPickListItems = pickListItems;
                    PicklistItems.Clear();
                    foreach (var item in pickListItems)
                    {
                        PicklistItems.Add(item);
                    }
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
            SelectedItem = item;            
            await Shell.Current.Navigation.PushModalAsync(new PicklistQuantityBottomPopup(SelectedItem, SetQuantity), true);
        }

        async void ChangeRack(PickListItem item)
        {
            if (item == null) return;
            await Shell.Current.Navigation.PushModalAsync(new PicklistSearchItemBottomPopup(item, SetRack), true);
        }

        async void SetQuantity(object sender,PickListItem item)
        {
            await Shell.Current.Navigation.PopModalAsync();
            if (item == null) return;
            var oldItem = new PickListItem();
            int index = 0;
            int insertIndex = 0;
            foreach (var pli in PicklistItems)
            {
                if (item.PickListOrderDetailsId == pli.PickListOrderDetailsId)
                {
                    oldItem = pli;
                    insertIndex = index;
                }
                index++;
            }
            if (oldItem.PickListOrderDetailsId != 0)
            {
                PicklistItems.Remove(oldItem);
                PicklistItems.Insert(insertIndex,item);
            }
            IsChanged = true;
        }

        async void SetRack(object sender,Rack item)
        {
            await Shell.Current.Navigation.PopModalAsync();
            if (item == null) return;
            var oldItem = new PickListItem();
            int index = 0;
            int insertIndex = 0;
            foreach (var pli in PicklistItems)
            {
                if (item.PickListItemId == pli.PickListOrderDetailsId)
                {
                    oldItem = pli;
                    insertIndex = index;
                }
                index++;
            }
            if (oldItem.PickListOrderDetailsId != 0)
            {
                var newItem = oldItem;
                PicklistItems.Remove(oldItem);
                newItem.RackName = item.RackName;
                newItem.StockRackId = item.StockRackId;
                PicklistItems.Insert(insertIndex, newItem);
            }
            IsChanged = true;
        }

        void StartEdit()
        {
            IsEditable = true;
            IsViewable = false;
            OnPropertyChanged(nameof(IsViewable));
            OnPropertyChanged(nameof(IsEditable));            
        }

        void SaveToBase() 
        {
            bool save = true;
            var list = new List<PickListItem>();

            foreach (var dod in _oldPickListItems.GroupBy(x=>x.DeliveryOrderDetailsId))
            {
                decimal countProducts = 0;
                foreach (var pli in PicklistItems.Where(x=>x.DeliveryOrderDetailsId==dod.Key))
                {
                    list.Add(pli);
                    countProducts += pli.Quantity;
                }
                if (countProducts != _oldPickListItems.Where(x=>x.DeliveryOrderDetailsId==dod.Key).Sum(x => x.Quantity))
                    save = false;
            }

            if (save)
            {
                int result = DataManager.SavePickListItems(list);
                App.Current.MainPage.DisplayAlert("Success", "The data has been saved!", "Ok");
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Sorry", "The number of products in the deliveries does not match. Please reconsider the number of products.", "Ok");                

            }
        }
    }
}
