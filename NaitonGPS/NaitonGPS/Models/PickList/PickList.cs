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

        public string ColorStatus
        {
            get
            {
                return listColors.ContainsKey(StatusIds?.FirstOrDefault() ?? -1) ? listColors[StatusIds?.FirstOrDefault() ?? -1] : listColors[-1];
            }
        }

        readonly Dictionary<int, string> listColors = new Dictionary<int, string>
        {
            {-1,"Gray" },
            { 0,"White"},
            { 2,"Orange"}
        };
    }
}
