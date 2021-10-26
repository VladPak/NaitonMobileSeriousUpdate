using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGPS.ViewModels
{
    public class DeliveryRemarkViewModel : BaseViewModel
    {
        public string Remark { get; set; }
        public DeliveryRemarkViewModel(string deliveryRemark)
        {
            Remark = deliveryRemark;
        }


    }
}
