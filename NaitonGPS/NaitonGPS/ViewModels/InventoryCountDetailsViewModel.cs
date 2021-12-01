using NaitonGPS.Models;
using NaitonGPS.Views.InventoryCount;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
	public class InventoryCountDetailsViewModel : BaseViewModel
	{
		private readonly InventoryCount _inventoryCount = null;
		private readonly EventHandler<InventoryCount> _callback = null;
		public InventoryCountDetailsViewModel(InventoryCount inventoryCount, EventHandler<InventoryCount> callback)
		{
			_inventoryCount = inventoryCount;
			_callback = callback;
			LoadItemsCommand = new Command(async () => await LoadItems());
			SaveCommand = new Command<InventoryCount>(Save);
			AddProductCommand = new Command<InventoryCount>(AddProduct);
		}

		public Command LoadItemsCommand { get; }
		public Command<InventoryCount> SaveCommand { get; set; }
		public Command<InventoryCount> AddProductCommand { get; set; }

		async Task LoadItems()
		{
			IsBusy = true;

			try
			{
				await Task.Run(() => true);
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

		async void Save(InventoryCount item)
		{
			await Shell.Current.Navigation.PopModalAsync();
			_callback?.Invoke(this, _inventoryCount);
		}
		async void AddProduct(InventoryCount item)
		{
			if (item == null)
				return;
			await Shell.Current.Navigation.PushModalAsync(new AddProductPage(), true);
		}
		public InventoryCount InventoryCount => _inventoryCount;
		public string StockRackName { get { return _inventoryCount.StockRackName; } }
		public string ProductName { get { return _inventoryCount.ProductName; } }
		public string ProductNameFormat { get { return _inventoryCount.ProductNameFormat; } }
		public string Variants { get { return _inventoryCount.Variants?.Trim(); } }

		public string Stock
		{
			get
			{
				return $"{_inventoryCount.QuantityInStock}";
			}
		}
		public string Count
		{
			get
			{
				//if (_inventoryCount.Delta <= 0)
				//	return string.Empty;

				return $"{_inventoryCount.CountedStock}";
			}
			set
			{
				if (!string.IsNullOrWhiteSpace(value) && float.TryParse(value, out var count))
				{
					if (count < 0)
					{
						App.Current.MainPage.DisplayAlert("Error", "The value of count can not be negative!", "Ok");
						OnPropertyChanged(nameof(InventoryCountDetailsViewModel.Count));
						return;
					}

					_inventoryCount.CountedStock = count;
					//OnPropertyChanged(nameof(InventoryCount.CountedStock));
				}
			}
		}

		public string Damaged
		{
			get
			{
				if (_inventoryCount.Delta <= 0)
					return string.Empty;
				return $"{_inventoryCount.Delta}";
			}
			set
			{
				if (!string.IsNullOrWhiteSpace(value) && float.TryParse(value, out var delta))
				{
					_inventoryCount.Delta = delta;
				}
			}
		}
	}
}
