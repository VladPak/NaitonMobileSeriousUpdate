using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Models
{
	public class OrderDetails
	{
		public decimal TotalPrice
		{
			get
			{
				switch (Type)
				{
					case OrderDetailsType.Product: return (1 - Discount) * SalesPrice * Quantity * (1 + Tax);

					case OrderDetailsType.Delivery:
					case OrderDetailsType.Payment: return SalesPrice * Quantity * (1 + Tax);

					default: return 0;
				}
			}
		}
		public int ID { get; private set; }
		public int OrderId { get; private set; }
		public int ProductId { get; private set; }
		public decimal Quantity { get; private set; }
		public Nullable<int> ParentId { get; private set; }
		public decimal SalesPrice { get; private set; }
		public decimal Discount { get; private set; }
		public decimal Tax { get; private set; }
		public OrderDetailsType Type { get; private set; }
		public decimal DiscountGroup { get; private set; }
		public decimal DiscountQuantity { get; private set; }
		public decimal DiscountPeriodical { get; private set; }
		public decimal DiscountCode { get; private set; }
		public decimal DiscountX4y { get; private set; }
		public decimal DiscountGiveaway { get; private set; }
		public decimal DiscountAmount { get; private set; }
		public decimal DiscountCheckOut { get; private set; }
		public Nullable<int> DeliveryId { get; private set; }
		public OrderStatus OrderStatus { get; set; }
		public Nullable<int> InvoiceId { get; private set; }
		public Nullable<DiscountType> DiscountType { get; private set; }
		public Nullable<int> DeliveryAddressId { get; private set; }
		public string Decor { get; set; }
		public int Rank { get; set; }
		public string ProductName { get; set; }
		public int OrderDetailsId { get; set; }
		public string MeasurementStatus { get; set; }
		public decimal Length { get; set; }
		public string Photo { get; set; }
		public decimal SalesPriceWeb { get; set; }
		public string ComponentNumber { get; set; }
		public string StairsId { get; set; }
	}
}
