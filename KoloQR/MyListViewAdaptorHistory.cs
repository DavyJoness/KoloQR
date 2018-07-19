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
	public class MyListViewAdaptorHistory : BaseAdapter<String>
	{
		private List<String> lista;
		private Context context;

		public MyListViewAdaptorHistory (Context context, List<String> lista)
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

		public override string this[int index] {
			get {return lista [index];}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) {
				row = LayoutInflater.From(this.context).Inflate(Resource.Layout.History, null, false);
			}
			TextView przedmiot = row.FindViewById<TextView> (Resource.Id.textView1);
			przedmiot.Text = lista [position];

			return row;
		}

	}
}

