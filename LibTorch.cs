using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
 
public class LibTorch : MonoBehaviour {
 
 static LibTorch _Instance;
	static LibTorch Instance {
		get {
			if(_Instance==null) {
				GameObject GO = new GameObject("_LibTorch");
				_Instance = GO.AddComponent<LibTorch>();
			}
			return _Instance;
		}
	}

	void Start() {
		_Instance = this;
	}

	#if !UNITY_EDITOR && UNITY_IPHONE

	[DllImport ("__Internal")]
	private static extern void LibTorch_TorchOn();
	[DllImport ("__Internal")]
	private static extern void LibTorch_StartTorch();

	public void TorchOn() { LibTorch_TorchOn(); }
	public void StartTorch() { LibTorch_StartTorch(); }

	#elif !UNITY_EDITOR && UNITY_ANDROID


	public static AndroidJavaClass libTorchAndroid;
	public static void InitClass()
	{
	if (libTorchAndroid == null)
	{
	libTorchAndroid = new AndroidJavaClass("com.maxvalley.libtorch.LibTorch");
	}
	}

	public void TorchOn() {
		//LibTorch_TorchOn(); 
		InitClass(); 
		libTorchAndroid.Call("torchOnOff");
	}

	public void StartTorch() {
		//LibTorch_TorchOff(); 
		InitClass(); 
		libTorchAndroid.Call("startTorch");
	}

	#else

	public static void TorchOn() { TorchOn(); }
	public static void StartTorch() { StartTorch(); }

	#endif

	
}