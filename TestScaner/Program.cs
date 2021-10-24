using System;
using NaitonGPS.API;
using ZebraBarcodeScannerSDK;

namespace TestScaner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SdkHandler.EnableBluetoothScannerDiscovery();
            SdkHandler.SetSTCEnabledState(true);
            SdkHandler.BarcodeDataEvent += BarcodeDataReceivedEvent;            
            Console.ReadLine();
        }

        private static void BarcodeDataReceivedEvent(BarcodeData barcodeData, int scannerID)
        {
            Console.WriteLine(scannerID);
            Console.WriteLine(barcodeData.ToString());
        }
    }
}
