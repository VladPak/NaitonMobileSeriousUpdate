using NaitonGPS.Helpers;
using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZebraBarcodeScannerSDK;
using NaitonGPS.API;

namespace NaitonGPS.ViewModels
{
    public class RacksViewModel : BaseViewModel
    {
        private event EventHandler<Rack> CallBackMethod;
        private readonly PickListItem _pickListItem;
        private string _searchText;

        private List<Rack> _searchRacks { get; set; }
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
        
        public RacksViewModel(PickListItem pickListItem, EventHandler<Rack> callBack)
        {
            _pickListItem = pickListItem;
            CallBackMethod = callBack;
            Racks = new ObservableCollection<Rack>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            TappedItemCommand = new Command<Rack>(TappedItem);
            ScanningCommand = new Command(Scanning);
            IsBusy = true;
            LoadItems().GetAwaiter();
            IsBusy = false;
            SdkHandler.DeviceConnected += OnDeviceConnectedEventHandler;
            SdkHandler.EnableBluetoothScannerDiscovery();
            SdkHandler.SetSTCEnabledState(true);
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
                        rackItems = _searchRacks.Where(x => x.RackName.ToLower().Contains(_searchText.ToLower())).ToList();
                    }
                    else
                    {
                        rackItems = _searchRacks;
                    }
                }
                else
                {
                    rackItems = await Task.Run(() => DataManager.GetPickRacks(_pickListItem.DeliveryOrderDetailsId));
                    _searchRacks = rackItems;
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
            }
        }

        void TappedItem(Rack rack)
        {
            CallBackMethod.Invoke(this,rack);
        }

        void Scanning()
        {
            SdkHandler.BarcodeDataEvent += BarcodeDataReceivedEvent;
        }

        private static void BarcodeDataReceivedEvent(BarcodeData barcodeData, int scannerID)
        {
            App.Current.MainPage.DisplayAlert("Scanner", barcodeData.ToString(), "Ok");
        }

        private void OnDeviceConnectedEventHandler(object scannerobject, EventArgs argument)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Scanner scanner = (Scanner)scannerobject;
                Globals.ConnectedScanner = scanner;
                Globals.ConnectedId = scanner.Id.ToString();
                IsScanerConnected = true;
            });
        }
    }
}
