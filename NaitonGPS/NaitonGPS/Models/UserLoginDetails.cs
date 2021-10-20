namespace NaitonGPS.Models
{
    public class UserLoginDetails
    {        
        public string UserEmail { get; set; }
     
        public string UserPassword { get; set; }
     
        public string UserToken { get; set; }
     
        public bool IsEncrypted { get; set; }
     
        public int AppId { get; set; }
     
        public string AppVersion { get; set; }
     
        public string Domain { get; set; }
             
        public string ConnectionProviderAddress { get; set; }
     
        public int PersonId { get; set; }
     
        public int RoleId { get; set; }

    }
}
