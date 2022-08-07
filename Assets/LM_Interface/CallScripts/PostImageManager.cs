using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;
using LitJson;
using System;
using UnityEngine.UI;

namespace LM_Workspace {
    public class PostImageManager : SingletonMono<PostImageManager> {

        /// <summary>
        /// Posts the image.
        /// </summary>
        /// <param name="_type">本地照片:1 拍照:2 编辑器模式:3</param>
        /// <param name="_typeName">图片标识符</param>
        public void PostImage(int _type, string _typeName) {
            if (!UtilManager.getInstance.CheckInternet())
                return;
            switch (_type) {
                case 1:
                    if (Application.platform == RuntimePlatform.IPhonePlayer) {
#if UNITY_IOS
                        IosMethodManager.CallLocalPhoto(_typeName);
#endif
                    }
                    else
                        AndroidMethodManager.CallLocalPhoto(_typeName);
                    break;
                case 2:
                    if (Application.platform == RuntimePlatform.IPhonePlayer) {
#if UNITY_IOS
						IosMethodManager.CallCameraPhoto(_typeName);
#endif
                    }
                    else
                        AndroidMethodManager.CallCameraPhoto(_typeName);
                    break;
                case 3:

                    break;
            }
        }

        /// <summary>
        /// 接收ios的反馈
        /// </summary>
        /// <param name="_path">Path.</param>
        void RequestIosPhoto(string _path) {
            //Debug.Log (_path);
            string[] str = _path.Split(':');
            LoadPhotoCoroutine(str[0], str[1]);
        }


        private void LoadPhotoCoroutine(string _path, string _typeName) {
            UtilManager.getInstance.loadingText.text = UtilManager.getInstance.LoadingNormalStr;
            UtilManager.getInstance.loadingPanel.SetActive(true);
            //				Texture2D tex2d = new Texture2D (imageWidth, imageHeight, TextureFormat.Alpha8, false);
            //				www.LoadImageIntoTexture (tex2d);
            EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImagePlugin, _typeName, _path);
            //GameObject.Find ("Camera").GetComponent<InterfaceManager> ().PostImage (_path, imageName, "image/jpg", OnPhoto);

        }

        public IEnumerator LoadImageFromUrl(string _url, int _imageWidth, int _imageHeight, Func<RawImage> callBack = null) {

            WWW www = new WWW(_url);
            yield return www;
            if (www.isDone) {
                Texture2D tex2d = new Texture2D(_imageWidth, _imageHeight, TextureFormat.Alpha8, false);
                www.LoadImageIntoTexture(tex2d);
                //  Debug.Log(callBack().name);
                callBack().texture = tex2d;
                tex2d = null;
                UtilManager.getInstance.loadingPanel.SetActive(false);
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
                //www.assetBundle.Unload (true);
                //Debug.Log (www.texture.texelSize);
            }
        }

        //		/// <summary>
        //		/// Raises the photo event.
        //		/// </summary>
        //		/// <param name="result">Result.</param>
        //		private void OnPhoto(JsonData result)
        //		{
        //			int status = int.Parse (result ["status"].ToString ());
        //			if (status == 1) 
        //			{
        //				photoImageDic [imageName] = currentT2dDic [imageName];
        //			} else 
        //			{
        //				currentT2dDic.Remove (imageName);
        //			}
        //				
        //		}

    }
}
