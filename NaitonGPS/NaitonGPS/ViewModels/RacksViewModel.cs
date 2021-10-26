using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

namespace NaitonGPS.ViewModels
{
    public class RacksViewModel : BaseViewModel
    {
        private event EventHandler<Rack> CallBackMethod;
        private event EventHandler<bool> AfterLoadData;
        private readonly PickListItem _pickListItem;
        private string _searchText; 

        private List<Rack> SearchRacks { get; set; }
        private bool IsSearch { get; set; }
        

        public ObservableCollection<Rack> Racks { get; set; }
        public bool IsScanerConnected { get; set; }

        public Command LoadItemsCommand { get; }
        public Command<Rack> TappedItemCommand { get; set; }
        public Command ScanningCommand { get; set; }

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                SetProperty(ref _searchText, value);
                IsSearch = true;
                IsBusy = true;
            }
        }

        public RacksViewModel(PickListItem pickListItem, EventHandler<Rack> callBack, EventHandler<bool> afterLoadData)
        {
            _pickListItem = pickListItem;
            CallBackMethod = callBack;
            AfterLoadData = afterLoadData;
            Racks = new ObservableCollection<Rack>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            TappedItemCommand = new Command<Rack>(TappedItem);
            ScanningCommand = new Command(Scanning);
            IsBusy = true;
            LoadItems().GetAwaiter();
            IsBusy = false;                     
        }

        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                var rackItems = new List<Rack>();
                if (IsSearch)
                {
                    if (!string.IsNullOrEmpty(_searchText))
                    {
                        rackItems = SearchRacks.Where(x => x.RackName.ToLower().Contains(_searchText.ToLower())).ToList();
                        if (rackItems.Count == 1)
                            TappedItem(rackItems.First());
                    }
                    else
                    {
                        IsSearch = false;
                        rackItems = SearchRacks;
                    }
                }
                else
                {
                    rackItems = await DataManager.GetPickRacks(_pickListItem.DeliveryOrderDetailsId);
                    SearchRacks = rackItems;
                }
                    
                Racks.Clear();
                foreach (var item in rackItems)
                {
                    item.PickListItemId = _pickListItem.PickListOrderDetailsId;
                    Racks.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                AfterLoadData.Invoke(this, true);
            }
        }

        void TappedItem(Rack rack)
        {
            CallBackMethod.Invoke(this,rack);
        }

        async void Scanning()
        {
#if __ANDROID__
            // Initialize the scanner first so it can track the current context
            MobileBarcodeScanner.Initialize (Application);
#endif

            //start scanner
            var scanner = new MobileBarcodeScanner
            {
                CancelButtonText = "Cancel",
                TopText = "Hold the camera up to the barcode\nAbout 6 inches away",
                BottomText = "Wait for the barcode to automatically scan!"
            };

            //This will start scanning
            ZXing.Result result = await scanner.Scan();

            //Show the result returned.
            BarcodeDataReceivedEvent(result);
        }

        private void BarcodeDataReceivedEvent(ZXing.Result result)
        {
            if (result != null)
            {
                var item = Racks.FirstOrDefault(x=>x.RackName== result.Text);
                if (item != null)
                    TappedItem(item);
            }
            //App.Current.MainPage.DisplayAlert("Scanner", msg, "Ok");
        }
    }
}
