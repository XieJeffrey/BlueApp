using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class IosMethodManager
{
#if UNITY_IOS
	[DllImport("__Internal")]
	/// <summary>
	/// 调用ios原生时间控件
	/// </summary>
	public static extern void CallIosDataTime ();


	[DllImport("__Internal")]
	/// <summary>
	/// 隐藏ios原生时间控件
	/// </summary>
	public static extern void HideIosDataTime ();

	[DllImport("__Internal")]
	/// <summary>
	/// 显示ios原生时间控件
	/// </summary>
	public static extern void ShowIosDataTime ();


	[DllImport("__Internal")]
	/// <summary>
	/// 打开相册
	/// </summary>
	/// <param name="_typeName">Type name.</param>
	public static extern void CallLocalPhoto (string _typeName);

	[DllImport("__Internal")]
	/// <summary>
	/// 打开摄像机
	/// </summary>
	/// <param name="_typeName">Type name.</param>
	public static extern void CallCameraPhoto (string _typeName);

	[DllImport("__Internal")]
	/// <summary>
	/// 拨打电话
	/// </summary>
	/// <param name="phoneNum">Phone number.</param>
	public static extern void CallTelePhone (string phoneNum);

	[DllImport("__Internal")]
	/// <summary>
	/// 复制文本进粘贴板
	/// </summary>
	/// <param name="textStr">Text string.</param>
	public static extern void CopyText (string textStr);

    [DllImport("__Internal")]
    /// <summary>
    /// 打开其他APP
    /// </summary>
    /// <param name="phoneNum">Phone number.</param>
    public static extern void CallOtherApp();
#endif
}
