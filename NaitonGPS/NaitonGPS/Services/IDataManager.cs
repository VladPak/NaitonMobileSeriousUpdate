using NaitonGPS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaitonGPS.Services
{
    public interface IDataManager
    {
        Task<bool> GetWebService(string webserviceLink);
        UserLoginDetails GetCurrentUser();
        Task<List<PickListItem>> GetPickListItems(int pickListId);
        Task<List<PickList>> GetPickLists(int? pickListId = null);
        Task<List<Rack>> GetPickRacks(int deliveryOrderDetailsId);
        Roles[] GetRoles(int roleId);
        UserLoginDetails RegistrationServiceSession();
        Task<int> SavePickListItems(List<PickListItem> items);
        void SetUserData(out int roleId);

        Task NewSession();
    }
}