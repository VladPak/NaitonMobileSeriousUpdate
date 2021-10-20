using Xamarin.Forms;

namespace NaitonGPS.Triggers
{
 public class Triggers : TriggerAction<Image>
    {
        protected async override void Invoke(Image img)
        {
            await img.ScaleTo(1, 2, Easing.Linear);
        }

    }
}
