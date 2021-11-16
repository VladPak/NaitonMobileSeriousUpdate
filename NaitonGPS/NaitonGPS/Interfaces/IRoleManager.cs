using NaitonGPS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.Interfaces
{
	public interface IRoleManager
	{
		void Set(List<Roles> roles);
		IEnumerable<Roles> GetAll();
		bool Exists(string name);
		Roles Get(string name);
	}
}
