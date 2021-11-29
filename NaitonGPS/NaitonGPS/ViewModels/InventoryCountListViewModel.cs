using FreshMvvm;
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
		private InventoryCountListPage _page;
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

		public InventoryCountListViewModel(InventoryCountListPage page)
		{
			_page = page;
			Title = "Count";
			List = new ObservableCollection<InventoryCount>();
			LoadItemsCommand = new Command(async () => await LoadItems());
			ItemTapped = new Command<InventoryCount>(OnItemSelected);


			var scanner = FreshIOC.Container.Resolve<IScanner>();

			scanner.Enable();
			scanner.OnScanDataCollected += Scanner_OnScanDataCollected;
			scanner.SetConfig(new ZebraScannerConfig
			{
				IsUPCE0 = false,
				IsUPCE1 = false
			});
		}

		private void Scanner_OnScanDataCollected(object sender, StatusEventArgs e)
		{
			this.SearchText = e.Data;
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
						inventoryCountList = _search.Where(x =>
														   x.ProductId.ToString().ToLower().Contains(_searchText.ToLower()) ||
														   (!string.IsNullOrWhiteSpace(x.BrandName) && x.BrandName.ToLower().Contains(_searchText.ToLower())) ||
														   (!string.IsNullOrWhiteSpace(x.StockRackName) && x.StockRackName.ToLower().Contains(_searchText.ToLower()))).ToList();
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
				if (IsSearch || string.IsNullOrWhiteSpace(_searchText))
				{
					_page.rv.IsVisible = inventoryCountList.Count > 0;
					_page.NotFoundLabel.IsVisible = inventoryCountList.Count <= 0;
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

		private async void OnItemSelected(InventoryCount item)
		{
			if (item == null)
				return;
			await Shell.Current.Navigation.PushModalAsync(new InventoryCountDetailsPage(item, SetCount), true);
		}
		private async void SetCount(object sender, InventoryCount item)
		{
			var result = await DataManager.SetCount(item.BusinessId,
													item.StockId,
													item.StockRackId,
													item.ProductId != null ? new[] { item.ProductId.Value } : null,
													item.BatchId != null ? new[] { item.BatchId.Value } : null,
													new[] { item.CountedStock },
													new[] { item.ProductCountId });
			if (result < 0)
			{

			}
		}
	}
}
