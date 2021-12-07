using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace NaitonGPS.ViewModels
{
	public class AddProductViewModel : BaseViewModel
	{
		private Product _selectedItem;
		private string _searchText;
		private List<Product> _productList;

		public ObservableCollection<Product> List { get; set; }

		public Command LoadItemsCommand { get; }

		public Command<Product> ItemTapped { get; }

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

		public AddProductViewModel()
		{
			Title = "Add product";
			List = new ObservableCollection<Product>();
			LoadItemsCommand = new Command(async () => await LoadItems());
			ItemTapped = new Command<Product>(OnItemSelected);
		}

		public Product SelectedItem
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

				var list = new List<Product>();
				if (IsSearch)
				{
					if (!string.IsNullOrEmpty(_searchText))
					{
						list = _productList.Where(x => x.ProductId.ToString().ToLower().Contains(_searchText.ToLower()) ||
														(!string.IsNullOrWhiteSpace(x.ProductName) ? x.ProductName.ToLower().Contains(_searchText.ToLower()) : false) ||
														(!string.IsNullOrWhiteSpace(x.ManufacturerCode) ? x.ManufacturerCode.ToLower().Contains(_searchText.ToLower()) : false) ||
														(!string.IsNullOrWhiteSpace(x.BarCode) ? x.BarCode.ToLower().Contains(_searchText.ToLower()) : false) ||
														(!string.IsNullOrWhiteSpace(x.BrandName) ? x.BrandName.ToLower().Contains(_searchText.ToLower()) : false) ||
														x.ProductGroup.ToString().Contains(_searchText.ToLower())).ToList();
					}

					List.Clear();
					foreach (var item in list)
					{
						List.Add(item);
					}
				}
				else
				{
					_productList = await DataManager.GetProducts(new[] { 943 });
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

		async void OnItemSelected(Product item)
		{
			if (item == null)
				return;
			//await Shell.Current.GoToAsync($"{nameof(PickListItemsPage)}?{nameof(Product.ProductId)}={item.ProductId}");
		}
	}
}
