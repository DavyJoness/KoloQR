
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;

namespace KoloQR
{
	[Activity (Label = "Rating", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class Rating : Activity
	{
		TextView przedmiot;
		TextView ocena;
		TextView uwagi;
		TextView sprawdzian;

		string ocenaZqr;
		Ocena osoba = new Ocena();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);



			ISharedPreferences preferences = Application.Context.GetSharedPreferences("PREFS", FileCreationMode.Private);
			string index = preferences.GetString ("Login", "<error>");

			ocenaZqr = Intent.GetStringExtra ("szyfrogram") ?? "Jakiś problem";
			if (osoba.wyszukajWynik (ocenaZqr, index)) {	
				SetContentView (Resource.Layout.Rating);
				przedmiot = FindViewById<TextView> (Resource.Id.przedmiot);
				ocena = FindViewById<TextView> (Resource.Id.ocena);
				uwagi = FindViewById<TextView> (Resource.Id.uwaga);
				sprawdzian = FindViewById<TextView> (Resource.Id.nazwaSprawdzianuLabel);

				przedmiot.Text = osoba.przedmiot;
				uwagi.Text = osoba.uwagi;
				sprawdzian.Text = osoba.nazwaSprawdzianu;

				switch (osoba.ocena) {
				case 20:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#FF0000"));
					ocena.Text = "Niedostateczny(2.0)";
					break;
				case 30:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#FFDD00"));
					ocena.Text = "Dostateczny(3.0)";
					break;
				case 35:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#F6FF00"));
					ocena.Text = "Dostateczny plus(3.5)";
					break;
				case 40:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#00FF00"));
					ocena.Text = "Dobry(4.0)";
					break;
				case 45:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#00FF00"));
					ocena.Text = "Dobry plus(4.5)";
					break;
				case 50:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#00FF00"));
					ocena.Text = "Bardzo Dobry(5.0)";
					break;
				case 5:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#FF0000"));
					ocena.Text = "Niezaliczony";
					break;
				case 10:
					ocena.SetTextColor (Android.Graphics.Color.ParseColor ("#00FF00"));
					ocena.Text = "Zaliczony";
					break;
				}
				if (dodajDoBazy (osoba)) {
					Toast info = Toast.MakeText(this, "Dodano do bazy", ToastLength.Short);
					info.Show ();
				}
			} else {
				AlertDialog.Builder adb = new AlertDialog.Builder (this).SetTitle ("Wynik operacji"); 
				AlertDialog showme = adb.Create ();
				showme.SetMessage ("W tym kodzie QR nie ma Twojej oceny");
				showme.SetCancelable (true);
				showme.SetButton ("Zamknij", delegate {
					Finish();
				});
				showme.Show ();
			}
		}

		public bool dodajDoBazy(Ocena obiekt)
		{
			bool wykonano;

			SQLiteDatabase sql = OpenOrCreateDatabase ("OCENA", 0, null);

			if (czyDodanoWczesniej (sql, obiekt.id)) {
				sql.ExecSQL("INSERT INTO wyniki VALUES ("+obiekt.id+",'"+obiekt.przedmiot+"','"+obiekt.nazwaSprawdzianu+"',"+obiekt.ocena+",'"+obiekt.uwagi+"','"+aktualnaData()+"')");
				sql.Close ();
				wykonano = true;
			} else {
				sql.Close ();
				wykonano = false;
			}
			return wykonano;
		}

		public bool czyDodanoWczesniej(SQLiteDatabase sql, int id)
		{
			var kursor = sql.RawQuery ("SELECT count(id) FROM `wyniki` WHERE id="+id.ToString(), null);
			kursor.MoveToFirst ();
			int i = kursor.GetInt (0);
			if (i > 0) {
				return false;
			} else {
				return true;
			}
		}

		public string aktualnaData()
		{
			DateTime dzis = DateTime.Today;
			string dzisCiurek = formatData(dzis.Day) + "." + formatData(dzis.Month) + "." + dzis.Year.ToString ();
			return dzisCiurek;
		}

		public string formatData(int wartosc)
		{
			if (wartosc < 10)
				return "0" + wartosc.ToString ();
			else
				return wartosc.ToString();
		}
	}
}

