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
	public class OrderListViewModel : BaseViewModel
	{
		private Order _selectedItem = null;
		private string _searchText;
		private List<Order> _searchOrder;

		public ObservableCollection<Order> List { get; set; }

		public Command LoadItemsCommand { get; }

		public Command<Order> ItemTapped { get; }
		public Command<Order> ShowDeliveryRemarkCommand { get; set; }

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

		public OrderListViewModel()
		{
			Title = "Order";
			List = new ObservableCollection<Order>();
			LoadItemsCommand = new Command(async () => await LoadItems());
			ItemTapped = new Command<Order>(OnItemSelected);
		}

		public Order SelectedItem
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

				var Order = new List<Order>();
				if (IsSearch)
				{
					if (!string.IsNullOrEmpty(_searchText))
					{
						Order = _searchOrder.Where(x => x.OrderId.ToString().ToLower().Contains(_searchText.ToLower()) || x.ClientName.ToLower().Contains(_searchText.ToLower())).ToList();
					}
					else
					{
						IsSearch = false;
						Order = _searchOrder;
					}
				}
				else
				{
					_searchOrder = Order = await DataManager.GetOrders();
				}


				List.Clear();
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

		async void OnItemSelected(Order item)
		{
			if (item == null)
				return;
			await Shell.Current.GoToAsync($"{nameof(OrderDetailsPage)}?{nameof(Order.OrderId)}={item.OrderId}");
		}
	}
}
