using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NaitonGPS.Controls;
using NaitonGPS.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderedEntry), typeof(CustomBorderEntry))]
namespace NaitonGPS.Droid.Renderers
{
    public class CustomBorderEntry : EntryRenderer
    {
        public CustomBorderEntry(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                //Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                ////Control.Bottom = 5;

                //int[][] states = new int[][] {
                //new int[] { -Android.Resource.Attribute.StateFocused}, // enabled
                //new int[] {Android.Resource.Attribute.StateFocused} // disabled
                //};

                //int[] colors = new int[] { Color.LightGray.ToAndroid(), Color.FromHex("#66a103").ToAndroid() };

                //ColorStateList myList = new ColorStateList(states, colors);

                //GradientDrawable gradientDrawable = new GradientDrawable();
                //gradientDrawable.SetCornerRadius(15);
                //Control.Background = gradientDrawable;
                //gradientDrawable.SetStroke(2, myList);

                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Color.FromHex("#E7E8E7").ToAndroid());
                gd.SetCornerRadius(15);
                gd.SetStroke(2, Color.FromHex("#E7E8E7").ToAndroid());
                Control.Background = gd;
            }
        }
    }
}