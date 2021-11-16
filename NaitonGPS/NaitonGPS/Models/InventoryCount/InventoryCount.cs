using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Models
{
	public class InventoryCount
	{
		public DateTime CreatedDate { get; set; }
		public DateTime? AssignedDate { get; set; }
		public int ProductCountId { get; set; }
		public int? ProductId { get; set; }
		public string ProductName { get; set; }
		public string ProductNameFormat { get; set; }
		public string Variants { get; set; }
		public float QuantityInStock { get; set; }
		public string CategoryName { get; set; }
		public string BrandName { get; set; }
		public string BarCode { get; set; }
		public string ManufacturerCode { get; set; }
		public string BusinessName { get; set; }
		public int BusinessId { get; set; }
		public string StockName { get; set; }
		public int? StockId { get; set; }
		public string StockRackName { get; set; }
		public int? StockRackId { get; set; }
		public int? BatchId { get; set; }
		public string BatchNumber { get; set; }
		public float CountedStock { get; set; }
		public float ShouldBeCount { get; set; }
		public float Delta { get; set; }
		public string EmployeeFullName { get; set; }
		public int? AssignedEmployeeId { get; set; }
		public int StatusId { get; set; }
		public string Status { get; set; }
	}
}
