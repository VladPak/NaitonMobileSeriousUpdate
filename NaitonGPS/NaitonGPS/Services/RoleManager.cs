using NaitonGPS.Interfaces;
using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Services
{
	internal enum RoleType
	{
		NaitonUserFunctions = 1,
		NaitonAminFunctions = 2,
		Intranet = 3,
		WHR = 4,
		WebSite = 5,
		Mobile = 6
	}
	internal enum ObjectType
	{
		MenuItem = 1,
		Form = 2,
		Button = 3,
		DataGridViewColumn = 4,
		OtherControls = 10
	}

	public sealed class RoleManager : IRoleManager
	{
		public const string PICK_LIST = "WMS_Picklist";
		public const string INVENTORY_COUNT = "WMS_InventoryCount";
		public const string SHOW_CURRENT_QUANTITY = "WMS_InventoryCount_ShowCurrentQuantity";
		public const string ADD_RANDOM_RACK = "WMS_InventoryCount_AddRandomRack";

		private static List<Roles> _roles;
		public void Set(List<Roles> roles)
		{
			_roles = roles;
		}
		public bool Exists(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				return false;

			return _roles.Exists(item => item.Object.Trim() == name.Trim());
		}
		public Roles Get(string name)
		{
			if (Exists(name))
			{
				return _roles.Find(item => item.Object.Trim() == name.Trim());
			}

			return null;
		}
		public IEnumerable<Roles> GetAll()
		{
			return _roles;
		}
	}
}
