using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using Xamarin.Forms;

namespace NaitonGPS.Droid
{
    [Activity(Label = "Naiton", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Removes status bar at the very top (time, battery level, network)
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            PhantomLib.Droid.Effects.Effects.Init();
            ZXing.Net.Mobile.Forms.Android.Platform.Init(); 
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);


            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnKeyMultiple([GeneratedEnum] Keycode keyCode, int repeatCount, KeyEvent e)
        {
            return base.OnKeyMultiple(keyCode, repeatCount, e);
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            App.Current.MainPage.DisplayAlert("Info 1", $"1 {(e.Characters ?? "").ToString()}", "Ok");
            App.Current.MainPage.DisplayAlert("Info 2", $"1 {keyCode.ToString()}", "Ok");

            if (keyCode == Keycode.Enter)
                {
                //SearchBar searchBar = FindViewById<SearchBar>(Resource.Id.searchT);
                //if (e.Characters.Length > 5)
                //    {
                //        m_barcodeValue = new string(m_barcode.ToArray());
                //        m_barcode.Clear();
                //        CCDScanned(m_barcodeValue);
                //    }
                App.Current.MainPage.DisplayAlert("Info",$"1 {(e.Characters??"").ToString()}","Ok");
                }
                
                return base.OnKeyUp(keyCode, e);
        }
    }
}