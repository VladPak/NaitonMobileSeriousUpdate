using NaitonGPS.Interfaces;
using NaitonGPS.Models;
using NaitonGPS.Services;
using NaitonGPS.ViewModels;
using NaitonGPS.Views;
using NaitonGPS.Views.PickList;
using Newtonsoft.Json;
using SimpleWSA;
using SimpleWSA.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NaitonGPS
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public IDataManager DataManager => DependencyService.Get<IDataManager>();
		public IRoleManager RoleManager => DependencyService.Get<IRoleManager>();

		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
			Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
			Routing.RegisterRoute(nameof(PickListItemsPage), typeof(PickListItemsPage));
			Routing.RegisterRoute(nameof(PickListItemsEditPage), typeof(PickListItemsEditPage));

			Routing.RegisterRoute(nameof(OrderListPage), typeof(OrderListPage));
			Routing.RegisterRoute(nameof(OrderDetailsPage), typeof(OrderDetailsPage));

			Routing.RegisterRoute(nameof(InventoryCountListPage), typeof(InventoryCountListPage));
			Routing.RegisterRoute(nameof(InventoryCountDetailsPage), typeof(InventoryCountDetailsPage));

			SetMenuItems().GetAwaiter();
		}

		public async Task SetMenuItems()
		{

			try
			{
				UserLoginDetails userData = JsonConvert.DeserializeObject<UserLoginDetails>((string)App.Current.Properties["UserDetail"]);

				Session session = new Session(userData.UserEmail,
												userData.UserPassword,
												userData.IsEncrypted,
												userData.AppId,
												userData.AppVersion,
												userData.Domain,
												null);

				await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");

				DataManager.SetUser(out int roleId);

				var roles = DataManager.GetRoles(roleId);

				RoleManager.Set(roles.ToList());

				var res = ScreenTemplatesViewModel.Screens.Where(screen => roles.Any(role => role.Object.Equals(screen.RoleRight) && role.ObjectTypeId == (int)ObjectType.Form && role.IsChecked)).ToList();

				if (res.Count() > 0)
				{
					var tabBar = new TabBar();
					foreach (var item in res)
					{
						//item.Title = "Picklist";
						tabBar.Items.Add(item);
					}
					ShellContent shi = new ShellContent() { Title = "Zebra", Icon = "picklist.png", Route = "ItemsPage", ContentTemplate = new DataTemplate(typeof(ItemsPage)) };
					tabBar.Items.Add(shi);
					appShell.Items.Add(tabBar);
				}
				else
				{
					TabBar tb = new TabBar();
					ShellContent shi = new ShellContent() { Title = "Login", Icon = "picklist.png", Route = "LoginPage", ContentTemplate = new DataTemplate(typeof(LoginPage)) };
					tb.Items.Add(shi);
					appShell.Items.Add(tb);
				}
			}
			catch (RestServiceException ex)
			{
				if (ex.Code == "MI008")
				{
					await DisplayAlert("", "The session is refreshed", "Ok");
					await SessionContext.RefreshAsync();
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("", ex.Message, "Haha");
			}
		}
	}
}
