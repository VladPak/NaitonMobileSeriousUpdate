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
		private List<InventoryCount> _list;
		private EventHandler<bool> _callback;

		public ObservableCollection<InventoryCount> List { get; set; }
		public IList<Rack> RackList { get; set; }

		public Command LoadItemsCommand { get; }

		public Command<InventoryCount> ItemTapped { get; }
		public Command AddRackButtonCommand { get; set; }

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

		public InventoryCountListViewModel(EventHandler<bool> callback)
		{
			_callback = callback;
			Title = "Count";
			List = new ObservableCollection<InventoryCount>();
			LoadItemsCommand = new Command(async () => await LoadItems());
			ItemTapped = new Command<InventoryCount>(OnItemSelected);
			AddRackButtonCommand = new Command(OnAddRack);

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
						inventoryCountList = _list.Where(x =>
														   x.ProductId.ToString().ToLower().Contains(_searchText.ToLower()) ||
														   (!string.IsNullOrWhiteSpace(x.BrandName) && x.BrandName.ToLower().Contains(_searchText.ToLower())) ||
														   (!string.IsNullOrWhiteSpace(x.StockRackName) && x.StockRackName.ToLower().Contains(_searchText.ToLower()))).ToList();

						_callback?.Invoke(this, inventoryCountList.Count > 0);
					}
					else
					{
						IsSearch = false;
						inventoryCountList = _list;
						_callback?.Invoke(this, inventoryCountList.Count > 0);
					}
				}
				else
				{
					_list = inventoryCountList = await DataManager.GetInventoryCount();
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

		private async void OnItemSelected(InventoryCount item)
		{
			if (item == null)
				return;
			await Shell.Current.Navigation.PushModalAsync(new InventoryCountDetailsPage(item, SetCount), true);
		}
		private async void OnAddRack()
		{
			await Shell.Current.Navigation.PushModalAsync(new AddRackPage(this.SelectedRack), true);
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

			await this.LoadItems();
		}
		private async void SelectedRack(object sender, Rack rack)
		{
			await DataManager.AddRack(943, new[] { (int)rack.StockId }, new[] { rack.StockRackId });
			await this.LoadItems();
		}
	}
}
