using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaitonGPS.Services
{
	public interface IDataManager
	{
		Task<bool> GetWebService(string webserviceLink);
		UserLoginDetails GetCurrentUser();
		Task<List<PickListItem>> GetPickListItems(int pickListId);
		Task<List<PickList>> GetPickLists(int? pickListId = null);
		Task<List<Rack>> GetPickRacks(int deliveryOrderDetailsId);
		Roles[] GetRoles(int roleId);
		UserLoginDetails RegistrationServiceSession();
		Task<string> SavePickListItems(List<PickListItem> items);
		void SetUserData(out int roleId);

		Task NewSession(string email, string password);
		Task<List<Order>> GetOrders(int[] orderIds = null);
		Task<Tuple<Address, Address>> GetOrderAddress(int orderId, int clientId);
		Task<List<OrderDetails>> GetOrderDetails(int orderId);
		Task<List<Invoice>> GetInvoices(string companyName, int[] paymentStatuses, int limit, int? clientId, int? orderId = null, int[] invoiceIds = null, int[] businessIds = null);
	}
}