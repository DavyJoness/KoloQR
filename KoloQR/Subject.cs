
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
using Android.Database.Sqlite;

namespace KoloQR
{
	[Activity (Label = "Historia wyników", Theme = "@android:style/Theme", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class Subject : Activity
	{
		List<Historia> lista;
		ListView lv;
		String przedmiot;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ShowList);

			lv = FindViewById<ListView> (Resource.Id.listView1);

			przedmiot = Intent.GetStringExtra ("przedmiot") ?? "";
			TextView label = FindViewById<TextView> (Resource.Id.Label4Id);
			label.Text = przedmiot;

			lista = new List<Historia> ();

			SQLiteDatabase sql = OpenOrCreateDatabase ("OCENA", 0, null);
			var kursor = sql.RawQuery ("select sprawdzian, ocena, uwaga, data from wyniki where przedmiot='"+przedmiot+"'", null);
			while (kursor.MoveToNext ()) 
			{
				lista.Add (new Historia {nazwaSprawdzianu = kursor.GetString (0), ocena = kursor.GetInt (1), uwagi = kursor.GetString (2), data = kursor.GetString(3)});
			}
			sql.Close ();

			MyListViewAdaptorSubject adapter = new MyListViewAdaptorSubject (this, lista);
			lv.Adapter = adapter;
		}
	}
}

