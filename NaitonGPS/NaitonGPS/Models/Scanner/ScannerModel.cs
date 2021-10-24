using ZebraBarcodeScannerSDK;

namespace NaitonGPS.Models
{
    /// <summary>
    /// Scanner Model
    /// </summary>
    public class ScannerModel
    {

      
        /// <summary>
        /// Name
        /// </summary>

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {             
                 _name = value;
                               
            }

         }

        /// <summary>
        /// Id
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// ModeType
        /// </summary>
        public string ModeType
        {
            get;
            set;
        }

        /// <summary>
        /// ScannerObject
        /// </summary>
        public Scanner ScannerObject
        {
            get;
            set;
        }

        /// <summary>
        /// ConnectionStatus
        /// </summary>
        public string ConnectionStatus
        {
            get;
            set;
        }
    }
}
