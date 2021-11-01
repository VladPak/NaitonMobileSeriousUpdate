using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Models
{
	public class Address
	{
		public int ID { get; set; }
		public int CountryId { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public string HouseNumberAdd { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
		public string Recipient { get; set; }
		public string AddressCode { get; set; }
		public string AddressName { get; set; }
		public string RecipientCode { get; set; }
		public string RecipientCompany { get; set; }
	}
}
