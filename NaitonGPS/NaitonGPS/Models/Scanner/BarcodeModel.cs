using System;
namespace NaitonGPS.Models
{
    /// <summary>
    /// BarcodeModel
    /// </summary>
    public class BarcodeModel
    {
        public byte[] BarcodeData { get; set; }
        public string DecodeData { get; set; }
        public string BarcodeType { get; set; }
        public int ScannerID { get; set; }

    }
}
