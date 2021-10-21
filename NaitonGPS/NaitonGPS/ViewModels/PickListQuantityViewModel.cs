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
        }

        private void SaveQuantity()
        {
            SetQuantity.Invoke(this, NewItem);
        }
    }
}
