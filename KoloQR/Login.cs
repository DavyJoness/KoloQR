using System;
using System.Text;
using System.Data;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Preferences;
using Android.Views.InputMethods;


namespace KoloQR
{
	[Activity (Label = "Login", Theme = "@android:style/Theme.Black.NoTitleBar", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]			
	public class Login : Activity
	{
		EditText login;
		EditText password;
		Button zaloguj;
		LinearLayout mlineLay;
		string passToSave;
		string loginToSave;
		string nameToSave;
		bool connectionState = true;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Login);

			login = FindViewById<EditText> (Resource.Id.editText2);
			password = FindViewById<EditText> (Resource.Id.editText1);
			zaloguj = FindViewById<Button> (Resource.Id.button1);
			mlineLay = FindViewById<LinearLayout> (Resource.Id.linLayoutLogin);

			mlineLay.Click += keyboardOff;

			zaloguj.Click += delegate {
				if(login.Text!="" && password.Text!="")
				{
					if(logIn(int.Parse(login.Text), password.Text))
					{
					if(connectionState){
					loginToSave=login.Text;

					ISharedPreferences preferences = Application.Context.GetSharedPreferences("PREFS", FileCreationMode.Private);
					ISharedPreferencesEditor editor = preferences.Edit();
					editor.PutString("Login",loginToSave);
					editor.PutString("Name",nameToSave);
					editor.PutString("Pass",passToSave);
					editor.PutBoolean("logged", true);
					editor.Apply();

					var activity = new Intent(this, typeof(MainMenu));
					StartActivity(activity);
					Finish ();
					}else{
						AlertDialog.Builder adb = new AlertDialog.Builder (this).SetTitle ("Wynik logowania"); 
						AlertDialog showme = adb.Create ();
						showme.SetMessage ("Brak połączenia z internetem lub bazą danych.");
						showme.SetCancelable (true);
						showme.SetButton("OK", delegate {});
						showme.Show ();
					}
				}else{
					AlertDialog.Builder adb = new AlertDialog.Builder (this).SetTitle ("Wynik logowania"); 
					AlertDialog showme = adb.Create ();
					showme.SetMessage ("Podałeś złe hasło");
					showme.SetCancelable (true);
					showme.SetButton("OK", delegate {});
					showme.Show ();
				}
				}else{
					Toast info = Toast.MakeText(this, "Wypełnij wszystkie pola.", ToastLength.Short);
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

		public bool logIn(int login, string pass)
		{
			MD5 hash = MD5.Create();
			string hashPass = GetMd5Hash(hash, pass);
			try{
			MySqlConnection con = new MySqlConnection("server="+mySQLconnector.server+";Port="+mySQLconnector.port+";User ID="+mySQLconnector.id+";password="+mySQLconnector.password+";database="+mySQLconnector.database+";");
			var cos = con.State;
				//if(cos == System.Data.ConnectionState.Closed)
			
			MySqlCommand cmd = new MySqlCommand("SELECT nr_index, pass, imie FROM konto_student WHERE nr_index=" + login);
			cmd.Connection = con;

			con.Open();
			connectionState = true;
			MySqlDataReader reader = cmd.ExecuteReader();

			if (reader.Read() != false)
			{
				string dbPass = (string)reader[1];
				if (hashPass == dbPass)
				{
					passToSave = dbPass;
					nameToSave = (string)reader[2];
					cmd.Connection.Close();
					reader.Dispose();
					cmd.Dispose();
					return true;
				}
				else
				{
					cmd.Connection.Close();
					reader.Dispose();
					cmd.Dispose();
					return false;
				}
			}
			else 
			{
				cmd.Connection.Close();
				reader.Dispose();
				cmd.Dispose();
				return false;
			}
			}
			catch(Exception e){
				connectionState = false;
				return true;

			}
		}
	}
}

