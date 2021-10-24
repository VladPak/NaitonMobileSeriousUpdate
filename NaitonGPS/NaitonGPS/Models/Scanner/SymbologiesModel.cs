using System;
namespace NaitonGPS.Models
{
    /// <summary>
    /// SymbologiesModel
    /// </summary>
    public class SymbologiesModel
    {

        public string SymbologyName { get; set; }
        public int RMDAttribute { get; set; }
        public bool Enabled { get; set; }
        public bool IsSupported { get; set; }

    }
}
