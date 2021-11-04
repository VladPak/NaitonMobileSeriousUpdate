using NaitonGPS.Models;
using NaitonGPS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
	public class InventoryCountListViewModel : BaseViewModel
	{
		private InventoryCount _selectedItem = null;
		private string _searchText;
		private List<InventoryCount> _search;

		public ObservableCollection<InventoryCount> List { get; set; }

		public Command LoadItemsCommand { get; }

		public Command<InventoryCount> ItemTapped { get; }
		public Command<InventoryCount> ShowDeliveryRemarkCommand { get; set; }

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

		public InventoryCountListViewModel()
		{
			Title = "Count";
			List = new ObservableCollection<InventoryCount>();
			LoadItemsCommand = new Command(async () => await LoadItems());
			ItemTapped = new Command<InventoryCount>(OnItemSelected);
			ShowDeliveryRemarkCommand = new Command<InventoryCount>(ShowDeliveryRemark);
		}

		public InventoryCount SelectedItem
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

				var inventoryCountList = new List<InventoryCount>();
				if (IsSearch)
				{
					if (!string.IsNullOrEmpty(_searchText))
					{
						inventoryCountList = _search.Where(x => x.ProductId.ToString().ToLower().Contains(_searchText.ToLower()) || x.BrandName.ToLower().Contains(_searchText.ToLower())).ToList();
					}
					else
					{
						IsSearch = false;
						inventoryCountList = _search;
					}
				}
				else
				{
					_search = inventoryCountList = await DataManager.GetInventoryCount();
				}


				List.Clear();
				inventoryCountList.ForEach(item => List.Add(item));
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

		async void OnItemSelected(InventoryCount item)
		{
			if (item == null)
				return;
			await Shell.Current.Navigation.PushModalAsync(new InventoryCountDetailsPage(), true);
			//await Shell.Current.GoToAsync($"{nameof(InventoryCountDetailsPage)}?{nameof(InventoryCount.ProductCountId)}={item.ProductCountId}");
		}
		async void ShowDeliveryRemark(InventoryCount item)
		{
			if (item == null)
				return;
			//await Shell.Current.Navigation.PushModalAsync(new DeliveryRemarkPopup(item.Remark));
		}
	}
}
