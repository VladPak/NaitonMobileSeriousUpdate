using NaitonGPS.Models;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NaitonGPS.Services
{
	public class DataManager : IDataManager
	{
		private static UserLoginDetails _user;
		static int count = 0;

		public async Task<bool> GetWebService(string webserviceLink)
		{
			string webservice = String.Format("https://connectionprovider.naiton.com/DataAccess/{0}/restservice/address", webserviceLink);

			try
			{
				var httpClient = new HttpClient();
				var response = await httpClient.GetAsync(webservice);
				var responseContent = await response.Content.ReadAsStringAsync();
				var rsToString = responseContent.ToString();

				Preferences.Set("webservicelink", rsToString);

				if (!response.IsSuccessStatusCode)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
			}

			return false;
		}

		#region Pick list

		public async Task<List<PickListItem>> GetPickListItems(int pickListId)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklistracks");
				command.Parameters.Add("_picklistids", PgsqlDbType.Integer | PgsqlDbType.Array, new int[] { pickListId });
				command.Parameters.Add("_limit", PgsqlDbType.Integer, 100);

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, PickListItem[]>>(xmlResult);

				var pickListItems = dict.First().Value.ToList();
				return pickListItems;
			}
			catch (Exception ex)
			{

				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetPickListItems(pickListId);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<PickListItem>();
				}
			}

		}

		public async Task<List<PickList>> GetPickLists(int? pickListId = null)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklists");
				command.Parameters.Add("_picklistid", PgsqlDbType.Integer, pickListId != null ? pickListId : (object)DBNull.Value);
				//command.Parameters.Add("_statusid", PgsqlDbType.Integer, 3);
				command.Parameters.Add("_pickerid", PgsqlDbType.Integer, _user.PersonId);
				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);
				var dict = JsonConvert.DeserializeObject<Dictionary<string, PickList[]>>(xmlResult);

				var pickList = dict.First().Value.ToList();

				return pickList;
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetPickLists(pickListId);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<PickList>();
				}
			}
		}

		public async Task<List<Rack>> GetPickRacks(int deliveryOrderDetailsId)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpickracksformobile");
				command.Parameters.Add("_deliveryorderdetailsid", PgsqlDbType.Integer, deliveryOrderDetailsId);

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, Rack[]>>(xmlResult);

				var rackList = dict.First().Value.ToList();

				return rackList.ToList();
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetPickRacks(deliveryOrderDetailsId);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<Rack>();
				}
			}
		}

		public async Task<string> SavePickListItems(List<PickListItem> items)
		{
			try
			{
				string result = "";
				foreach (var item in items.GroupBy(x => x.DeliveryOrderDetailsId))
				{
					SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_addupdateracks");
					command.Parameters.Add("_deliveryorderdetailsid", PgsqlDbType.Integer, item.Key);
					command.Parameters.Add("_picklistorderdetailsids", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.PickListOrderDetailsId).ToArray());
					command.Parameters.Add("_stockrackids", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.StockRackId ?? 0).ToArray());
					command.Parameters.Add("_statusids", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.StatusId).ToArray());
					command.Parameters.Add("_quantities", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.Quantity).ToArray());

					command.WriteSchema = WriteSchema.TRUE;
					string xmlResult = SimpleWSA.Command.Execute(command,
														RoutineType.NonQuery,
														httpMethod: SimpleWSA.HttpMethod.GET,
														responseFormat: ResponseFormat.JSON);

					var dict = JsonConvert.DeserializeObject<Dictionary<string, ReturnScaler>>(xmlResult);
					int r = dict.FirstOrDefault().Value.Value;
					switch (r)
					{
						case -2:
							result += $"The number of products in the deliveries does not match. Please reconsider the number of products. For delivery {item.FirstOrDefault().DeliveryOrderDetailsId}";
							break;
						case -3:
							result += $"Not enough products on the rack {item.FirstOrDefault().RackName}";
							break;
					}
				}

				if (string.IsNullOrEmpty(result))
					result = "Successfully saved in database.";
				return result;
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await SavePickListItems(items);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return "Error on function database or server service";
				}
			}
		}

		#endregion Pick list


		#region Account 

		public UserLoginDetails RegistrationServiceSession()
		{
			try
			{

				Preferences.Set("token", SessionContext.Token);

				UserLoginDetails userLoginDetails = new UserLoginDetails
				{
					UserEmail = SessionContext.Login,
					UserPassword = SessionContext.Password,
					UserToken = SessionContext.Token,
					AppId = SessionContext.AppId,
					AppVersion = SessionContext.AppVersion,
					IsEncrypted = SessionContext.IsEncrypted,
					ConnectionProviderAddress = "https://connectionprovider.naiton.com/",
					Domain = SessionContext.Domain
				};
				_user = userLoginDetails;

				return userLoginDetails;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}
		}

		public void SetUser(out int roleId)
		{
			roleId = 0;
			UserLoginDetails dataFinalizeUserEP = JsonConvert.DeserializeObject<UserLoginDetails>((string)App.Current.Properties["UserDetail"]);
			var userid = dataFinalizeUserEP.PersonId;
			SimpleWSA.Command commandGetAllUserData = new SimpleWSA.Command("userlogin_checklogin5");
			commandGetAllUserData.Parameters.Add("_login", PgsqlDbType.Varchar).Value = dataFinalizeUserEP.UserEmail;
			commandGetAllUserData.Parameters.Add("_password", PgsqlDbType.Varchar).Value = dataFinalizeUserEP.UserPassword;
			commandGetAllUserData.WriteSchema = WriteSchema.TRUE;

			string xmlResult1 = SimpleWSA.Command.Execute(commandGetAllUserData,
														RoutineType.DataSet,
														httpMethod: SimpleWSA.HttpMethod.GET,
														responseFormat: ResponseFormat.JSON);

			var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, UserDetails[]>>(xmlResult1);
			var newUser = dataFinalize.First().Value.First();

			if (_user is null)
				RegistrationServiceSession();

			_user.RoleId = newUser.RoleRightId;
			_user.PersonId = newUser.EmployeeId;

			roleId = dataFinalize["userlogin_checklogin5"].Select(x => x.RoleRightId).First();
		}

		public UserLoginDetails GetCurrentUser()
		{
			return _user;
		}

		//public Roles[] GetRoles(int roleId)
		//{
		//	SimpleWSA.Command command = new SimpleWSA.Command("rolemanager_getcheckroleobjects");
		//	command.Parameters.Add("_roleid", PgsqlDbType.Integer).Value = roleId;
		//	command.WriteSchema = WriteSchema.TRUE;
		//	string xmlResult = SimpleWSA.Command.Execute(command,
		//										RoutineType.DataSet,
		//										httpMethod: SimpleWSA.HttpMethod.GET,
		//										responseFormat: ResponseFormat.JSON);

		//	var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, Roles[]>>(xmlResult);
		//	var allRoles = dataFinalize.Values.ToList();
		//	var mobile = allRoles.SelectMany(i => i).Where(x => x.ObjectTypeId == 2 && x.TypeId == 6).ToArray();
		//	return mobile;
		//}
		public IEnumerable<Roles> GetRoles(int roleId)
		{
			SimpleWSA.Command command = new SimpleWSA.Command("rolemanager_getroleobjects");
			command.Parameters.Add("_roleid", PgsqlDbType.Integer).Value = roleId;
			command.WriteSchema = WriteSchema.TRUE;
			string xmlResult = SimpleWSA.Command.Execute(command,
												RoutineType.DataSet,
												httpMethod: SimpleWSA.HttpMethod.GET,
												responseFormat: ResponseFormat.JSON);

			var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, Roles[]>>(xmlResult);
			var allRoles = dataFinalize.Values.ToList();
			var mobile = allRoles.SelectMany(i => i).Where(x => x.TypeId == (int)RoleType.Mobile);
			return mobile;
		}
		#endregion Account


		public async Task NewSession(string email, string password)
		{
			string currentAppVersion = VersionTracking.CurrentVersion;
			Session session = new Session(email,
											password,
											false,
											6,
											currentAppVersion,
											Preferences.Get("loginCompany", string.Empty),
											null);
			await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");
		}

		#region Order
		public async Task<List<Order>> GetOrders(int[] orderIds = null)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("ordermanager_getfilteredorders_iod");

				command.Parameters.Add("_orderid", PgsqlDbType.Integer | PgsqlDbType.Array).Value = orderIds ?? (object)DBNull.Value;
				command.Parameters.Add("_clientid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_clientorcompany", PgsqlDbType.Varchar).Value = string.Empty;
				command.Parameters.Add("_sourceid", PgsqlDbType.Integer | PgsqlDbType.Array).Value = new int[] { 0, 1, 2, 3, 4 };
				command.Parameters.Add("_orderstatusid", PgsqlDbType.Integer | PgsqlDbType.Array).Value = new int[] { 0, 1, 3 };
				command.Parameters.Add("_businessid", PgsqlDbType.Integer | PgsqlDbType.Array).Value = new int[] { 943 };
				command.Parameters.Add("_deliveryid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_availabilityid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_paymentstatusid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_invoicestatusid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_productid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_productname", PgsqlDbType.Varchar).Value = string.Empty;
				command.Parameters.Add("_datefrom", PgsqlDbType.Timestamp).Value = DBNull.Value;
				command.Parameters.Add("_dateto", PgsqlDbType.Timestamp).Value = DBNull.Value;
				command.Parameters.Add("_clientreference", PgsqlDbType.Varchar).Value = string.Empty;
				command.Parameters.Add("_clientremark", PgsqlDbType.Varchar).Value = string.Empty;
				command.Parameters.Add("_internalremark", PgsqlDbType.Varchar).Value = string.Empty;
				command.Parameters.Add("_deliveryremark", PgsqlDbType.Varchar).Value = string.Empty;
				command.Parameters.Add("_taskstatusid", PgsqlDbType.Integer).Value = -2;
				command.Parameters.Add("_billable", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_deliverydate", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_validationstatusid", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_schedulestart", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_scheduleend", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_coursevalue", PgsqlDbType.Numeric, 1);
				command.Parameters.Add("_salesmanagerd", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_ordertypeid", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_deliveryperiodstart", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_deliveryperiodend", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_shipmentstart", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_shipmentend", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_searchshipmentdateequalnull", PgsqlDbType.Boolean, false);
				command.Parameters.Add("_dpistart", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_dpiend", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_invstart", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_invend", PgsqlDbType.Timestamp, DBNull.Value);
				command.Parameters.Add("_deliveryaddresses", PgsqlDbType.Text, string.Empty);
				command.Parameters.Add("_createdbyemployeeid", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_subscriptionid", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_approved", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_calculateavailable", PgsqlDbType.Boolean, true);
				command.Parameters.Add("_company", PgsqlDbType.Text, string.Empty);
				command.Parameters.Add("_trip", PgsqlDbType.Text, string.Empty);
				command.Parameters.Add("_limit", PgsqlDbType.Integer, 100);

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, Order[]>>(xmlResult);

				return dict.First().Value.ToList();
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetOrders();
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<Order>();
				}
			}
		}
		public async Task<Tuple<Address, Address>> GetOrderAddress(int orderId, int clientId)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("ordermanager_getclientaddressdata");
				command.Parameters.Add("_clientid", PgsqlDbType.Integer).Value = clientId;
				command.Parameters.Add("_orderid", PgsqlDbType.Integer).Value = orderId;

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, Address[]>>(xmlResult);

				return new Tuple<Address, Address>(new Address(), new Address());
				//return dict.First().Value.ToList();
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetOrderAddress(orderId, clientId);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new Tuple<Address, Address>(new Address(), new Address());
				}
			}
		}
		public async Task<List<OrderDetails>> GetOrderDetails(int orderId)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("ordermanager_getorderdetailsbyid");
				command.Parameters.Add("_orderid", PgsqlDbType.Integer).Value = orderId;
				command.Parameters.Add("_coursevalue", PgsqlDbType.Numeric).Value = 0;

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, OrderDetails[]>>(xmlResult);

				return dict.First().Value.ToList();
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetOrderDetails(orderId);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<OrderDetails>();
				}
			}
		}
		#endregion

		#region Invoice		
		public async Task<List<Invoice>> GetInvoices(string companyName, int[] paymentStatuses, int limit, int? clientId, int? orderId = null, int[] invoiceIds = null, int[] businessIds = null)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("invoicemanager_getfilteredinvoices3_iod");
				command.Parameters.Add("_invoiceid", PgsqlDbType.Integer | PgsqlDbType.Array).Value = invoiceIds ?? (object)DBNull.Value;
				command.Parameters.Add("_orderid", PgsqlDbType.Integer).Value = orderId ?? (object)DBNull.Value;
				command.Parameters.Add("_clientid", PgsqlDbType.Integer, clientId ?? (object)DBNull.Value);
				command.Parameters.Add("_businessid", PgsqlDbType.Integer | PgsqlDbType.Array, businessIds ?? (object)DBNull.Value);
				command.Parameters.Add("_clientname", PgsqlDbType.Varchar).Value = DBNull.Value;
				command.Parameters.Add("_companyname", PgsqlDbType.Varchar).Value = string.IsNullOrEmpty(companyName) ? (object)DBNull.Value : companyName;
				command.Parameters.Add("_datefrom", PgsqlDbType.Timestamp, DateTime.MinValue);
				command.Parameters.Add("_dateto", PgsqlDbType.Timestamp, DateTime.MaxValue);
				command.Parameters.Add("_invoicenumber", PgsqlDbType.Varchar).Value = DBNull.Value;
				command.Parameters.Add("_countryid", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_discountgroupid", PgsqlDbType.Integer, 0);
				command.Parameters.Add("_udffilter", PgsqlDbType.Text).Value = DBNull.Value;
				command.Parameters.Add("_payments", PgsqlDbType.Integer | PgsqlDbType.Array, paymentStatuses);
				command.Parameters.Add("_limit", PgsqlDbType.Integer, limit);

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, Invoice[]>>(xmlResult);

				return dict.First().Value.ToList();
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetInvoices(companyName, paymentStatuses, limit, clientId, orderId, invoiceIds, businessIds);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<Invoice>();
				}
			}
		}


		#endregion

		#region Inventory count
		public async Task<List<InventoryCount>> GetInventoryCount()
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("stockproductinvertorymanager_get");

				command.Parameters.Add("_productid", PgsqlDbType.Integer).Value = DBNull.Value;
				command.Parameters.Add("_businessid", PgsqlDbType.Integer).Value = 1;
				command.Parameters.Add("_createdbyemployeeid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_assignedtoemployeeid", PgsqlDbType.Integer).Value = _user.PersonId;
				command.Parameters.Add("_stockrackid", PgsqlDbType.Integer).Value = DBNull.Value;
				command.Parameters.Add("_stockid", PgsqlDbType.Integer).Value = 1;
				command.Parameters.Add("_batchnumber", PgsqlDbType.Text).Value = string.Empty;
				command.Parameters.Add("_categoryid", PgsqlDbType.Integer).Value = DBNull.Value;
				command.Parameters.Add("_brandid", PgsqlDbType.Integer).Value = DBNull.Value;
				command.Parameters.Add("_statusid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_isassigned", PgsqlDbType.Boolean).Value = true;
				command.Parameters.Add("_createddatestart", PgsqlDbType.Timestamp).Value = DBNull.Value;
				command.Parameters.Add("_createddateend", PgsqlDbType.Timestamp).Value = DBNull.Value;
				command.Parameters.Add("_counteddatestart", PgsqlDbType.Timestamp).Value = DBNull.Value;
				command.Parameters.Add("_counteddateend", PgsqlDbType.Timestamp).Value = DBNull.Value;
				command.Parameters.Add("_delta", PgsqlDbType.Numeric).Value = DBNull.Value;
				command.Parameters.Add("_productcountid", PgsqlDbType.Integer).Value = 0;

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, InventoryCount[]>>(xmlResult);

				return dict.First().Value.ToList();
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetInventoryCount();
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<InventoryCount>();
				}
			}
		}
		public async Task<int> SetCount(int businessId, int? stockId, int? rackId, int[] productIds, int[] batchIds, float[] quantities, int[] productCountIds)
		{
			try
			{

				SimpleWSA.Command command = new SimpleWSA.Command("stockproductinvertorymanager_setcount");
				command.Parameters.Add("_businessid", PgsqlDbType.Integer, businessId);
				command.Parameters.Add("_stockid", PgsqlDbType.Integer, stockId);
				command.Parameters.Add("_stockrackid", PgsqlDbType.Integer, rackId);
				command.Parameters.Add("_productids", PgsqlDbType.Integer | PgsqlDbType.Array, productIds);
				command.Parameters.Add("_batchids", PgsqlDbType.Integer | PgsqlDbType.Array, batchIds);
				command.Parameters.Add("_quantities", PgsqlDbType.Numeric | PgsqlDbType.Array, quantities);
				command.Parameters.Add("_productcountids", PgsqlDbType.Integer | PgsqlDbType.Array, productCountIds);
				command.Parameters.Add("_userid", PgsqlDbType.Integer, _user.PersonId);
				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.NonQuery,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dictionary = JsonConvert.DeserializeObject<Dictionary<string, ReturnScaler>>(xmlResult);
				if (dictionary == null && !dictionary.Any())
					return -100;

				return dictionary.First().Value.Value;
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await SetCount(businessId, stockId, rackId, productIds, batchIds, quantities, productCountIds);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return -100;
				}
			}
		}

		public async Task<List<Product>> GetProducts(int[] businessIds)
		{
			try
			{
				SimpleWSA.Command command = new SimpleWSA.Command("crm_project_getproducts");

				command.Parameters.Add("_productid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_productname", PgsqlDbType.Text).Value = string.Empty;
				command.Parameters.Add("_serial", PgsqlDbType.Text).Value = string.Empty;
				command.Parameters.Add("_pricefrom", PgsqlDbType.Numeric).Value = DBNull.Value;
				command.Parameters.Add("_priceto", PgsqlDbType.Numeric).Value = DBNull.Value;
				command.Parameters.Add("_ean", PgsqlDbType.Text).Value = string.Empty;
				command.Parameters.Add("_offer", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_imported", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_obsolete", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_brandid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_categoryid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_types", PgsqlDbType.Integer | PgsqlDbType.Array).Value = new int[] { 1 };
				command.Parameters.Add("_businessids", PgsqlDbType.Integer | PgsqlDbType.Array).Value = businessIds;
				command.Parameters.Add("_limit", PgsqlDbType.Integer).Value = 500;
				command.Parameters.Add("_currencycourse", PgsqlDbType.Numeric).Value = 1;
				command.Parameters.Add("_searchonlyparents", PgsqlDbType.Boolean).Value = false;
				command.Parameters.Add("_showparentlabel", PgsqlDbType.Boolean).Value = true;
				command.Parameters.Add("_getvariantsonly", PgsqlDbType.Boolean).Value = false;
				command.Parameters.Add("_onlywithproductgroup", PgsqlDbType.Boolean).Value = false;
				command.Parameters.Add("_supplierid", PgsqlDbType.Integer).Value = 0;
				command.Parameters.Add("_sourcebusinessid", PgsqlDbType.Integer).Value = 0;

				command.WriteSchema = WriteSchema.TRUE;
				string xmlResult = SimpleWSA.Command.Execute(command,
													RoutineType.DataSet,
													httpMethod: SimpleWSA.HttpMethod.GET,
													responseFormat: ResponseFormat.JSON);

				var dict = JsonConvert.DeserializeObject<Dictionary<string, Product[]>>(xmlResult);

				return dict.First().Value.ToList();
			}
			catch (Exception ex)
			{
				count++;
				if (count == 1)
				{
					await NewSession(_user.UserEmail, _user.UserPassword);
					return await GetProducts(businessIds);
				}
				else
				{
					count = 0;
					await App.Current.MainPage.DisplayAlert("Sorry", ex.Message, "Ok");
					return new List<Product>();
				}
			}
		}
		#endregion

	}

	public class ReturnScaler
	{
		public int ReturnValue { get; set; }

		[JsonProperty("arguments._returnvalue")]
		public int Value { get; set; }
	}
}
