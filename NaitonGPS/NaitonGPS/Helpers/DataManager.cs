using NaitonGPS.Models;
using Newtonsoft.Json;
using SimpleWSA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace NaitonGPS.Helpers
{
    public static class DataManager
    {
        private static UserLoginDetails _user;
        static int count = 0;
        #region Pick list

        public static List<PickListItem> GetPickListItems(int pickListId)
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
                if (count < 3)
                {
                    ContinueSession();
                    count = count + 1;
                    return GetPickListItems(pickListId);                    
                }
                else
                {
                    count = 0;
                    throw new Exception(ex.Message, ex);
                }

            }

        }

        public static List<PickList> GetPickLists(int? pickListId = null)
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
                if (count < 3)
                {
                    ContinueSession();
                    count = count + 1;
                    return GetPickLists(pickListId);
                }
                else
                {
                    count = 0;
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public static List<Rack> GetPickRacks(int deliveryOrderDetailsId)
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
                if (count < 3)
                {
                    ContinueSession();
                    count = count + 1;
                    return GetPickRacks(deliveryOrderDetailsId);
                }
                else
                {
                    count = 0;
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public static int SavePickListItems(List<PickListItem> items)
        {
            try
            {
                int result = 0;
                foreach (var item in items.GroupBy(x=>x.DeliveryOrderDetailsId))
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
                if (count < 3)
                {
                    ContinueSession();
                    count = count + 1;
                    return SavePickListItems(items);
                }
                else
                {
                    count = 0;
                    throw new Exception(ex.Message, ex);
                }
            }            
        }

        #endregion Pick list

        #region Account 

        public static UserLoginDetails RegistrationServiceSession()
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

        public static void SetUserData(out int roleId)
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

        public static UserLoginDetails GetCurrentUser()
        {
            return _user;
        }

        public static Roles[] GetRoles(int roleId)
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

        public static void ContinueSession()
        {
            SessionContext.RefreshStatic();
        }
    }

    public class ReturnScaler
    {
        public int ReturnValue { get; set; }
    }
}
