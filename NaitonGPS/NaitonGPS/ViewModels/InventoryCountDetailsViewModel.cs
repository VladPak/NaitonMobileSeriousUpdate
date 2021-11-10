using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
	public class InventoryCountDetailsViewModel : BaseViewModel
	{
		private readonly InventoryCount _inventoryCount = null;
		private EventHandler<InventoryCount> _callback = null;
		public InventoryCountDetailsViewModel(InventoryCount inventoryCount, EventHandler<InventoryCount> callback)
		{
			_inventoryCount = inventoryCount;
			_callback = callback;
			LoadItemsCommand = new Command(async () => await LoadItems());
			SaveCommand = new Command<InventoryCount>(Save);
		}

		public Command LoadItemsCommand { get; }
		public Command<InventoryCount> SaveCommand { get; set; }

		async Task LoadItems()
		{
			IsBusy = true;

			try
			{

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

		public string ProductName { get { return _inventoryCount.ProductName; } }
		public string ProductNameFormat { get { return _inventoryCount.ProductNameFormat; } }
		public string Stock
		{
			get
			{
				if (_inventoryCount.ShouldBeCount <= 0)
					return string.Empty;

				return $"{_inventoryCount.ShouldBeCount}";
			}
		}
		public string Count
		{
			get
			{
				if (_inventoryCount.CountedStock <= 0)
					return string.Empty;
				return $"{_inventoryCount.CountedStock}";
			}
			set
			{
				if (!string.IsNullOrWhiteSpace(value) && float.TryParse(value, out var count))
				{
					if (_inventoryCount.ShouldBeCount < count)
					{
						App.Current.MainPage.DisplayAlert("Sorry", "The value of count not to be more then Stock.", "Ok");
						return;
					}

					_inventoryCount.CountedStock = count;
					OnPropertyChanged(nameof(InventoryCount.CountedStock));
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
