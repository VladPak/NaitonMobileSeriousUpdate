using ZebraBarcodeScannerSDK;

namespace NaitonGPS.API
{
    /// <summary>
    /// Globals
    /// </summary>
    public class Globals
    {
        /// <summary>
        /// Initializes a new instance of Globals
        /// </summary>
        public Globals()
        {
        }

        /// <summary>
        /// Current status of the connect page
        /// </summary>
        private static bool _isConnectPageLoaded = false;
        public static bool IsConnectPageLoaded
        {
            get
            {
                return _isConnectPageLoaded;
            }
            set
            {
                _isConnectPageLoaded = value;
            }
        }

        /// <summary>
        /// Current status of the scant to connect page
        /// </summary>
        /// <returns>Page status</returns>
        private static bool _isScanToConnectPageLoaded = false;
        public static bool isScanToConnectPageLoaded
        {
            get
            {
                return _isScanToConnectPageLoaded;
            }
            set
            {
                _isScanToConnectPageLoaded = value;
            }
        }
        /// <summary>
        /// Current status of the main page
        /// </summary>
        /// <returns>Page status</returns>
        private static bool _isMainPageLoaded = false;
        public static bool isMainPageLoaded
        {
            get
            {
                return _isMainPageLoaded;
            }
            set
            {
                _isMainPageLoaded = value;
            }
        }
        /// <summary>
        /// Current status of the app settings page
        /// </summary>
        /// <returns>Page status</returns>
        private static bool _isAppSettingsPageLoaded = false;
        public static bool isAppSettingsPageLoaded
        {
            get
            {
                return _isAppSettingsPageLoaded;
            }
            set
            {
                _isScanToConnectPageLoaded = value;
            }
        }
        /// <summary>
        /// Current status of the barcode detailed page
        /// </summary>
        private static bool _isBarcodeDetailPageLoaded = false;
        public static bool IsBarcodeDetailPageLoaded
        {
            get
            {
                return _isBarcodeDetailPageLoaded;
            }
            set
            {
                _isBarcodeDetailPageLoaded = value;
            }
        }

        /// <summary>
        /// The connected scanner ID.
        /// </summary>
        private static string _connectedId = ConstantsString.DefaultScannerID;
        public static string ConnectedId
        {
            get
            {
                return _connectedId;
            }
            set
            {
                _connectedId = value;
            }
        }



      


        /// <summary>
        /// The Connected Row 
        /// </summary>
        private static int _connectedRow = ConstantsString.ConnectedRow;
        public static int ConnectedRow
        {
            get
            {
                return _connectedRow;
            }
            set
            {
                _connectedRow = value;
            }
        }


        /// <summary>
        /// The Disconnected Scanner Name 
        /// </summary>
        private static string _disconnectedScanerName = ConstantsString.ImgEmpty;
        public static string DisconnectedScanerName
        {
            get
            {
                return _disconnectedScanerName;
            }
            set
            {
                _disconnectedScanerName = value;
            }
        }


        /// <summary>
        /// The Connected Scanner Object  
        /// </summary>
        private static Scanner _connectedScanner = null;
        public static Scanner ConnectedScanner
        {
            get
            {
                return _connectedScanner;
            }
            set
            {
                _connectedScanner = value;
            }
        }
    }
}
