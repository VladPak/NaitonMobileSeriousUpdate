using FreshMvvm;
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
        private readonly EventHandler<PickListItem> SCrollSelected;
        
        public ObservableCollection<PickListItem> PicklistItems { get; set; }
        public Command LoadItemsCommand { get; }
        public Command StartEditCommand { get; }

        public Command<PickListItem> ItemTapped { get; }
        public Command<PickListItem> ChangeQuantityCommand { get; }
        public Command<PickListItem> ChangeRackCommand { get; }
        public Command<PickListItem> ChangeStatusCommand { get; }
        public Command SaveToBaseCommand { get; set; }
        public Command<PickList> ShowDeliveryRemarkCommand { get; set; }
        public Command ClosePopupCommand { get; set; }

        public bool IsEditable { get; set; }
        public bool IsViewable { get; set; }
        public bool IsChanged { get; set; }
        public PickList PickList { get; set; }
        public bool IsScanning { get; set; }

        public PickListItemsViewModel(PickList pickList, bool isEditable, EventHandler<PickListItem> scrollToSelected)
        {            
            _pickListId = pickList.PickListId;
            if (scrollToSelected != null)
                SCrollSelected = scrollToSelected;
            IsEditable = isEditable;
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
            ClosePopupCommand = new Command(ClosePopup);

            IsBusy = true;
            LoadItems().GetAwaiter();
            IsBusy = false;
            IsViewable = !isEditable;
            SelectedItem = null;

            if (isEditable)
            {
                var scanner = FreshIOC.Container.Resolve<IScanner>();
                scanner.OnScanDataCollected -= ScannedDataCollected;
                scanner.OnStatusChanged -= ScannedStatusChanged;

                scanner.Enable();
                scanner.OnScanDataCollected += ScannedDataCollected;
                scanner.OnStatusChanged += ScannedStatusChanged;

                var config = new ZebraScannerConfig
                {
                    IsUPCE0 = false,
                    IsUPCE1 = false
                };

                scanner.SetConfig(config);
            }
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
                if (IsScanning)
                {
                    item.IsScanned = true;
                    IsScanning = false;
                    item.StatusId = 9;                    
                }                
                PicklistItems.Insert(insertIndex, item);
                if (PicklistItems.Count > (insertIndex + 1))
                {
                    var i = PicklistItems[insertIndex + 1];
                    SCrollSelected?.Invoke(this,i);
                }
            }
            if(newItem.Quantity > 0)
            {
                PicklistItems.Add(newItem);
                await App.Current.MainPage.DisplayAlert("Information", "Added a new row due to lack of quantity of products.", "Ok");
            }
            IsChanged = true;
        }

        async void SetRack(object sender,RackMobile item)
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
                else if (PickList.Products == countProducts)
                {
                    save = true;
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

        async void ClosePopup()
        {
            IsScanning = false;
            await Shell.Current.Navigation.PopModalAsync(true);
        }

        private async void ScannedDataCollected(object sender, StatusEventArgs a_status)
        {
            if (!IsScanning)
            {
                var item = PicklistItems.FirstOrDefault(x => x.RackName == a_status.Data.TrimEnd());
                if (item != null)
                {
                    if (SCrollSelected != null)
                        SCrollSelected.Invoke(this, item);
                    IsScanning = true;
                    if (!item.IsScanned)
                        ChangeQuantity(item);
                }
                else
                {
                    List<RackMobile> rackList = new List<RackMobile>();
                    foreach (var dod in PicklistItems.GroupBy(x=>x.DeliveryOrderDetailsId))
                    {
                        rackList.AddRange(await DataManager.GetPickRacks(dod.Key));
                        if (rackList.Any(x => x.RackName == a_status.Data.TrimEnd())
                            && rackList.Any(x => PicklistItems.Where(d => d.DeliveryOrderDetailsId == dod.Key).Select(d => d.PickListOrderDetailsId).ToArray().Contains(x.StockRackId)))
                            break;
                    }
                    var rack = rackList.FirstOrDefault(x=>x.RackName==a_status.Data.TrimEnd() 
                        && PicklistItems.Select(d => d.PickListOrderDetailsId).ToArray().Contains(x.StockRackId));

                    item = PicklistItems.FirstOrDefault(x => x.PickListOrderDetailsId == rack.StockRackId);

                    if (item != null && item.Quantity<=rack.QuantityInStock)
                    {
                        if (SCrollSelected != null)
                            SCrollSelected.Invoke(this, item);
                        item.RackName = rack.RackName;
                        item.StockRackId = rack.StockRackId;
                        IsScanning = true;
                        if (!item.IsScanned)
                            ChangeQuantity(item);
                    }
                    else
                        await App.Current.MainPage.DisplayAlert("Sorry", "This rack does not contain a product on this picklist", "Ok");
                }
            }
        }

        private void ScannedStatusChanged(object sender, string a_message)
        {
            string status = a_message;
            Console.WriteLine(status);
        }
    }
}
