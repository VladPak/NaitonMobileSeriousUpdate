using NaitonGPS.Models;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NaitonGPS.Services
{
    public class DataManager : IDataManager
    {
        private static UserLoginDetails _user;
        static int count = 0;

        public async Task<bool> GetWebService(string webserviceLink)
        {
            string webservice = String.Format("https://connectionprovider.naiton.com/DataAccess/{0}/restservice/address", webserviceLink);

            try
            {
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(webservice);
                var responseContent = await response.Content.ReadAsStringAsync();
                var rsToString = responseContent.ToString();

                Preferences.Set("webservicelink", rsToString);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
        }

        #region Pick list

        public async Task<List<PickListItem>> GetPickListItems(int pickListId)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklistracks");
                command.Parameters.Add("_picklistids", PgsqlDbType.Integer | PgsqlDbType.Array, new int[] { pickListId });
                command.Parameters.Add("_limit", PgsqlDbType.Integer, 100);

                command.WriteSchema = WriteSchema.TRUE;
                string xmlResult = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);

                var dict = JsonConvert.DeserializeObject<Dictionary<string, PickListItem[]>>(xmlResult);

                var pickListItems = dict.First().Value.ToList();
                return pickListItems;
            }
            catch (Exception ex)
            {
                return new List<PickListItem>();
            }

        }

        public async Task<List<PickList>> GetPickLists(int? pickListId = null)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpicklists");
                command.Parameters.Add("_picklistid", PgsqlDbType.Integer, pickListId != null ? pickListId : (object)DBNull.Value);
                //command.Parameters.Add("_statusid", PgsqlDbType.Integer, 3);
                command.Parameters.Add("_pickerid", PgsqlDbType.Integer, _user.PersonId);
                command.WriteSchema = WriteSchema.TRUE;
                string xmlResult = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);
                var dict = JsonConvert.DeserializeObject<Dictionary<string, PickList[]>>(xmlResult);

                var pickList = dict.First().Value.ToList();

                return pickList;
            }
            catch (Exception ex)
            {
                
                count++;
                if (count == 1)
                {
                    await NewSession(_user.UserEmail, _user.UserPassword);
                    return await GetPickLists(pickListId);                    
                }
                else
                    return new List<PickList>();
            }
        }

        public async Task<List<Rack>> GetPickRacks(int deliveryOrderDetailsId)
        {
            try
            {
                SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_getpickracksformobile");
                command.Parameters.Add("_deliveryorderdetailsid", PgsqlDbType.Integer, deliveryOrderDetailsId);

                command.WriteSchema = WriteSchema.TRUE;
                string xmlResult = SimpleWSA.Command.Execute(command,
                                                    RoutineType.DataSet,
                                                    httpMethod: SimpleWSA.HttpMethod.GET,
                                                    responseFormat: ResponseFormat.JSON);

                var dict = JsonConvert.DeserializeObject<Dictionary<string, Rack[]>>(xmlResult);

                var rackList = dict.First().Value.ToList();

                return rackList.ToList();
            }
            catch (Exception ex)
            {                
                return new List<Rack>();                
            }
        }

        public async Task<int> SavePickListItems(List<PickListItem> items)
        {
            try
            {
                int result = 0;
                foreach (var item in items.GroupBy(x => x.DeliveryOrderDetailsId))
                {
                    SimpleWSA.Command command = new SimpleWSA.Command("picklistmanager_addupdateracks");
                    command.Parameters.Add("_deliveryorderdetailsid", PgsqlDbType.Integer, item.Key);
                    command.Parameters.Add("_picklistorderdetailsids", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.PickListOrderDetailsId).ToArray());
                    command.Parameters.Add("_stockrackids", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.StockRackId ?? 0).ToArray());
                    command.Parameters.Add("_statusids", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.StatusId).ToArray());
                    command.Parameters.Add("_quantities", PgsqlDbType.Integer | PgsqlDbType.Array, items.Where(i => i.DeliveryOrderDetailsId == item.Key).Select(x => x.Quantity).ToArray());

                    command.WriteSchema = WriteSchema.TRUE;
                    string xmlResult = SimpleWSA.Command.Execute(command,
                                                        RoutineType.Scalar,
                                                        httpMethod: SimpleWSA.HttpMethod.GET,
                                                        responseFormat: ResponseFormat.JSON);

                    var dict = JsonConvert.DeserializeObject<Dictionary<string, ReturnScaler>>(xmlResult);

                }


                return result;
            }
            catch (Exception ex)
            {               
                return 0;                
            }
        }

        #endregion Pick list


        #region Account 

        public UserLoginDetails RegistrationServiceSession()
        {
            try
            {                
                                
                Preferences.Set("token", SessionContext.Token);

                UserLoginDetails userLoginDetails = new UserLoginDetails
                {
                    UserEmail = SessionContext.Login,
                    UserPassword = SessionContext.Password,
                    UserToken = SessionContext.Token,
                    AppId = SessionContext.AppId,
                    AppVersion = SessionContext.AppVersion,
                    IsEncrypted = SessionContext.IsEncrypted,
                    ConnectionProviderAddress = "https://connectionprovider.naiton.com/",
                    Domain = SessionContext.Domain
                };
                _user = userLoginDetails;

                return userLoginDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void SetUserData(out int roleId)
        {
            roleId = 0;
            UserLoginDetails dataFinalizeUserEP = JsonConvert.DeserializeObject<UserLoginDetails>((string)App.Current.Properties["UserDetail"]);
            var userid = dataFinalizeUserEP.PersonId;
            SimpleWSA.Command commandGetAllUserData = new SimpleWSA.Command("userlogin_checklogin5");
            commandGetAllUserData.Parameters.Add("_login", PgsqlDbType.Varchar).Value = dataFinalizeUserEP.UserEmail;
            commandGetAllUserData.Parameters.Add("_password", PgsqlDbType.Varchar).Value = dataFinalizeUserEP.UserPassword;
            commandGetAllUserData.WriteSchema = WriteSchema.TRUE;

            string xmlResult1 = SimpleWSA.Command.Execute(commandGetAllUserData,
                                                        RoutineType.DataSet,
                                                        httpMethod: SimpleWSA.HttpMethod.GET,
                                                        responseFormat: ResponseFormat.JSON);

            var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, UserDetails[]>>(xmlResult1);
            var newUser = dataFinalize.First().Value.First();

            if (_user is null)
                RegistrationServiceSession();

            _user.RoleId = newUser.RoleRightId;
            _user.PersonId = newUser.EmployeeId;

            roleId = dataFinalize["userlogin_checklogin5"].Select(x => x.RoleRightId).First();
        }

        public UserLoginDetails GetCurrentUser()
        {
            return _user;
        }

        public Roles[] GetRoles(int roleId)
        {
            SimpleWSA.Command command = new SimpleWSA.Command("rolemanager_getcheckroleobjects");
            command.Parameters.Add("_roleid", PgsqlDbType.Integer).Value = roleId;
            command.WriteSchema = WriteSchema.TRUE;
            string xmlResult = SimpleWSA.Command.Execute(command,
                                                RoutineType.DataSet,
                                                httpMethod: SimpleWSA.HttpMethod.GET,
                                                responseFormat: ResponseFormat.JSON);

            var dataFinalize = JsonConvert.DeserializeObject<Dictionary<string, Roles[]>>(xmlResult);
            var allRoles = dataFinalize.Values.ToList();
            var mobile = allRoles.SelectMany(i => i).Where(x => x.ObjectTypeId == 2 && x.TypeId == 6).ToArray();
            return mobile;
        }

        #endregion Account


        public async Task NewSession(string email, string password)
        {
            string currentAppVersion = VersionTracking.CurrentVersion;
            Session session = new Session(email,
                                            password,
                                            false,
                                            6,
                                            currentAppVersion,
                                            Preferences.Get("loginCompany", string.Empty),
                                            null);
            await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");
        }

        private async Task RefreshSession()
        {
            UserLoginDetails userData = JsonConvert.DeserializeObject<UserLoginDetails>((string)App.Current.Properties["UserDetail"]);

            Session session = new Session(userData.UserEmail,
                                            userData.UserPassword,
                                            userData.IsEncrypted,
                                            userData.AppId,
                                            userData.AppVersion,
                                            userData.Domain,
                                            null);

            await session.CreateByConnectionProviderAddressAsync("https://connectionprovider.naiton.com/");
        }
    }

    public class ReturnScaler
    {
        public int ReturnValue { get; set; }
    }
}
