using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
	public class AddRackViewModel : BaseViewModel
	{
		private Rack _selectedItem;
		private string _searchText;
		private List<Rack> _searched;

		public ObservableCollection<Rack> List { get; set; }

		public Command LoadItemsCommand { get; }
		public Command<Rack> ItemTapped { get; }

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

		public AddRackViewModel()
		{
			Title = "Add rack";
			List = new ObservableCollection<Rack>();
			LoadItemsCommand = new Command(async () => await LoadItems());
			ItemTapped = new Command<Rack>(OnItemSelected);
		}

		public Rack SelectedItem
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

				var list = new List<Rack>();
				if (IsSearch && !string.IsNullOrWhiteSpace(_searchText))
				{
					var racks = await DataManager.GetRacks(0, _searchText, _searchText);
					List.Clear();
					foreach (var item in racks)
					{
						List.Add(item);
					}
				}
				else
				{
					List.Clear();
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

		async void OnItemSelected(Rack item)
		{
			if (item == null)
				return;
			//await Shell.Current.GoToAsync($"{nameof(PickListItemsPage)}?{nameof(Product.ProductId)}={item.ProductId}");
		}
	}
}
