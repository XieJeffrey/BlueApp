using LitJson;
using LM_Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;
using DG.Tweening;

namespace GD
{
    /// <summary>
    /// 编辑帖子界面
    /// </summary>
    public class ForumEditorPanel : Panel
    {
        /// <summary>
        /// 返回按钮
        /// </summary>
        private UIEventListener back;

        ///// <summary>
        ///// 帖子的标题
        ///// </summary>
        //private InputField title;
        /// <summary>
        /// 帖子的内容
        /// </summary>
        private InputField context;

        /// <summary>
        /// 上传图片按钮
        /// </summary>
        private UIEventListener imageBtn1;
        private UIEventListener imageBtn2;
        private UIEventListener imageBtn3;
        private UIEventListener imageBtn4;
        private UIEventListener imageBtn5;
        private UIEventListener imageBtn6;
        private Transform imageParent;
        /// <summary>
        /// 上传按钮
        /// </summary>
        private UIEventListener shangChuan;

        /// <summary>
        /// 版块选择按钮
        /// </summary>
        private UIEventListener dongTai;
        private UIEventListener gongYi;
        private UIEventListener gongYou;
        private UIEventListener ziXun;
        /// <summary>
        /// 0 未选择
        /// 1 动态分享
        /// 2 工艺展示
        /// 3 寻找工友 
        /// 4 问题咨询
        /// </summary>
        private int currentType=0;

        /// <summary>
        /// 发布类型选择提醒
        /// </summary>
        private Transform fabuAlert;

        public override void Hide()
        {
            gameObject.SetActive(false);
            UIManager.instance.RefrehImageCache();
            UIManager.instance.BackToLastPanel();
        }

        private void Start()
        {
            EventManager.instance.RegisterEvent(XMWorkspace.Event.PostImagePlugin, OnLocalPhoto);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.CreateForum, OnFaBuTieZi);
            back = transform.Find("topbar").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = delegate { Hide(); };

            dongTai = transform.Find("bottom").Find("typeButtons").Find("btn_dongtai").GetComponent<UIEventListener>();
            gongYi = transform.Find("bottom").Find("typeButtons").Find("btn_gongyi").GetComponent<UIEventListener>();
            gongYou = transform.Find("bottom").Find("typeButtons").Find("btn_xunzhao").GetComponent<UIEventListener>();
            ziXun = transform.Find("bottom").Find("typeButtons").Find("btn_wenti").GetComponent<UIEventListener>();
            shangChuan = transform.Find("bottom").Find("btn_signin").GetComponent<UIEventListener>();
            fabuAlert = transform.Find("bottom").Find("alertType");
            if (imageParent == null)
            {
                imageParent = transform.Find("center").Find("imageArea").transform;
            }

            for(int i=0;i< imageParent.childCount; i++)
            {
                int index = i;
                imageParent.GetChild(i).GetComponent<UIEventListener>().onClick = delegate { PosImage(index); };

            }

            //imageBtn1 = transform.Find("center").Find("imageArea").Find("imageItem1").GetComponent<UIEventListener>();
            //imageBtn2 = transform.Find("center").Find("imageArea").Find("imageItem2").GetComponent<UIEventListener>();
            //imageBtn3 = transform.Find("center").Find("imageArea").Find("imageItem3").GetComponent<UIEventListener>();
            //imageBtn4 = transform.Find("center").Find("imageArea").Find("imageItem4").GetComponent<UIEventListener>();
            //imageBtn5 = transform.Find("center").Find("imageArea").Find("imageItem5").GetComponent<UIEventListener>();
            //imageBtn6 = transform.Find("center").Find("imageArea").Find("imageItem6").GetComponent<UIEventListener>();
            

            dongTai.onClick = delegate { UpdateTypeState(1); };
            gongYi.onClick = delegate { UpdateTypeState(2); };
            gongYou.onClick = delegate { UpdateTypeState(3); };
            ziXun.onClick = delegate { UpdateTypeState(4); };
            shangChuan.onClick = delegate { FaBuTieZi(); };

        }

        private void OnLocalPhoto(object[] obj)
        {
            if (gameObject.activeSelf)
            {
                string typeName = obj[0].ToString();
                string path = obj[1].ToString();
                PosImage(typeName, path);
            }
        }

        private void OnFaBuTieZi(object[] obj)
        {
            if ((bool)obj[0])
            {
                UIManager.instance.ShowLoggerMsg("帖子发布成功","提示",Hide);
                // Hide();
            }else
            {
                UIManager.instance.ShowLoggerMsg("帖子发布失败，请检查帖子详情");

            }
           
        }




        /// <summary>
        /// 发布帖子
        /// </summary>
        private void FaBuTieZi()
        {
            if (currentType != 0)
                GameObject.Find("Camera").GetComponent<InterfaceManager>().CreateForum(currentType.ToString(), "", context.text, url);
            else
            {
                ChangeFbAlert(true);
                return;
            }
        }

        void GetCompent()
        {
            if (imageParent == null)
            {
                imageParent = transform.Find("center").Find("imageArea").transform;
            }
            for (int i = 0; i < imageParent.childCount; i++)
            {
                int index = i;
                imageParent.GetChild(i).GetComponent<RawImage>().texture = null;
                imageParent.GetChild(i).Find("icon").gameObject.SetActive(true);
            }

            //if (title == null)
            //    title = transform.Find("center").Find("titleArea").GetComponent<InputField>();
            //title.text = "";
            if (context == null)
                context = transform.Find("center").Find("subArea").GetComponent<InputField>();
            context.text = "";
            url = new string[6];
            currentIndex = -1;

        }
        public override void Show()
        {
            gameObject.SetActive(true);
            GetCompent();
            UpdateTypeState(0);


        }
        private Color32 normalColor=new Color32(194,203,207,255);
        private Color32 selectColor=new Color32(38,149,159,255);

        void ChangeFbAlert(bool _show)
        {
            float speed = 0.5f;
            if(_show)
            {
                fabuAlert.DOPause();
                fabuAlert.DOScale(Vector3.one, speed);
                fabuAlert.GetComponent<Image>().DOFade(1, speed);
            }
            else
            {
                fabuAlert.DOPause();
                fabuAlert.DOScale(Vector3.zero, speed);
                fabuAlert.GetComponent<Image>().DOFade(0, speed);
            }
        }

        /// <summary>
        /// 选择上传类型
        /// </summary>
        /// <param name="type"></param>
        void UpdateTypeState(int type)
        {
            if (currentType == type)
            {
                currentType = 0;

            }
            else
            {
                currentType = type;
            }
            
            if (dongTai)
            {
                dongTai.GetComponent<SVGImporter.SVGImage>().color = normalColor;
                gongYi.GetComponent<SVGImporter.SVGImage>().color = normalColor;
                gongYou.GetComponent<SVGImporter.SVGImage>().color = normalColor;
                ziXun.GetComponent<SVGImporter.SVGImage>().color = normalColor;
                switch (currentType)
                {
                    case 1:
                        dongTai.GetComponent<SVGImporter.SVGImage>().color = selectColor;
                        ChangeFbAlert(false);
                        break;
                    case 2:
                        gongYi.GetComponent<SVGImporter.SVGImage>().color = selectColor;
                        ChangeFbAlert(false);
                        break;
                    case 3:
                        gongYou.GetComponent<SVGImporter.SVGImage>().color = selectColor;
                        ChangeFbAlert(false);
                        break;
                    case 4:
                        ziXun.GetComponent<SVGImporter.SVGImage>().color = selectColor;
                        ChangeFbAlert(false);
                        break;
                }
            }
           
           
        }
        /// <summary>
        /// 地址数组
        /// </summary>
        string[] url = new string[6];
        int currentIndex=-1;
        int clickIndex;
        RawImage currentImage;
        /// <summary>
        /// 上传头像
        /// </summary>
        void PosImage(int index)
        {

           // clickIndex = index;
#if UNITY_EDITOR
            PosImage(index.ToString(), "头像.png");
#else
             PostImageManager.getInstance.PostImage (1, index.ToString());
#endif
            //   StartCoroutine(GameObject.Find("Camera").GetComponent<InterfaceManager>().PostImage("头像.png", "测试图片", "tiezi", "image/jpg", OnLocalPhotoJson));


        }
        void PosImage(string _typeName, string _path)
        {

            //   StartCoroutine(GameObject.Find("Camera").GetComponent<InterfaceManager>().PostImage("头像.png", "测试图片", typeName, "image/jpg", OnLocalPhotoJson));
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
#if UNITY_EDITOR
                Util.ShowErrorMessage(status);
                Debug.LogError("OnLocalPhotoJson >>>>error status:" + status);
#endif
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            UIManager.instance.LoadImageFromUrl(result["url"].ToString(), 300, 500, delegate {

                clickIndex=int.Parse( result["param"].ToString());
                if (clickIndex <= currentIndex)
                {
                    currentImage = imageParent.GetChild(clickIndex).GetComponent<RawImage>();
                    url[clickIndex] = result["url"].ToString();
                }
                else
                {
                    currentIndex++;
                    currentImage = imageParent.GetChild(currentIndex).GetComponent<RawImage>();
                    currentImage.transform.Find("icon").gameObject.SetActive(false);
                    url[currentIndex] = result["url"].ToString();
                }
                return currentImage;
                // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            });
            ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
            //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(result["url"].ToString(), 300, 500, delegate {

            //    if (clickIndex <= currentIndex)
            //    {
            //        currentImage = imageParent.GetChild(clickIndex).GetComponent<RawImage>();
            //        url[clickIndex] = result["url"].ToString();
            //    }
            //    else
            //    {
            //        currentIndex++;
            //        currentImage = imageParent.GetChild(currentIndex).GetComponent<RawImage>();
            //        currentImage.transform.Find("icon").gameObject.SetActive(false);
            //        url[currentIndex] = result["url"].ToString();
            //    }
            //    return currentImage;
            //    // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            //}));

        }


    }

}
