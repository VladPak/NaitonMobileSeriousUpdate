using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace NaitonGPS.Droid
{
	[Activity(Label = "Naiton", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		List<char> barCode = new List<char>();
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

		public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
		{
			try
			{

				//if (e.Device != null)
				//	App.Current.MainPage.DisplayAlert("Device info", $"{e.Device}", "Ok");

				//App.Current.MainPage.DisplayAlert("Device Id", $"{e.DeviceId}", "Ok");
				//App.Current.MainPage.DisplayAlert("Event time", $"{e.EventTime}", "Ok");
				//App.Current.MainPage.DisplayAlert("Scan code", $"{e.ScanCode}", "Ok");
				//App.Current.MainPage.DisplayAlert("Source", $"{e.Source}", "Ok");
				//App.Current.MainPage.DisplayAlert("Info 2", $"{keyCode}", "Ok");

				////e.Device.KeyCharacterMap

				//if ((int)keyCode == 10036 || keyCode == Keycode.Enter)
				//{
				//	//SearchBar searchBar = FindViewById<SearchBar>(Resource.Id.searchT);
				//	//if (e.Characters.Length > 5)
				//	//    {
				//	//        m_barcodeValue = new string(m_barcode.ToArray());
				//	//        m_barcode.Clear();
				//	//        CCDScanned(m_barcodeValue);
				//	//    }
				//	App.Current.MainPage.DisplayAlert("Info 3", $"I'm in", "Ok");
				//}
				//App.Current.MainPage.DisplayAlert("Scan code", $"{keyCode}", "Ok");
				if (e.KeyCode == Keycode.Enter || (int)keyCode == 10036)
				{
					string barCodeString = new string(barCode.ToArray());
					App.Current.MainPage.DisplayAlert("Scan code", $"{barCodeString}", "Ok");
					barCode.Clear();
				}
			}
			catch (Exception ex)
			{
				App.Current.MainPage.DisplayAlert("Error", $"{ex.Message}", "Ok");
			}
			return base.OnKeyUp(keyCode, e);
		}

		private DateTime m_lastKeystroke = new DateTime(0);
		public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent? e)
		{
			var unicodeChar = (char)e.UnicodeChar;

			TimeSpan elapsed = (DateTime.Now - m_lastKeystroke);
			if (elapsed.TotalMilliseconds > 100)
			{
				//barCode.Clear();
			}
			barCode.Add(unicodeChar);
			m_lastKeystroke = DateTime.Now;

			App.Current.MainPage.DisplayAlert("Key code ", $"{e.KeyCode}", "Ok");

			return base.OnKeyDown(keyCode, e);
		}
	}
}