
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ZXing;
using ZXing.Mobile;
using Android.Database.Sqlite;

namespace KoloQR
{
	[Activity (Label = "MainMenu", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class MainMenu : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.MainMenu);

			SQLiteDatabase sql = OpenOrCreateDatabase ("OCENA", 0, null);
			sql.ExecSQL ("CREATE TABLE IF NOT EXISTS wyniki(id INT NOT NULL, przedmiot VARCHAR( 25 ) NOT NULL, sprawdzian VARCHAR( 20 ) NOT NULL, ocena INT NOT NULL, uwaga VARCHAR( 50 ), data VARCHAR(12) NOT NULL)");
			sql.Close ();

			Button b1 = FindViewById<Button> (Resource.Id.opcja1);
			Button b2 = FindViewById<Button> (Resource.Id.opcja2);
			Button b3 = FindViewById<Button> (Resource.Id.opcja3);

			b1.Click += delegate { skanujKod(); };
			b2.Click += history;
			b3.Click += delegate { zamykacz(); };
		}

		public void history(Object sender, EventArgs e)
		{
			Intent intentH = new Intent(this, typeof(History));
			StartActivity (intentH);

		}

		public override void OnBackPressed()
		{
			zamykacz ();
		}

		public void zamykacz()
		{
			AlertDialog.Builder dialog = new AlertDialog.Builder (this);
			AlertDialog zamknij = dialog.Create ();
			zamknij.SetMessage ("Czy na pewno chcesz zamknąć program?");
			zamknij.SetCancelable (true);

			zamknij.SetButton ("Tak", delegate {
				Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
				System.Environment.Exit(0);

			});
			zamknij.SetButton2 ("Nie", delegate {

			});
			zamknij.Show ();
		}

		public async void skanujKod()
		{
			MobileBarcodeScanner mobbarsc = new MobileBarcodeScanner (this);

			mobbarsc.UseCustomOverlay = false;
			mobbarsc.TopText = "Przybliż telefon na 10 cm od kodu";
			mobbarsc.BottomText = "Zeby odczytac swój wynik";

			var result = await mobbarsc.Scan (); //w zmiennej 'result' beda zaszyfrowane dane 

			if (result != null && !string.IsNullOrEmpty (result.Text)) {
				decryptQR dekoder = new decryptQR ();
				try{
				string odkod = dekoder.DecryptRJ256 (Decode (result.ToString ()), decryptQR.key, decryptQR.iv);

				Intent intent = new Intent (this, typeof(Rating));
				intent.PutExtra ("szyfrogram", odkod.ToString ());//tu musza byc juz odkodowane dane
				StartActivity (intent);

				}catch(Exception e) {
					Toast info = Toast.MakeText (this, "Niepoprawny kod", ToastLength.Short);
					info.Show ();
				}
			}
		}


		public byte[] Decode(string str)
		{
			var decbuff = Convert.FromBase64String(str);
			return decbuff;
		}
	}
}

