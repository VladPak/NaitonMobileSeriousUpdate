using Xamarin.Forms;

namespace NaitonGPS.Triggers
{
    public class TriggerLabelNavMenu : TriggerAction<Frame>
    {

        protected override void Invoke(Frame frm)
        {
             frm.BackgroundColor = Color.Red;
        }
    }
}
