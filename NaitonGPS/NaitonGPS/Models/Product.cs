using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Models
{
	public class Product
	{
		public int[] SupplierIds { get; set; }
		public float? SupplierBuyPrice { get; set; }
		public float SupplierOrderQuantity { get; set; }
		public string FormatName { get; set; }
		public int ProductId { get; set; }
		public int? ParentId { get; set; }
		public int TaxCategoryId { get; set; }
		public bool? IsExpire { get; set; }
		public float Width { get; set; }
		public float Thickness { get; set; }
		public float Length { get; set; }
		public float? Weight { get; set; }
		public string ProductNameWoVariantsWoLabel { get; set; }
		public string ProductNameWoVariants { get; set; }
		public string Variants { get; set; }
		public string ProductNameWeLabel { get; set; }
		public string ProductName { get; set; }
		public bool HasIncurrentBusiness { get; set; }
		public string ManufacturerCode { get; set; }
		public float SalesPrice { get; set; }
		public float BuyPrice { get; set; }
		public string BarCode { get; set; }
		public bool Obsolete { get; set; }
		public bool SpecialOffer { get; set; }
		public int? MeasurementId { get; set; }
		public int ProductType { get; set; }
		public int BusinessId { get; set; }
		public int CategoryId { get; set; }
		public string Measurement { get; set; }
		public string MeasurementShortName { get; set; }
		public string BrandName { get; set; }
		public string CategoryName { get; set; }
		public float Vat { get; set; }
		public int[] BusinessIds { get; set; }
		public string BusinessName { get; set; }
		public int ProductGroup { get; set; }
		public float Factor { get; set; }
		public int[] ProductGroups { get; set; }
		public int ProductClaimId { get; set; }
		public float Discount { get; set; }
		public string ProductClaim { get; set; }
	}
}
