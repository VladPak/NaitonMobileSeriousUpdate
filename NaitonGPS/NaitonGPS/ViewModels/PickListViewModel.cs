using NaitonGPS.Helpers;
using NaitonGPS.Models;
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
    public class PickListViewModel : BaseViewModel
    {
        private PickList _selectedItem;
        private string _searchText;
        private List<PickList> _searchPicks;

        public ObservableCollection<PickList> Picklists { get; set; }

        public Command LoadItemsCommand { get; }

        public Command<PickList> ItemTapped { get; }
        public Command<PickList> ShowDeliveryRemarkCommand { get; set; }

        private bool IsSearch { get; set; }

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

        public PickListViewModel()
        {
            Title = "Picklist";
            Picklists = new ObservableCollection<PickList>();
            LoadItemsCommand = new Command(async () => await LoadItems());
            ItemTapped = new Command<PickList>(OnItemSelected);
            ShowDeliveryRemarkCommand = new Command<PickList>(ShowDeliveryRemark);
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

                var pickList = new List<PickList>();
                if (IsSearch)
                {
                    if (!string.IsNullOrEmpty(_searchText))
                    {
                        pickList = _searchPicks.Where(x => x.PickListId.ToString().ToLower().Contains(_searchText.ToLower())
                                                        || x.PickerName.ToLower().Contains(_searchText.ToLower())).ToList();                        
                    }
                    else
                    {
                        IsSearch = false;
                        pickList = _searchPicks;
                    }
                }
                else
                {
                    pickList = await Task.Run(() => DataManager.GetPickLists());
                    _searchPicks = pickList;
                }

                
                Picklists.Clear();
                foreach (var item in pickList)
                {
                    item.ColorStatus = item.GetColor();
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

        async void ShowDeliveryRemark(PickList item)
        {
            if (item == null)
                return;
            await Shell.Current.Navigation.PushModalAsync(new DeliveryRemarkPopup(item.Remark));
        }
    }
}
