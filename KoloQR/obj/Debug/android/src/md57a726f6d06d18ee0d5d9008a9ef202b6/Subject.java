package md57a726f6d06d18ee0d5d9008a9ef202b6;


public class Subject
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("KoloQR.Subject, KoloQR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Subject.class, __md_methods);
	}


	public Subject () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Subject.class)
			mono.android.TypeManager.Activate ("KoloQR.Subject, KoloQR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}