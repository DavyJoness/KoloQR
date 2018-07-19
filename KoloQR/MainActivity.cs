using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Preferences;

namespace KoloQR
{
	[Activity (Label = "KoloQR", MainLauncher = true, NoHistory=true, Icon = "@drawable/icon2", Theme = "@style/Theme.Splash", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			try{

			ISharedPreferences preferences = Application.Context.GetSharedPreferences ("PREFS", FileCreationMode.Private);
			bool loggedInto = preferences.GetBoolean ("logged", false);

			if (loggedInto) {
				var activity = new Intent (this, typeof(LoginX));
				StartActivity (activity);
				Finish ();
			} else {
				var activity = new Intent (this, typeof(Login));
				StartActivity (activity);
				Finish ();
			}
			}
			catch(Exception e) {
				AlertDialog.Builder adb = new AlertDialog.Builder (this).SetTitle ("Wynik logowania"); 
				AlertDialog showme = adb.Create ();
				showme.SetMessage ("Blad "+e.HResult+" o tresci: "+e.Message);
				showme.SetCancelable (true);
				showme.SetButton("OK", delegate {});
				showme.Show ();
			}
		}
	}
}


