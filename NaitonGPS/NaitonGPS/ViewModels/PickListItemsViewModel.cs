using NaitonGPS.Models;
using NaitonGPS.Views;
using NaitonGPS.Views.PickList;
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
        
        public ObservableCollection<PickListItem> PicklistItems { get; set; }
        public Command LoadItemsCommand { get; }
        public Command StartEditCommand { get; }

        public Command<PickListItem> ItemTapped { get; }
        public Command<PickListItem> ChangeQuantityCommand { get; }
        public Command<PickListItem> ChangeRackCommand { get; }
        public Command<PickListItem> ChangeStatusCommand { get; }
        public Command SaveToBaseCommand { get; set; }
        public Command<PickList> ShowDeliveryRemarkCommand { get; set; }

        public bool IsEditable { get; set; }
        public bool IsViewable { get; set; }
        public bool IsChanged { get; set; }
        public PickList PickList { get; set; }

        public PickListItemsViewModel(PickList pickList)
        {            
            _pickListId = pickList.PickListId;
            PickList = pickList;
            Title = "Picklist";
            PicklistItems = new ObservableCollection<PickListItem>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            ItemTapped = new Command<PickListItem>(OnItemSelected);
            ChangeQuantityCommand = new Command<PickListItem>(ChangeQuantity);
            ChangeRackCommand = new Command<PickListItem>(ChangeRack);
            ChangeStatusCommand = new Command<PickListItem>(ChangeStatus);
            StartEditCommand = new Command(StartEdit);
            SaveToBaseCommand = new Command(SaveToBase);
            ShowDeliveryRemarkCommand = new Command<PickList>(ShowDeliveryRemark);

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
                    var pickListItems = await DataManager.GetPickListItems(_pickListId);                                        
                    PicklistItems.Clear();
                    foreach (var item in pickListItems.OrderBy(x => x.StatusId).ThenBy(x => x.Sequence))
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

        /*async*/ void OnItemSelected(PickListItem item)
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

        void ChangeStatus(PickListItem item)
        {
            if (item == null) return;

            var oldItem = item;
            int index = 0;
            foreach (var pli in PicklistItems)
            {
                if (item.PickListOrderDetailsId == pli.PickListOrderDetailsId)
                {
                    int insertIndex = index;
                }
                index++;
            }
            
            PicklistItems.Remove(oldItem);
            if (item.StatusId != 9)
            {
                item.StatusId = 9;                
                PicklistItems.Add(item);
            }
            else
            {
                item.StatusId = 3;                
                PicklistItems.Insert(0, item);
            }
            
            IsChanged = true;
            IsBusy = true;
        }

        async void SetQuantity(object sender,PickListItem item)
        {
            await Shell.Current.Navigation.PopModalAsync();
            if (item == null) return;
            var oldItem = new PickListItem();
            int index = 0;
            int insertIndex = 0;
            var newItem = new PickListItem();
            foreach (var pli in PicklistItems)
            {
                if (item.PickListOrderDetailsId == pli.PickListOrderDetailsId)
                {
                    oldItem = pli;
                    insertIndex = index;
                    if (oldItem.Quantity > item.Quantity)
                    {
                        newItem.DeliveryOrderDetailsId = item.DeliveryOrderDetailsId;
                        newItem.Quantity = oldItem.Quantity - item.Quantity;
                        newItem.RackName = item.RackName;
                        newItem.PickListId = item.PickListId;
                        newItem.ProductId = item.ProductId;
                        newItem.Sizes = item.Sizes;
                        newItem.StatusId = 3;
                        newItem.StockRackId = item.StockRackId;
                    }
                }
                index++;
            }
            if (oldItem.PickListOrderDetailsId != 0)
            {
                PicklistItems.Remove(oldItem);
                PicklistItems.Insert(insertIndex,item);
            }
            if(newItem.Quantity > 0)
            {
                PicklistItems.Add(newItem);
                await App.Current.MainPage.DisplayAlert("Information", "Added a new row due to lack of quantity of products.", "Ok");
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

        async void StartEdit()
        {
            IsEditable = true;
            IsViewable = false;
            OnPropertyChanged(nameof(IsViewable));
            OnPropertyChanged(nameof(IsEditable));
            
            await Shell.Current.GoToAsync($"{nameof(PickListItemsEditPage)}?{nameof(PickList.PickListId)}={PickList.PickListId}");
        }

        async void SaveToBase() 
        {
            bool save = true;
            var list = new List<PickListItem>();
            decimal countProducts = 0;

            foreach (var dod in PicklistItems.GroupBy(x=>x.DeliveryOrderDetailsId))
            {
                foreach (var pli in PicklistItems.Where(x=>x.DeliveryOrderDetailsId==dod.Key))
                {
                    list.Add(pli);
                    countProducts += pli.Quantity;
                }
                //if (countProducts != PicklistItems.Where(x=>x.DeliveryOrderDetailsId==dod.Key).Sum(x => x.Quantity))
                if (PickList.Products != countProducts)
                {
                    save = false;
                }
            }


            if (save)
            {
                string result = await DataManager.SavePickListItems(list);
                IsChanged = false;                
                await App.Current.MainPage.DisplayAlert("Message", result, "Ok");
                IsBusy = true;
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Sorry", "The number of products in the deliveries does not match. Please reconsider the number of products.", "Ok");                

            }
        }

        async void ShowDeliveryRemark(PickList item)
        {
            if (item == null)
                return;
            await Shell.Current.Navigation.PushModalAsync(new DeliveryRemarkPopup(item.Remark));
        }
    }
}
