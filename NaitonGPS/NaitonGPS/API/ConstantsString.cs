using System;
namespace NaitonGPS.API
{
    public static class ConstantsString
    {    
        
        //Version
        public const string ScanVersion = "Zebra Scanner Control v.";
        public const string SdkVersion = "Zebra Scanner SDK v.";
        public const string CopyrightMsg = "©2020 Zebra Technologies Corp. and/or its affiliates.  All rights reserved";

        //Instructions
        public const string InstructionCS4070_1 = "1.  Scan the \"Reset Factory Defaults\" barcode below:";
        public const string InstructionCS4070_2 = "2.  Scan the \"Bluetooth MFi_SSI\" barcode below to pair:";
        public const string InstructionCS4070_3 = "3.  Enable Bluetooth on the device.";
        public const string InstructionCS4070_4 = "4.  The device will discover the CS4070 and display it on the Bluetooth Devices list.";
        public const string InstructionCS4070_5 = "5.  Tap the \"CS4070\" to pair from the Bluetooth Devices list.";
        public const string InstructionCS4070_6 = "6.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and select the CS4070.";

        public const string InstructionRFD8500_1 = "1.  Turn the RFD8500 on and ensure Bluetooth is enabled.\n\nThe RFD8500 is discoverable over Bluetooth for 40 seconds after start up.After that time Bluetooth suspends and is no longer discoverable.To make discoverable again, press the Bluetooth button, located on the side of RFD8500.";
        public const string InstructionRFD8500_2 = "2.  Enable Bluetooth on the device.";
        public const string InstructionRFD8500_3 = "3.  The device will discover the RFD8500 and display it on the Bluetooth Devices list.";
        public const string InstructionRFD8500_4 = "4.  Tap the \"RFD8500\" to initiate pairing from the Bluetooth Devices list.";
        public const string InstructionRFD8500_5 = "5.  Press the RFD8500 trigger to complete pairing when the Bluetooth LED starts flashing fast.";

        public const string InstructionLIDS3678_1 = "1.  If previously paired, use Bluetooth Settings to unpair the LI/DS3678.";
        public const string InstructionLIDS3678_2 = "2.  Scan the \"Reset Factory Defaults\" barcode below:";
        public const string InstructionLIDS3678_3 = "3.  Scan the \"SSI BT LE\" barcode below to pair:";
        public const string InstructionLIDS3678_4 = "4.  Enable Bluetooth on the device.";
        public const string InstructionLIDS3678_5 = "5.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and wait for the LI/DS3678 to appear in the list.";
        public const string InstructionLIDS3678_6 = "6.  Tap the LI/DS3678 to initiate pairing.";
        public const string InstructionLIDS3678_7 = "7.  Tap the \"Pair\" button when the \"Bluetooth Pairing Request\" prompt appears.";

        public const string InstructionDS2278_1 = "1.  If previously paired, use Bluetooth Settings to unpair the DS2278.";
        public const string InstructionDS2278_2 = "2.  Scan the \"Reset Factory Defaults\" barcode below:";
        public const string InstructionDS2278_3 = "3.  Scan the \"SSI BT LE\" barcode below to pair:";
        public const string InstructionDS2278_4 = "4.  Enable Bluetooth on the device.";
        public const string InstructionDS2278_5 = "5.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and wait for the DS2278 to appear in the list.";
        public const string InstructionDS2278_6 = "6.  Tap the DS2278 to initiate pairing.";
        public const string InstructionDS2278_7 = "7.  Tap the \"Pair\" button when the \"Bluetooth Pairing Request\" prompt appears.";

        public const string InstructionRS5100_1 = "1.  If previously paired, use Bluetooth Settings to unpair the RS5100.";
        public const string InstructionRS5100_2 = "2.  Scan the \"Reset Factory Defaults\" barcode below:";
        public const string InstructionRS5100_3 = "3.  Scan the \"SSI BT LE\" barcode below to pair:";
        public const string InstructionRS5100_4 = "4.  Enable Bluetooth on the device.";
        public const string InstructionRS5100_5 = "5.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and wait for the RS5100 to appear in the list.";
        public const string InstructionRS5100_6 = "6.  Tap the RS5100 to initiate pairing.";
        public const string InstructionRS5100_7 = "7.  Tap the \"Pair\" button when the \"Bluetooth Pairing Request\" prompt appears.";


        public const string InstructionDefault = "Scan the barcode below to restore to default parameters:";

        public const string PairCS4070 = "Pair CS4070";
        public const string PairCS6080 = "Pair CS6080";
        public const string PairRFD8500 = "Pair RFD8500";
        public const string PairLIDS3678 = "Pair LI/DS3678";
        public const string PairDS8178 = "Pair DS8178";
        public const string PairDS2278 = "Pair DS2278";
        public const string PairRS5100 = "Pair RS5100";
        public const string PairDS8178BTLE = "Pair DS8178(BTLE)";
        public const string PairDS8178MFI = "Pair DS8178(MFI)";
        public const string SetDefaults = "Set Defaults";

        public const string InstructionDS8178BTLE_1 = "1.  If previously paired, use Bluetooth Settings to unpair the DS8178.";
        public const string InstructionCS6080BTLE_1 = "1.  If previously paired, use Bluetooth Settings to unpair the CS6080.";
        public const string InstructionBTLE_2 = "2.  Scan the \"Reset Factory Defaults\" barcode below:";
        public const string InstructionBTLE_3 = "3.  Scan the \"SSI BT LE\" barcode below to pair:";
        public const string InstructionBTLE_4 = "4.  Enable Bluetooth on the device.";
        public const string InstructionDS8178BTLE_5 = "5.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and wait for the DS8178 to appear in the list.";
        public const string InstructionCS6080BTLE_5 = "5.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and wait for the CS6080 to appear in the list.";
        public const string InstructionDS8178BTLE_6 = "6.  Tap the DS8178 to initiate pairing.";
        public const string InstructionCS6080BTLE_6 = "6.  Tap the CS6080 to initiate pairing.";
        public const string InstructionBTLE_7 = "7.  Tap the \"Pair\" button when the \"Bluetooth Pairing Request\" prompt appears.";

        public const string InstructionDS8178MFI_1 = "1.  If previously paired, use Bluetooth Settings to unpair the DS8178.";
        public const string InstructionCS6080MFI_1 = "1.  If previously paired, use Bluetooth Settings to unpair the CS6080.";
        public const string InstructionMFI_2 = "2.  Scan the \"Reset Factory Defaults\" barcode below:";
        public const string InstructionMFI_3 = "3.  Scan the \"Bluetooth MFi_SSI\" barcode below to pair:";
        public const string InstructionMFI_4 = "4.  Enable Bluetooth on the device.";
        public const string InstructionDS8178MFI_5 = "5.  The device will discover the DS8178 and display it on the Bluetooth Devices list.";
        public const string InstructionCS6080MFI_5 = "5.  The device will discover the CS6080 and display it on the Bluetooth Devices list.";
        public const string InstructionDS8178MFI_6 = "6.  Tap the \"DS8178\" to pair from the Bluetooth Devices list.";
        public const string InstructionCS6080MFI_6 = "6.  Tap the \"CS6080\" to pair from the Bluetooth Devices list.";
        public const string InstructionDS8178MFI_7 = "7.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and select the DS8178.";
        public const string InstructionCS6080MFI_7 = "7.  Go to the \"Zebra Scanner Control\" app's \"Connect\" screen and select the CS6080.";

        public const string NoInstructions = "";

        //Navigation
        public const string Back = "Back";

        //Image Assert
        public const string ImgFactoryDefaultBarcode = "CS4070FactorySetDefault";
        public const string ImgCS4070MFiSSIBarcode = "CS4070MFi_SSI";
        public const string ImgArrow = "ArrowImage";
        public const string ImgBarcode = "Barcode";
        public const string Img3678ResetBarcode = "Barcode3678Reset";
        public const string Img3678SSIBarcode = "Barcode3678SSI";
        public const string ImgRS5100ResetBarcode = "Barcode3678Reset";
        public const string ImgRS5100SSIBarcode = "Barcode3678SSI";
        public const string Img2278Default = "Barcode2278FactoryDefault";
        public const string Img2278SSI = "Barcode2278SSI";
        public const string ImgEmpty = "";
        public const string Img8178FactoryBLEBarcode = "DS8178FactoryBTLE";
        public const string Img8178SSIBLEBarcode = "DS8178SSIBTLE";
        public const string Img6080BTLEBarcode = "CS6080BTLE";
        public const string Img6080MFIBarcode = "CS6080MFI";
        public const string ImgDS8178MFIFactory = "DS8178MFIFactory";
        public const string ImgDS8178MFISSI = "DS8178MFISSI";
        public const string ImgTabInfo = "TabInfo";
        public const string ImgTabBarcode = "TabBarcode";
        public const string ImgTabSettings= "TabSetting";


        //Main Page List 
        public const string MainListItemConnect = "Connect";
        public const string MainListItemConnectHelp = "Connection Help";
        public const string MainListItemAppSetting = "App Settings";
        public const string MainListItemAbout = "About";

        //DS8178 Page List
        public const string DS8178ListItemMFI = "MFI";
        public const string DS8178ListItemBTLE = "BTLE";

        //Mode Type
        public const string MFI = "MFI";//1
        public const string BTLE = "BTLE";//2
        public const string MFI_BTLE = "MFI_BTLE";//3
        public const string SSI = "SSI";//4
        public const string SNAPI = "SNAPI";//5
        public const string Invalid = "Invalid";
        public const int MFIType = 1;
        public const int BTLEType = 2;
        public const string ALL = "ALL";
        public const string Enable = "Enable";
        public const string Disable = "Disable";

        //Title
        public const string ActiveScannerTitle = "Active Scanner";
        public const string AssertInfoTitle = "Asset Information";

        //Color Codes
        public const string ColorNavigationBarBlue = "#389cff";

        //Assert info
        public const string xmlInScannerArg = "</scannerID>";
        public const string xmlCmdArg = "<cmdArgs>";
        public const string xmlarg_int = "<arg-int>";

        public const string xmlEndArg_int = "</arg-int>";
        public const string xmlEndCmdArg = "</cmdArgs>";
        public const string xmlInArg = "<inArgs><scannerID>";
        public const string xmlEndInArg = "</inArgs>";
        public const string xmlIn = "<cmdArgs><arg-xml><attrib_list>20012,535,534,533</attrib_list></arg-xml></cmdArgs></inArgs>";
        public const int Opcode5001 = 5001;
        public const string FirmwareAttributeID = "20012";
        public const string DOMAttributeID = "535";
        public const string ScannerIDKey = "scannerID";
        public const string SerialnumberKey = "serialnumber";
        public const string ModelnumberKey = "modelnumber";
        public const string AttributeKey = "attribute";
        public const string IDKey = "id";
        public const string ValueKey = "value";

        public const string DefaultScannerID = "-1";
        public const int ConnectedRow = -1;

        //Alert Message
        public const string MsgDisconnect = "This will disconnect the application from the scanner, however the device will still be paired to the system.";
        public const string MsgAppsettingDisconnect = " will disconnect the application from the scanner";
        public const string MsgDisconnectTitle = "Disconnect?";
        public const string MsgDisconnectAction = "Disconnect?";
        public const string MsgCancelAction = "Cancel";
        public const string MsgContinueAction = "Continue";
        public const string Msg = "Message";
        public const string MsgUnableToCommunicate = "Unable to communicate with scanner.";
        
        public const string MsgConnectTitle = "Zebra Scanner Control";
        public const string MsgConnected = "\n has connected";
        public const string MsgActionOk = "OK";
        public const string MsgNotSupported = "Feature not supported.";

        //Rows
        public const int Row1 = 0;
        public const int Row2 = 1;
        public const int Row3 = 2;
        public const int Row4 = 3;
        public const int Row5 = 4;
        public const int Row6 = 5;
        public const int Row7 = 6;
        public const int Row8 = 7;

        //Instr
        public const int Instruction1 = 0;
        public const int Instruction2 = 1;
        public const int Instruction3 = 2;
        public const int Instruction4 = 3;
        public const int Instruction5 = 4;
        public const int Instruction6 = 5;
        public const int Instruction7 = 6;
        public const int Instruction8 = 7;

        //Version
        public const string BundleVersion = "CFBundleShortVersionString";

        //App settings
        public const string CheckMark = "\u2713";
        public const string UnCheckMark = "      ";
        public const string ModeTypeStoringKey = "OpMode";
        public const int UntappedRow = -1;
        public const string EmptyModeType = "-";
        public const string BluetoothDiscoveryStatus = "BluetoothDiscoveryStatus";

        //SettingsMenuItems
        public const string Symbologies = "Symbologies";
        public const string Beeper = "Beeper";
        public const string EnableScanning = "Enable scanning";
        public const string DisableScanning = "Disable scanning";
        public const string AimOn = "Aim On";
        public const string AimOff = "Aim Off";
        public const string VibrationFeedback = "Vibration Feedback";
        public const string ChangeMode = "Change Mode";
        public const string FirmwareUpdate = "Firmware Update";

        //Symbologies
        public const string UPC_A = "UPC-A";
        public const string UPC_E = "UPC-E";
        public const string UPC_E1 = "UPC-E1";
        public const string EAN_8_JAN8 = "EAN-8/JAN8";
        public const string EAN_13_JAN13 = "EAN-13/JAN13";
        public const string BOOLAND_EAN = "Bookland EAN";

        public const string CODE_128 = "Code 128";
        public const string GS1_128 = "GS1-128";
        public const string Code_39 = "Code 39";
        public const string Code_93 = "Code 93";
        public const string Code_11 = "Code 11";
        public const string INTERVEL_2_5 = "Interleaved 2 of 5";

        public const string DISCRETE_2_5 = "Discrete 2 of 5";
        public const string CHINESE_2_5 = "Chinese 2 of 5";

        public const string CODEBAR = "Codabar";
        public const string MSI = "MSI";
        public const string DATA_MATRIX = "Data Matrix";
        public const string PDF = "PDF";
        public const string ISBT_128 = "ISBT 128";
        public const string UCC_COUPON_EXTENDED_CODE = "UCCCouponExtendedCode";

        public const string US_POSTNET = "US Postnet";
        public const string US_PLANET = "US Planet";
        public const string UK_POST = "UKPost";
        public const string US_POSTAL_CHECK_DIGIT = "USPostal Check Digit";
        public const string UK_POSTAL_CHECK_DIGIT = "UKPostal Check Digit";
        public const string JAPAN_POST = "JapanPost";


        public const string AUS_POST = "AusPost";
        public const string GS1_DATA_BAR_14 = "GS1DataBar14";
        public const string GS1_DATA_BAR_LIMITED = "GS1DataBarLimited";
        public const string GS1_DATA_BAR_EXPANDED = "GS1DataBarExpanded";
        public const string MICRO_PDF = "MicroPDF";
        public const string MAXI_CODE = "MaxiCode";


        public const string ISSN_EAN = "ISSN EAN";
        public const string MATRIX_2_5 = "Matrix 2 of 5";
        public const string KOREAN_3_5 = "Korean 3 of 5";
        public const string QR_CODE = "QR Code";
        public const string MICRO_QR_CODE = "Micro QR Code";
        public const string AZTEC = "Aztec";


        public const string HANXIN = "HanXin";
        public const string COMPOSITE_CC_C = "Composite CC-C";
        public const string COMPOSITE_CC_A_B = "Composite CC-A/B";
        public const string COMPOSITE_TLC_39 = "Composite TLC-39";
        public const string NETHERLANDS_KIX = "Netherlands KIX";
        public const string UPU_FICS = "UPU FICS";

        public const string BarcodeCount = "BARCODE LIST: COUNT = ";
        public const string Empty = "";

        public const string SampleScanner1 = "Scanner 1";
        public const string SampleScanner2 = "Scanner 2";

        //Tab bar title
        public const string TabTitleInfo = "Info";
        public const string TabTitleBarcode = "Barcode";
        public const string TabTitleSettings = "Settings";
        public const string TitleAbout = "About";
        public const string TitleConnectionHelp = "Connection Help";
        

        public const string ComunicationModeSSI = "  SSI";

        public const string FIRMWARE_FOLDER_PATH = "ZebraFirmware/";
        public const string OUTPUT_FOLDER_PATH = "ZebraOutput/";     
        public const string SCNPLG = ".SCNPLG";
        public const string METADATA_FILE = "Metadata.xml";
        public const string TXT_FILE = ".txt";
        public const string PNG_FILE = ".png";
        public const string JPEG_FILE = ".jpeg";
        public const string RMD_ATTR_VALUE_ACTION_FAST_BLINK = "85";
        public const string RMD_ATTR_VALUE_ACTION_FAST_BLINK_OFF = "90";




        public const string FirmwareUpdateTitle = "Firmware update";
        public const string DecendentComponent = "component";
        public const string DecendentCombineFirmware = "combined-firmware";
        public const string DecendentReleaseDate = "release-date";
        public const string DecendentRevision = "revision";
        public const string DecendentFamily = "family";
        public const string DecendentName = "name";
        
        public const string Release = "Release ";
        public const string FirmwareNameSeprator = " , ";
        public const string FirmwarePrecentageFormater = "{0:0.00}";
        public const string FirmwarePrecentageSymbol = " %";

        //ScanToConnect

        public const string BluetoothMacAddress = "";
        public const string BluetoothMacAddressStoringKey = "BluetoothMacAddress";
        public const int BluetoothMacAddressLength = 17;
        public const char BluetoothMacAddressColon = ':';
        public const string BluetoothMacAddressRegex = "[^A-F0-9:]";
        public const string ComProtocol = "ComProtocol";
        public const string BarcodeTypeStoringKey = "BarcodeType";
        public const string BarcodeTypeSTC = "BARCODE_TYPE_STC";
        public const string BarcodeTypeLegacyPairing = "BARCODE_TYPE_LEGACY";
      
        public const string ComProtocolMFI = "SSI_BT_MFI";
        public const string ComProtocolBLE = "SSI over Bluetooth Low Energy(BLE)";
        public const string ComProtocolCLASSIC = "SSI over Bluetooth Classic";
        public const string SetFactoryDefaults = "SET_FACTORY_DEFAULTS";



        public const double ScaleSize = 1.9;
        public const double XaxisMove = 12;
        public const double YaxisMove = 0;
        public const int Movement  = 0;
        public const double ImageDataDevider = 10;

        public const double SixInchScreen = 540;


    }
}

