using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using ZebraBarcodeScannerSDK;
using NaitonGPS.Models;

namespace NaitonGPS.API
{
    public class SdkHandler
    {

        static ScannerSDK sdkInstance = null;
        static Scanners scanners = null;

        public delegate void BarcodeDataEventHandler(BarcodeData barcodeData, int scannerID);
        public static event BarcodeDataEventHandler BarcodeDataEvent;
        public delegate void ScannerAppearEventHandler(Scanner scanner);
        public static event ScannerAppearEventHandler ScannerAppearEvent;
        public static event EventHandler DeviceDisappeared;
        public static event EventHandler DeviceConnected;
        public static event EventHandler DeviceDisconnected;

     
        public static List<SymbologiesModel> symblogiesList = new List<SymbologiesModel>();
        public static List<BarcodeModel> barcodeList = new List<BarcodeModel>();
        public static List<ScannerModel> scannerList = new List<ScannerModel>();
        public static List<Scanner> discoveredScannerList = new List<Scanner>();
        public static List<Scanner> DiscoverdScannerList
        {
            set { discoveredScannerList = value; }
            get { return discoveredScannerList; }
        }

        public SdkHandler()
        {
        }

        /// <summary>
        /// GetInstance
        /// </summary>
        /// <returns>SDK instance</returns>
        public static ScannerSDK GetInstance()
        {
            if (sdkInstance == null)
            {
                sdkInstance = new ScannerSDK();
                scanners = sdkInstance.ScannerManager;
                scanners.EnableAvailableScannersDetection(true);

                string modeType = Application.Current.Properties[ConstantsString.ModeTypeStoringKey].ToString();
                switch (modeType)
                {
                    case ConstantsString.BTLE:
                        scanners.SetOperationMode(OpMode.OPMODE_BTLE);
                        break;
                    case ConstantsString.MFI:
                        scanners.SetOperationMode(OpMode.OPMODE_MFI);
                        break;
                    case ConstantsString.MFI_BTLE:
                        scanners.SetOperationMode(OpMode.OPMODE_MFI_BTLE);
                        break;
                    case ConstantsString.SSI:
                        scanners.SetOperationMode(OpMode.OPMODE_SSI);
                        break;
                    default:
                        scanners.SetOperationMode(OpMode.OPMODE_SNAPI);
                        break;
                }

                scanners.SubscribeForEvents((int)(Notifications.EVENT_SCANNER_APPEARANCE | Notifications.EVENT_SCANNER_DISAPPEARANCE | Notifications.EVENT_SESSION_ESTABLISHMENT | Notifications.EVENT_SESSION_TERMINATION | Notifications.EVENT_BARCODE ));
           
                scanners.Appeared += ScannerAppeared;
                scanners.Disappeared += ScannerDisappeared;
                scanners.Connected += ScannerConnected;
                scanners.Disconnected += ScannerDisconnected;
                scanners.BarcodeData += ScannerBarcodeData;
             
                DiscoverdScannerList = scanners.GetAvailableScanners();

            }

            return sdkInstance;
        }



      

        /// <summary>
        /// Set Operation Mode On App Setting
        /// </summary>
        /// <param name="opMode">Operation Mode</param>
        public static void SetOperationModeOnAppSetting(OpMode opMode)
        {
            scanners.SetOperationMode(opMode);
        }

        /// <summary>
        /// Get SDK version
        /// </summary>
        /// <returns></returns>
        public static string GetSdkVersion()
        {
            return GetInstance().Version;

        }

        /// <summary>
        /// Enable Bluetooth scanner discovery process
        /// </summary>
        public static void EnableBluetoothScannerDiscovery()
        {
            GetInstance().ScannerManager.EnableBluetoothScannerDiscovery();

        }

        /// <summary>
        /// Disable Bluetooth scanner discovery process
        /// </summary>
        public static void DisableBluetoothScannerDiscovery()
        {
            GetInstance().ScannerManager.DisableBluetoothScannerDiscovery();

        }

        /// <summary>
        /// Enable Bluetooth scanner discovery process
        /// </summary>
        /// <param name="enabledState">STC enabled state</param>
        public static void SetSTCEnabledState(bool enabledState)
        {
            GetInstance().ScannerManager.SetSTCEnabledState(enabledState);

        }


        /// <summary>
        /// Get Scaner List 
        /// </summary>
        /// <returns>ScannerModel List</returns>
        public static List<ScannerModel> GetScanerList()
        {
            for (int index = 0; index < discoveredScannerList.Count; index++)
            {
                AddNewScannerToScannerList(discoveredScannerList[index]);
            }

            List<ScannerModel> filteredScannerList = new List<ScannerModel>();

            string modeType = Application.Current.Properties[ConstantsString.ModeTypeStoringKey].ToString();           
            switch (modeType)
            {
                case ConstantsString.BTLE:
                    filteredScannerList = scannerList.Where(scannerObject => scannerObject.ModeType == ConstantsString.BTLE).ToList();
                    break;
                case ConstantsString.MFI:
                    filteredScannerList = scannerList.Where(scannerObject => scannerObject.ModeType == ConstantsString.MFI).ToList();
                    break;
                case ConstantsString.SSI:
                    filteredScannerList = scannerList.Where(scannerObject => scannerObject.ModeType == ConstantsString.SSI).ToList();
                    break;
                case ConstantsString.SNAPI:
                    filteredScannerList = scannerList.Where(scannerObject => scannerObject.ModeType == ConstantsString.SNAPI).ToList();
                    break;
                default:
                    filteredScannerList = scannerList;
                    break;
            }

            return filteredScannerList;
        }

        /// <summary>
        /// Add a new scanner to sannerList
        /// </summary>
        /// <param name="scanner">Scanner</param>
        private static void AddNewScannerToScannerList(Scanner scanner)
        {
            try
            {

                string conectionType;
                ScannerModel availableScanner = scannerList.Find((obj) => obj.Id == scanner.Id.ToString());

                if (availableScanner != null)
                {
                    return;
                }

                ConnectionType caseSwitch = scanner.ConnectionType;

                switch (caseSwitch)
                {
                    case ConnectionType.CONNECTION_TYPE_MFI:
                        conectionType = ConstantsString.MFI;
                        break;
                    case ConnectionType.CONNECTION_TYPE_BTLE:
                        conectionType = ConstantsString.BTLE;
                        break;
                    case ConnectionType.CONNECTION_TYPE_SNAPI:
                        conectionType = ConstantsString.SNAPI;
                        break;
                    case ConnectionType.CONNECTION_TYPE_SSI:
                        conectionType = ConstantsString.SSI;
                        break;
                    case ConnectionType.CONNECTION_TYPE_MFI_BTLE:
                        conectionType = ConstantsString.MFI_BTLE;
                        break;
                    default:
                        conectionType = ConstantsString.Invalid;
                        break;
                }

                scannerList.Add(new ScannerModel {Id = scanner.Id.ToString(), Name = scanner.Name, ModeType = conectionType,ScannerObject = scanner ,ConnectionStatus = ConstantsString.Empty});
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }

        }


        /// <summary>
        /// To get scanner mode type
        /// </summary>
        /// <param name="scanner"></param>
        /// <returns>Scanner mode type</returns>
        public static string GetScannerCommunicationProtocol(Scanner scanner)
        {
            string communicationProtocol;
            if (scanner != null)
            {

                ConnectionType caseSwitch = scanner.ConnectionType;

                switch (caseSwitch)
                {
                    case ConnectionType.CONNECTION_TYPE_MFI:
                        communicationProtocol = ConstantsString.MFI;
                        break;
                    case ConnectionType.CONNECTION_TYPE_BTLE:
                        communicationProtocol = ConstantsString.BTLE;
                        break;
                    case ConnectionType.CONNECTION_TYPE_SNAPI:
                        communicationProtocol = ConstantsString.SNAPI;
                        break;
                    case ConnectionType.CONNECTION_TYPE_MFI_BTLE:
                        communicationProtocol = ConstantsString.MFI_BTLE;
                        break;
                    case ConnectionType.CONNECTION_TYPE_SSI:
                        communicationProtocol = ConstantsString.SSI;
                        break;
                    default:
                        communicationProtocol = ConstantsString.Invalid;
                        break;
                }

                return communicationProtocol;

            }
            else
            {
                return ConstantsString.EmptyModeType;
            }

        }

        /// <summary>
        /// RemoveDeviceFromList
        /// </summary>
        /// <param name="scannerID">Scanner Id</param>
        private static void RemoveDeviceFromList(int scannerID)
        {
            try
            {
                ScannerModel availableScanner = scannerList.Find((obj) => obj.Id == scannerID.ToString());
                if (availableScanner != null)
                {
                    scannerList.Remove(availableScanner);

                }

                Scanner scanner = discoveredScannerList.Find((obj) => obj.Id == scannerID);
                if (scanner != null)
                {
                    discoveredScannerList.Remove(scanner);

                }



                UpdateScannerConnectionStatus(scannerID,"");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception " + e.Message);
            }
        }

        /// <summary>
        /// Get barcode list
        /// </summary>
        /// <returns>BarcodeModel List</returns>
        public static List<BarcodeModel> GetBarcodeList()
        {
            return barcodeList;
        }

        /// <summary>
        /// Clear the barcode list
        /// </summary>
        public static void ClearBarcodeList()
        {
            if (barcodeList != null)
            {
                barcodeList.Clear();
            }
            
        }

        /// <summary>
        /// Device connected event
        /// </summary>
        /// <param name="scannerObject">Scanner object</param>
        /// <param name="eventArg">Event Argument</param>
        public static void OnDeviceConnected(object scannerObject, EventArgs eventArg)
        {
            EventHandler handler = DeviceConnected;
            handler?.Invoke(scannerObject, eventArg);
        }

        /// <summary>
        /// Scanner disconnected event
        /// </summary>
        /// <param name="scannerObject">Scanner object</param>
        /// <param name="eventArg">Event Argument</param>
        public static void OnDeviceDisconnected(object scannerObject, EventArgs eventArg)
        {
            EventHandler handler = DeviceDisconnected;
            handler?.Invoke(scannerObject, eventArg);
        }


       
       


        /// <summary>
        /// Scanner disappeared event
        /// </summary>
        /// <param name="scannerObject">Scanner object</param>
        /// <param name="eventArg">EventArgs</param>
        public static void OnDeviceDisappeared(object scannerObject, EventArgs eventArg)
        {
            EventHandler handler = DeviceDisappeared;
            handler?.Invoke(scannerObject, eventArg);
        }

        /// <summary>
        /// Update scanner connection status in scannerList
        /// </summary>
        /// <param name="scannerID">Scaner Id</param>
        /// <param name="status">Status</param>
        public static void UpdateScannerConnectionStatus(int scannerID, string status)
        {
            var indexOf = scannerList.IndexOf(scannerList.Find(scanner => scanner.Id == scannerID.ToString()));
            scannerList[indexOf].ConnectionStatus = status;
        }


        /// <summary>
        /// Event handler of  Barcode Data
        /// </summary>
        /// <param name="barcodeData">Barcode Data</param>
        /// <param name="scannerID">Scanner Id</param>
        private static void ScannerBarcodeData(BarcodeData barcodeData, int scannerID)
        {
            System.Diagnostics.Debug.WriteLine("Barcode data" + barcodeData.Barcode);
            var handler = BarcodeDataEvent;
            if (handler != null)
            {
                handler.Invoke(barcodeData, scannerID);
            }

            barcodeList.Add(new BarcodeModel { BarcodeData = barcodeData.RawData, DecodeData = barcodeData.Barcode, BarcodeType = barcodeData.Type, ScannerID = scannerID });            
        }

        /// <summary>
        /// Event handler of Scanner Disconnected
        /// </summary>
        /// <param name="scannerID">Scanner Id</param>
        private static void ScannerDisconnected(int scannerID)
        {
            System.Diagnostics.Debug.WriteLine("Scanner Disconnected" + scannerID.ToString());
            UpdateScannerConnectionStatus(scannerID, "");
            OnDeviceDisconnected(scannerID, null);

            Globals.ConnectedScanner = null;
            Globals.ConnectedId = ConstantsString.DefaultScannerID;

        }

        



        /// <summary>
        /// Event handler of Scanner Connected
        /// </summary>
        /// <param name="scannerInfo">Scanner Information</param>
        private static void ScannerConnected(Scanner scannerInfo)
        {
            System.Diagnostics.Debug.WriteLine("Scanner Connected " + scannerInfo.Name);
            OnDeviceConnected(scannerInfo, null);
            UpdateScannerConnectionStatus(scannerInfo.Id, ConstantsString.CheckMark);
        }

        /// <summary>
        /// Event handler of Scanner Disappeared event
        /// </summary>
        /// <param name="scannerID">Scanner Id</param>
        private static void ScannerDisappeared(int scannerID)
        {
            System.Diagnostics.Debug.WriteLine("Scanner Disappear " + scannerID);
            RemoveDeviceFromList(scannerID);
            OnDeviceDisappeared(scannerID, null);
        }

        /// <summary>
        /// Scanner Appeared
        /// </summary>
        /// <param name="scannerInfo">Scanner Information</param>
        private static void ScannerAppeared(Scanner scannerInfo)
        {
            System.Diagnostics.Debug.WriteLine("Scanner appeared : " + scannerInfo.Name);
            AddNewScannerToScannerList(scannerInfo);

            var handler = ScannerAppearEvent;
            if (handler != null)
            {
                ScannerAppearEvent.Invoke(scannerInfo);

            }

        }
        /// <summary>
        /// Get stc pairing barcode
        /// </summary>
        /// <param name="barcodeType"> Barcode type</param>
        /// <param name="bluetoothProtocol"> Bluetooth communication protocol</param>
        /// <param name="scannerConfiguration"> Scanner configuration</param>
        /// <param name="bluetoothMAC"> Bluetooth mac address</param>
        /// <returns>Barcode images</returns>
        public static byte[] GetBluetoothParingBarcode(PairingBarcodeType barcodeType, BluetoothProtocol bluetoothProtocol, ScannerConfiguration scannerConfiguration, string bluetoothMAC)
        {
            byte[] imageData = sdkInstance.GetBluetoothPairingBarcode(barcodeType, bluetoothProtocol, scannerConfiguration, bluetoothMAC);
            return imageData;
        }

        /// <summary>
        /// Get stc pairing barcode without Bluetooth MAC address
        /// </summary>
        /// <param name="barcodeType"> Barcode type</param>
        /// <param name="bluetoothProtocol"> Bluetooth communication protocol</param>
        /// <param name="scannerConfiguration"> Scanner configuration</param>
        /// <returns>Barcode images</returns>
        public static byte[] GetBluetoothParingBarcode(PairingBarcodeType barcodeType, BluetoothProtocol bluetoothProtocol, ScannerConfiguration scannerConfiguration)
        {
            byte[] imageData = sdkInstance.GetBluetoothPairingBarcode(barcodeType, bluetoothProtocol, scannerConfiguration);
            return imageData;
        }
    
    }
}
