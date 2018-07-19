using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

	public class mySQLconnector
	{
		public static string server="db4free.net";
		public static string port="3306";
		public static string id="kielpol";
		public static string password="kielpol";
		public static string database="mattykielpoldb";
	}

public class decryptQR
{
	public static string iv = "45287112549354892144548565456541";
	public static string key = "anjueolkdiwpoida";

	public String DecryptRJ256(byte[] cypher, string KeyString, string IVString)
	{
		var ciagZnakow = "";

		var encoding = new UTF8Encoding();
		var Key = encoding.GetBytes(KeyString);
		var IV = encoding.GetBytes(IVString);

		using (var rj = new RijndaelManaged())
		{
			try
			{
				rj.Padding = PaddingMode.PKCS7;
				rj.Mode = CipherMode.CBC;
				rj.KeySize = 256;
				rj.BlockSize = 256;
				rj.Key = Key;
				rj.IV = IV;
				var ms = new MemoryStream(cypher);
				
				using (var cs = new CryptoStream(ms, rj.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
				{
					using (var sr = new StreamReader(cs))
					{
						ciagZnakow = sr.ReadLine();
					}
				}
			}
			finally
			{
				rj.Clear();
			}
		}
		return ciagZnakow;
	}

}
