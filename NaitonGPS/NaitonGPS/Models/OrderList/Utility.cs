using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Models
{
	public static class Utility
	{
		
	}
	public enum OrderDetailsType : int
	{
		Product = 1,
		Delivery = 2,
		Payment = 3,
		Giveaway = 4
	}

	public enum OrderStatus : int
	{
		Offer = 0,
		Order = 1,
		Cancelled = 2,
		Pending = 3,
		Sent = 4,
		Returned = 5,
		Damaged = 6,
		NotReturned = 7,
		Request = 8
	}
	public enum DiscountType : int
	{
		Group = 1,
		Quantity = 2,
		Periodical = 3,
		Code = 4,
		X4Y = 5,
		GiveAways = 6,
		Amount = 7,
		CheckOut = 8
	}
}
