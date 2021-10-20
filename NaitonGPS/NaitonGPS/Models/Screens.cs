using Xamarin.Forms;

namespace NaitonGPS.Models
{
    public class Screens
    {
        public int screenNumber { get; set; }
        public string ScreenTitle { get; set; }
        public string ScreenImage { get; set; }
        public ControlTemplate ScreenLink { get; set; }

        public IGestureRecognizer ScreLink { get; set; }
    }
}
