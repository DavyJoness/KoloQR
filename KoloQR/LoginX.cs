
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
using System.Security.Cryptography;
using Android.Views.InputMethods;

namespace KoloQR
{
	[Activity (Label = "LoginX", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class LoginX : Activity
	{
		TextView whoLog;
		EditText pass;
		Button zaloguj;
		LinearLayout mlineLay;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.LoginX);

			whoLog = FindViewById<TextView> (Resource.Id.textView1);
			pass = FindViewById<EditText> (Resource.Id.editText1);
			zaloguj = FindViewById<Button> (Resource.Id.button1);
			mlineLay = FindViewById<LinearLayout> (Resource.Id.linLayoutLoginX);

			ISharedPreferences preferences = Application.Context.GetSharedPreferences("PREFS", FileCreationMode.Private);
			string l = preferences.GetString ("Name", "<error>");
			string p = preferences.GetString ("Pass", "pro");

			whoLog.Text = "Witaj "+l;

			mlineLay.Click += keyboardOff;

			zaloguj.Click += delegate {
				if(pass.Text != ""){
				MD5 hash = MD5.Create ();
				string hashPass = GetMd5Hash (hash, pass.Text);

				if (hashPass == p) {
					var activity = new Intent (this, typeof(MainMenu));
					StartActivity (activity);
					Finish ();
				} else {
					AlertDialog.Builder adb = new AlertDialog.Builder (this).SetTitle ("Wynik logowania"); 
					AlertDialog showme = adb.Create ();
					showme.SetMessage ("Podałeś złe hasło");
					showme.SetCancelable (true);
					showme.SetButton ("OK", delegate {
					});
					showme.Show ();
				}
				}else{
					Toast info = Toast.MakeText(this, "Podaj hasło!", ToastLength.Short);
					info.Show();
				}
			};
		}

		public override void OnBackPressed()
		{
			Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
			System.Environment.Exit(0);
		}

		public void keyboardOff(Object sender, EventArgs e)
		{
			InputMethodManager inputManager = (InputMethodManager)this.GetSystemService (Activity.InputMethodService);
			inputManager.HideSoftInputFromWindow (this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
		}

		static string GetMd5Hash(MD5 md5Hash, string input)//funkcja z msdn sluzaca do zamiany ciagu na hash
		{
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			StringBuilder sBuilder = new StringBuilder();

			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			return sBuilder.ToString();
		}
	}
}

