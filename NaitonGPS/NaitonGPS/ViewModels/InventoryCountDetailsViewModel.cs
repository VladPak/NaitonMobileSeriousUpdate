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
		public InventoryCountDetailsViewModel(InventoryCount inventoryCount)
		{
			_inventoryCount = inventoryCount;
			LoadItemsCommand = new Command(async () => await LoadItems());
		}

		public Command LoadItemsCommand { get; }

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

		public string ProductName { get { return _inventoryCount.ProductName; } }

		public string ProductNameFormat { get { return _inventoryCount.ProductNameFormat; } }

		public string Stock { get { return $"{_inventoryCount.CountedStock}"; } }
		public string Damaged { get { return $"{_inventoryCount.Delta}"; } }
	}
}
