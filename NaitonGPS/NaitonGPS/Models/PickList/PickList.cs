using System.Collections.Generic;
using System.Linq;

namespace NaitonGPS.Models
{
    public class PickList
    {        
        public int PickListId { get; set; }             

        public string PickerName { get; set; }        

        public decimal Products { get; set; }
                
        public decimal Weight { get; set; }
                
        public int[] StatusIds { get; set; }

        public string ColorStatus { get; set; }
        public string Client { get; set; }
        public string[] DeliveryRemark { get; set; }
        public int[] OrderIds { get; set; }

        public string Remark
        {
            get
            {
                return DeliveryRemark!=null?string.Join(" ", DeliveryRemark):"No delivery remark";
            }
        }

        public string Order
        {
            get
            {
                return OrderIds!=null?string.Join(",", OrderIds):"No order id";
            }
        }
        
        public string GetColor()
        {
            if (StatusIds != null && StatusIds.Count() > 0 && listColors.ContainsKey(StatusIds[0]))
                return listColors[StatusIds[0]];
            return listColors[-1];
        }

        readonly Dictionary<int, string> listColors = new Dictionary<int, string>
        {
            {-1,"Gray" },
            { 0,"White"},
            { 2,"Orange"},
            { 9,"#66a103"}
        };
    }
}
