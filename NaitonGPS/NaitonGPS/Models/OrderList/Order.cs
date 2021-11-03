using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Models
{
	public class Order
	{
		public int OrderId { get; set; }
		public string BusinessName { get; set; }
		public string CompanyName { get; set; }
		public string ClientName { get; set; }
		public string OrderUser { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal OrderSum { get; set; }
		public decimal PaidSum { get; set; }
		public bool FinanciallyApproved { get; set; }
		public string Availability { get; set; }
		public string OrderStatus { get; set; }
		public string PaymentStatus { get; set; }

		// Invoicing
		public decimal? DownPaymentPercentage { get; set; }
		public DateTime? DownPaymentInvoiceDate { get; set; }
		public DateTime? InvoiceDate { get; set; }

		// Comments
		public string ClientRemark { get; set; }
		public string SalesRemark { get; set; }
		public string ShipmentRemark { get; set; }
		public string ClientReferenceNumber { get; set; }

		// Client info
		public int ClientId { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public int[] FileId { get; set; }
		public int[] RelationFileIds { get; set; }

		public int BusinessId { get; set; }
		public int? CreatedByEmployeeId { get; set; }
		public int? OrderTypeId { get; set; }
		public DateTime? ShipmentDate { get; set; }
		public int? SalesManagerId { get; set; }
		//Company info
		public int? CompanyId { get; set; }
		public string CompanyPhone { get; set; }
		public string CompanyEmail { get; set; }
		public string CompanyFax { get; set; }
		public int? StockId { get; set; }
		public float? FreeDeliveryAmount { get; set; }
		public string PaymentMethod { get; set; }
		public decimal PaymentSumWithoutTax { get; set; }
		public decimal DeliverySumWithoutTax { get; set; }
		public decimal OrderSumWithoutTax { get; set; }
		public decimal VAT
		{
			get
			{
				return OrderSum - OrderSumWithoutTax;
			}
		}
		public bool HasExclamation { get; set; }
	}
}
