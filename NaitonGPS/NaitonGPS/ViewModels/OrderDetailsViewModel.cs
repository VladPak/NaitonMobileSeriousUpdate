using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NaitonGPS.ViewModels
{
	public class OrderDetailsViewModel : BaseViewModel
	{
		private readonly Order _order = null;
		private List<Invoice> _invoices = null;
		private Tuple<Address, Address> _address = null;
		private List<OrderDetails> _orderDetails = null;
		public OrderDetailsViewModel(Order order)
		{
			_order = order;
			Title = "Order details";
			LoadCommand = new Command(async () =>
			{
				_invoices = await DataManager.GetInvoices(string.Empty, new int[] { 1, 1, 1, 1 }, 200, null, _order.OrderId);
				_address = await DataManager.GetOrderAddress(_order.OrderId, 0);
				_orderDetails = await DataManager.GetOrderDetails(_order.OrderId);
				this.Initialize();
			});
		}

		public Command LoadCommand { get; }

		private void Initialize()
		{

		}
	}
}
