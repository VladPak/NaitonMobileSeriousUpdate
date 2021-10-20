using NaitonGPS.Helpers;
using NaitonGPS.Models;
using NaitonGPS.Views.PickList;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
    public class PickListViewModel : BaseViewModel
    {
        private PickList _selectedItem;
        public ObservableCollection<PickList> Picklists { get; set; }

        public Command LoadItemsCommand { get; }

        public Command<PickList> ItemTapped { get; }
            
        
        public PickListViewModel()
        {
            Title = "Picklist";
            Picklists = new ObservableCollection<PickList>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            ItemTapped = new Command<PickList>(OnItemSelected);            
        }

        public PickList SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        async Task LoadItems()
        {
            IsBusy = true;

            try
            {
                var pickList = await Task.Run(()=> DataManager.GetPickLists());
                Picklists.Clear();
                foreach (var item in pickList)
                {
                    Picklists.Add(item);
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

        async void OnItemSelected(PickList item)
        {
            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(PickListItemsPage)}?{nameof(PickList.PickListId)}={item.PickListId}");            
        }
    }
}
