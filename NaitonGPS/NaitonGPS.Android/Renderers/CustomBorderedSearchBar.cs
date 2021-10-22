using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Service.Controls;
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

[assembly: ExportRenderer(typeof(BorderedSearchBar), typeof(CustomBorderedSearchBar))]
namespace NaitonGPS.Droid.Renderers
{
    public class CustomBorderedSearchBar : SearchBarRenderer
    {
        public CustomBorderedSearchBar(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(Color.FromHex("#ffffff").ToAndroid());
                gd.SetCornerRadius(10);
                gd.SetStroke(2, Color.FromHex("#69717E").ToAndroid());
                Control.Background = gd;

                LinearLayout linearLayout = this.Control.GetChildAt(0) as LinearLayout;
                linearLayout = linearLayout.GetChildAt(2) as LinearLayout;
                linearLayout = linearLayout.GetChildAt(1) as LinearLayout;

                linearLayout.Background = null; //removes underline
            }
        }

    }
}