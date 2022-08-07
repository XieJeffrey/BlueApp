using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XMWorkspace;
using LM_Workspace;

namespace GD
{
    /// <summary>
    /// 设置界面
    /// </summary>
    public class SheZhiPanel : Panel,IRefresh
    {

        /// <summary>
        /// 返回
        /// </summary>
        private UIEventListener back;
        /// <summary>
        /// 头像
        /// </summary>
        private UIEventListener header;
        /// <summary>
        /// 用户名
        /// </summary>
        private Text userName;
        /// <summary>
        /// 修改密码
        /// </summary>
        private UIEventListener miMa;
        /// <summary>
        /// 修改个人信息
        /// </summary>
        private UIEventListener modifyInfo;
        /// <summary>
        /// 退出登录
        /// </summary>
        private UIEventListener exit;

        /// <summary>
        /// 中财纯净蓝
        /// </summary>
        private UIEventListener chunJingLan;

        

        void OnEnable()
        {
           
            //if (back == null)
            {
               
                back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
                header = transform.Find("内容区").Find("头像").Find("mask").Find("RawImage").GetComponent<UIEventListener>();

                miMa = transform.Find("内容区").Find("修改密码").GetComponent<UIEventListener>();
                chunJingLan= transform.Find("内容区").Find("关于").GetComponent<UIEventListener>();
                exit= transform.Find("buttonPrefab").GetComponent<UIEventListener>();
                modifyInfo= transform.Find("内容区").Find("修改信息").GetComponent<UIEventListener>();
            }

            back.onClick = delegate { Hide(); };
            exit.onClick = delegate {

                if (PlayerPrefs.HasKey("TOKEN"))
                {
                    PlayerPrefs.DeleteKey("TOKEN");
                }
              
                UIManager.instance.GoToSelectPanel(gameObject);
            };
            header.onClick = PosHeaderImage;
            miMa.onClick = SetMiMaPanel;
            chunJingLan.onClick = GoToChunJingLan;
            modifyInfo.onClick = ModifyInfo;
			EventManager.instance.RegisterEvent(XMWorkspace.Event.PostImagePlugin, OnLocalPhoto);

            InitModifyBtn();
        }

        private void InitModifyBtn() {
            modifyInfo.gameObject.SetActive(false);
            InterfaceManager instance = GameObject.Find("Camera").GetComponent<InterfaceManager>();
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/checkModifyInfo";
            StartCoroutine(instance.Post(url, (JsonData data) => {
                if (((bool)data["data"]["result"])) {
                    modifyInfo.gameObject.SetActive(true);
                }
                else
                    modifyInfo.gameObject.SetActive(false);
            }));

            //  modifyInfo.gameObject.SetActive(true);
        }

        /// <summary>
        /// 打开纯净蓝界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToChunJingLan(GameObject go, PointerEventData data)
        {
           

        }

        /// <summary>
        /// 打开修改密码界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void SetMiMaPanel(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToXiuGaiMiMa(this);
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void PosHeaderImage(GameObject go, PointerEventData data)
        {

#if UNITY_EDITOR
            PosImage("header", "头像.png");

#else
             PostImageManager.getInstance.PostImage (1, "header");
#endif
        }


        private void ModifyInfo(GameObject go, PointerEventData data) {

            UIManager.instance.GoToXiuGaiXinXi(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Hide()
        {
            ClearCache();
            UIManager.instance.BackToLastPanel();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            if (userName==null)
                userName = transform.Find("内容区").Find("用户名").Find("name").GetComponent<Text>();
            userName.text = UserData.instance.name;

            if (UserData.instance.avatar == "")
            {
                header.gameObject.SetActive(true);
                header.GetComponent<RawImage>().color=new Color(1,1,1,0);
            }else
            {
                UIManager.instance.LoadImageFromUrl(UserData.instance.avatar, 80, 80, delegate {
                    header.gameObject.SetActive(true);
                    header.GetComponent<RawImage>().color=new Color(1,1,1,1);
                    return header.GetComponent<RawImage>();
                });

            }

        }

		void OnLocalPhoto(object[] data)
		{

            if (gameObject.activeSelf)
            {
                string typeName = data[0].ToString();
                string path = data[1].ToString();
                //UserData.instance.token = "mwpVJ5K0tjojPkaL";
                //StartCoroutine(GameObject.Find ("Camera").GetComponent<InterfaceManager> ().PostImage (path, "测试图片",typeName, "image/jpg", OnLocalPhotoJson));
                PosImage(typeName, path);
            }
		}

        /// <summary>
        /// 上传头像
        /// </summary>
		void PosImage(string _typeName,string _path)
        {
           // UserData.instance.token = "d4gOkj0Pvz2QJ6mRtjpYyrmXM7lEBLW8";
           // StartCoroutine(GameObject.Find("Camera").GetComponent<InterfaceManager>().PostImage("头像.png", "测试图片", "header", "image/jpg", OnLocalPhotoJson));
			StartCoroutine(GameObject.Find("Camera").GetComponent<InterfaceManager>().PostImage(_path, "测试图片", _typeName, "image/jpg", OnLocalPhotoJson));

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
                EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, false);
              //  Util.ShowErrorMessage(status);
             //   Debug.LogError("OnLocalPhotoJson >>>>error status:" + status);
                return;
            }

            result = result["data"];
            if (result == null)
                return;
          //  Debug.Log(result["param"].ToString());
           // Debug.Log(result["url"].ToString());

            if (result["param"].ToString() == "header")
            {
                bool checkType;
                UIManager.instance.LoadImageFromUrl(result["url"].ToString(), 80, 80, out checkType, delegate {
                    EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, true);
                    header.gameObject.SetActive(true);
                    header.GetComponent<RawImage>().color=new Color(1,1,1,1);
                    return header.GetComponent<RawImage>();
                    // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
                });
                if (checkType)
                {
                    UserData.instance.avatar = result["url"].ToString();
                    GameObject.Find("Camera").GetComponent<InterfaceManager>().SetAvatar(UserData.instance.avatar);
                }
               
            }
           
            ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
            //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(result["url"].ToString(), 300, 500, delegate {

            //    return header.GetComponent<RawImage>();
            //    // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            //}));

        }

        public void Reload()
        {
            Show();
        }

        public void ClearCache()
        {
            gameObject.SetActive(false);
        }
    }
}
