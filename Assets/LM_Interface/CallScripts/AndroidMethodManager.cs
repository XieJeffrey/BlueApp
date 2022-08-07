using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidMethodManager
{

    public static void CopyText(string str)
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CopyForAndroid", str);
    }

    public static void CallAndroidDataTime()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("showDatePickerDialog");
    }

    public static void CallLocalPhoto(string _type)
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CallLocalPhoto",_type);
    }

    public static void CallCameraPhoto(string _type)
    {
        //Debug.Log("hahaha");
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CallCameraPhoto", _type);
    }

    public static void CallOtherApp()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("CallOtherApp");
    }
}
