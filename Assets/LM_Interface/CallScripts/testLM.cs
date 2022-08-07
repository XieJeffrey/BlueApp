using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using LM_Workspace;
using LitJson;

namespace XMWorkspace
{
    public class testLM : MonoBehaviour {
        public UIEventListener registerBtn;
        public UIEventListener loginBtn;

		public UIEventListener localPhotoBtn;
		public UIEventListener cameraPhotoBtn;

		public UIEventListener callTelePhoneBtn;

		public UIEventListener copyTextBtn;

		public RawImage testImage;

		GameObject messageGo;


        // Use this for initialization
        void Start () {
			//UserData.instance.token = "mwpVJ5K0tjojPkaL";
//			messageGo = GameObject.Find ("Message_Alert");
//			messageGo.SetActive (false);

         //   Debug.Log((System.DateTime.Now.Ticks / 10000000).ToString());
           // registerBtn.onClick = register;
           // loginBtn.onClick = login;

			localPhotoBtn.onClick = LocalPhoto;
			cameraPhotoBtn.onClick = CameraPhoto;

			callTelePhoneBtn.onClick = CallTelePhone;
			copyTextBtn.onClick = CopyText;

            EventManager.instance.RegisterEvent(Event.Login, OnLogin);
			EventManager.instance.RegisterEvent(Event.PostImagePlugin, OnLocalPhoto);
        }
	
	    // Update is called once per frame
	    void Update () {
//			if(Input.GetKeyDown(KeyCode.Space))
//				Util.ShowAlert (messageGo, "这是标题", "这是内容");
	    }

        void register(GameObject go, PointerEventData eventdata)
        {
            Debug.Log("click regist");    
            GameObject.Find("Camera").GetComponent<InterfaceManager>().Register("11223344556","8888","123456","maple");
        }

        void login(GameObject go, PointerEventData eventdata)
        {
            System.DateTime start = new System.DateTime(1970, 1, 1, 0, 0, 0);
            Debug.Log(Util.GetTimeStamp());
            
            return;
            Debug.Log("click login");
            GameObject.Find("Camera").GetComponent<InterfaceManager>().Login(1, "11223344556", "123456", "8888");
        }
			
		void LocalPhoto(GameObject go, PointerEventData eventdata)
		{
			PostImageManager.getInstance.PostImage (1, "图片类型1");
		}

		void CameraPhoto(GameObject go, PointerEventData eventdata)
		{
			PostImageManager.getInstance.PostImage (2, "图片类型2");
		}

		void CallTelePhone(GameObject go, PointerEventData eventdata)
		{
			string phoneNum = "15205183216";
#if UNITY_IOS
			IosMethodManager.CallTelePhone (phoneNum);
#endif
		}

		void CopyText(GameObject go, PointerEventData eventdata)
		{
			string str = "这是复制内容";
#if UNITY_IOS
			IosMethodManager.CopyText (str);
#endif
		}

		void OnLocalPhoto(object[] data)
		{
			Debug.Log ("事件注册" + data [1]);
			string typeName = data [0].ToString();
			string path = data [1].ToString();
			//UserData.instance.token = "mwpVJ5K0tjojPkaL";
			StartCoroutine(GameObject.Find ("Camera").GetComponent<InterfaceManager> ().PostImage (path, "测试图片",typeName, "image/jpg", OnLocalPhotoJson));
		}

		/// <summary>
		/// photo返回结果
		/// </summary>
		/// <param name="result"></param>
		private void OnLocalPhotoJson(JsonData result)
		{
			int status = int.Parse(result["status"].ToString());
			if (status != 1)
			{
				EventManager.instance.NotifyEvent(Event.PostImageJson, false);
				Util.ShowErrorMessage(status);
				Debug.LogError("OnLocalPhotoJson >>>>error status:" + status);
				return;
			}
			result = result["data"];
			if (result == null)
				return;
			Debug.Log (result ["param"].ToString ());
			Debug.Log (result ["url"].ToString ());

			//实际情况要用param作为图片类型的标识符进行区分情况加载图片
			StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(result ["url"].ToString (),300,500,delegate {
				return testImage;
				EventManager.instance.NotifyEvent(Event.PostImageJson, "已完成图片加载功能!");
			}));

		}

        void OnLogin(object[] data)
        {
            Debug.Log("login result");
            Debug.Log(data[0]);
        }
    }
}
