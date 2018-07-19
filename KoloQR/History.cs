
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
	public class History : Activity
	{
		List<string> lista;
		ListView lv;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ShowList);

			lv = FindViewById<ListView> (Resource.Id.listView1);

			lista = new List<string> ();

			SQLiteDatabase sql = OpenOrCreateDatabase ("OCENA", 0, null);
			var kursor = sql.RawQuery ("select distinct(przedmiot) from wyniki", null);
			while (kursor.MoveToNext ()) 
			{
				lista.Add (kursor.GetString (0));
			}
			sql.Close ();

			MyListViewAdaptorHistory adapter = new MyListViewAdaptorHistory (this, lista);
			lv.Adapter = adapter;

			lv.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs position) {

				String selectedFromList =(String) (lv.GetItemAtPosition(position.Position));

				Intent intent = new Intent(this, typeof(Subject));
				intent.PutExtra("przedmiot",selectedFromList);
				StartActivity(intent);
			};
		}
	}
}

