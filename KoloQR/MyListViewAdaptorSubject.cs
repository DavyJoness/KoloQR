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

namespace KoloQR
{
	public class MyListViewAdaptorSubject : BaseAdapter<Historia>
	{
		private List<Historia> lista;
		private Context context;

		public MyListViewAdaptorSubject (Context context, List<Historia> lista)
		{
			this.lista = lista;
			this.context = context;
		}

		public override long GetItemId (int position)
		{
			return position;
		}

		public override int Count {
			get {return lista.Count;}
		}

		public override Historia this[int index] {
			get {return lista [index];}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) {
				row = LayoutInflater.From(this.context).Inflate(Resource.Layout.Subject, null, false);
			}

			TextView sprawdzian = row.FindViewById<TextView> (Resource.Id.przedmiotSprawdzian);
			sprawdzian.Text = lista [position].nazwaSprawdzianu;

			TextView data = row.FindViewById<TextView> (Resource.Id.data);
			data.Text = lista [position].data;

			TextView ocenaZeSpr = row.FindViewById<TextView> (Resource.Id.przedmiotOcena);
			ocenaZeSpr.Text = nazwaOceny(lista [position].ocena);

			TextView uwagi = row.FindViewById<TextView> (Resource.Id.przedmiotUwagi);
			uwagi.Text = lista [position].uwagi;

			ImageView grafika = row.FindViewById<ImageView> (Resource.Id.grafikaOcena);

			switch (lista [position].ocena) 
			{
			case 20:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_2);
				break;
			case 30:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_3);
				break;
			case 35:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_3);
				break;
			case 40:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_4);
				break;
			case 45:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_4);
				break;
			case 50:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_5);
				break;
			case 10:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_5);
				break;
			case 5:
				grafika.SetImageResource (Resource.Drawable.star_rating_icon_2);
				break;
			}

			return row;
		}

		public string nazwaOceny(int ocena)
		{
			switch (ocena) 
			{
			case 20:
				return "Niedostateczny(2.0)";

			case 30:
				return "Dostateczny(3.0)";

			case 35:
				return "Dostateczny plus(3.5)";

			case 40:
				return "Dobry(4.0)";

			case 45:
				return "Dobry plus(4.5)";

			case 50:
				return "Bardzo Dobry(5.0)";

			case 5:
				return "Niezaliczony";

			case 10:
				return "Zaliczony";

			default:
				return "Problem";

			}

		}

	}
}

