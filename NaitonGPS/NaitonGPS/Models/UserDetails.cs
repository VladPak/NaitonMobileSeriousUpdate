namespace NaitonGPS.Models
{
    public class UserDetails
    {        
        public int EmployeeId { get; set; }
                
        public int RoleRightId { get; set; }
                
        public string EmployeeName { get; set; }
                
        public int CurrencyId { get; set; }
                
        public int DefaultStockId { get; set; }
                
        public int DefaultCountryId { get; set; }
                
        public string DatabaseName { get; set; }
                
        public bool AllowTaskNotification { get; set; }
                
        public bool IsSupervisor { get; set; }
                
        public string DashboardSettings { get; set; }
                
        public int Version { get; set; }
                
        public string CompanyGuid { get; set; }
                
        public bool IsNotUpdate { get; set; }
                
        public bool IsPasswordExpired { get; set; }
                
        public bool IsLoginAllowed { get; set; }
                
        public bool CanSave { get; set; }
    }
}
