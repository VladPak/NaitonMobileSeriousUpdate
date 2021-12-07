using FreshMvvm;
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
		private readonly EventHandler<Rack> _callback;
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

		public AddRackViewModel(EventHandler<Rack> callback)
		{
			_callback = callback;
			Title = "Add rack";
			List = new ObservableCollection<Rack>();
			LoadItemsCommand = new Command(async () => await LoadItems());
			ItemTapped = new Command<Rack>(OnItemSelected);

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
					var racks = await DataManager.GetStockRacksProducts(943, 0, _searchText, _searchText);
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

			_callback?.Invoke(this, item);
		}
	}
}
