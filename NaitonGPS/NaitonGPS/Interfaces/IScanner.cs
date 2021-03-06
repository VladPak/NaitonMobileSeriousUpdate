using System;
namespace NaitonGPS
{
    public interface IScanner
    {
        event EventHandler<StatusEventArgs> OnScanDataCollected;
        event EventHandler<string> OnStatusChanged;

        void Read();

        void Enable();

        void Disable();

        void SetConfig(IScannerConfig a_config);
    }
}
