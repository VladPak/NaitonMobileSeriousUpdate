using FreshMvvm;
using NaitonGPS.Models;
using System;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
    public class PickListQuantityViewModel : BaseViewModel
    {
        private PickListItem _newItem;
        public PickListItem NewItem 
        {
            get
            {
                return _newItem;
            }
            set
            {
                _newItem = value;
                SetProperty<PickListItem>(ref _newItem, value);
            }
        }


        public event EventHandler<PickListItem> SetQuantity;
        public Command SetQuantityCommand { get; set; }

        public PickListQuantityViewModel(PickListItem item, EventHandler<PickListItem> callBack)
        {
            NewItem = item;
            SetQuantity = callBack;
            SetQuantityCommand = new Command(SaveQuantity);

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

        private void SaveQuantity()
        {
            SetQuantity.Invoke(this, NewItem);
        }

        private void ScannedDataCollected(object sender, StatusEventArgs a_status)
        {
            if (NewItem.RackName == a_status.Data.TrimEnd())
            {
                SetQuantity.Invoke(this, NewItem);
            }
        }

        private void ScannedStatusChanged(object sender, string a_message)
        {
            string status = a_message;
            Console.WriteLine(status);
        }
    }
}
