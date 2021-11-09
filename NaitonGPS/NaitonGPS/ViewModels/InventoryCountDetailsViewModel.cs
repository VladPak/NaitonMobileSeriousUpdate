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

		void Save(InventoryCount item)
		{
			_callback?.Invoke(this, _inventoryCount);
		}

		public string ProductName { get { return _inventoryCount.ProductName; } }
		public string ProductNameFormat { get { return _inventoryCount.ProductNameFormat; } }
		public string Stock { get { return $"{_inventoryCount.ShouldBeCount}"; } }
		public string Count
		{
			get { return $"{_inventoryCount.CountedStock}"; }
			set
			{
				if (!string.IsNullOrWhiteSpace(value) && float.TryParse(value, out var count))
				{
					_inventoryCount.CountedStock = count;
				}
			}
		}
		public string Damaged
		{
			get { return $"{_inventoryCount.Delta}"; }
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
