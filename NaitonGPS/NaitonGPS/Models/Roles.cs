namespace NaitonGPS.Models
{
	public class Roles
	{
		public bool IsChecked { get; set; }
		public float Users { get; set; }
		public int ObjectTypeId { get; set; }
		public string Object { get; set; }
		public int TypeId { get; set; }
		public bool Disabled { get; set; }
		public bool? ParameterEnabled { get; set; }
		public bool? ParameterValue { get; set; }
	}
}
