using LM_Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;
using UnityEngine.EventSystems;

namespace GD
{


    public class GongRenMainPanel : Panel, IRefresh
    {

        /// <summary>
        /// 用户名称
        /// </summary>
        private Text userName;
       

        private RawImage touXiang;


        private Transform level;

        private Text levelmsg;
        private Text lanBi;

        private Text tianShu;
        private UIEventListener qianDao;
        private UIEventListener renwu;
        void Start()
        {
            qianDao = transform.Find("right").Find("icon").GetComponent<UIEventListener>();
            qianDao.onClick = QianDao;
            renwu = transform.Find("left").Find("icon").GetComponent<UIEventListener>();
            renwu.onClick = ToRenWuPanel;

            EventManager.instance.RegisterEvent(XMWorkspace.Event.SetSign, OnQianDao);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetBaseInfo, OnGetBaseInfo);
          //  EventManager.instance.RegisterEvent(XMWorkspace.Event.PostImageJson, OnHeaderChange);
        }

        /// <summary>
        /// 加载头像
        /// </summary>
        void LoadHeaderImage()
        {
            bool checkType;
            UIManager.instance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, out checkType, delegate
            {
                touXiang.gameObject.SetActive(true);
                return touXiang;
            });
            if (!checkType)
            {
                touXiang.gameObject.SetActive(false);
            }
        }

        private void OnHeaderChange(object[] obj)
        {
            if (!gameObject.activeSelf) return;
            LoadHeaderImage();
            //if (UserData.instance.avatar == "")
            //{
            //    touXiang.gameObject.SetActive(false);
            //}
            //else
            //{
            //    UIManager.instance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, delegate
            //    {

            //        touXiang.gameObject.SetActive(true);
            //        return touXiang;
            //    });
            //}
        }

        private void OnGetBaseInfo(object[] obj)
        {
            if (!gameObject.activeSelf) return;
            if ((bool)obj[0])
            {
                UpdateUserData();
            }
        }

        /// <summary>
        /// 签到结果返回
        /// </summary>
        /// <param name="obj"></param>
        private void OnQianDao(object[] obj)
        {
           // if (!gameObject.activeSelf) return;
            if ((bool)obj[0])
            {
                UIManager.instance.ShowLoggerMsg("签到成功");
                GameObject.Find("Camera").GetComponent<InterfaceManager>().GetBaseInfo();
            }else
            {


            }

        }

        private void QianDao(GameObject go, PointerEventData data)
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().SetSign();

        }

        /// <summary>
        /// 打开任务界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void ToRenWuPanel(GameObject go, PointerEventData data)
        {

            UtilManager.getInstance.signInGo.SetActive(true);
        }

      
        public override void Hide()
        {

            gameObject.SetActive(false);

        }
        public override void Show()
        {
            gameObject.SetActive(true);
            UpdateUserData();
        }

        private void UpdateUserData()
        {
            if (userName == null)
            {
                userName = transform.Find("userName").GetComponent<Text>();
            }
            string role = "";
            switch (UserData.instance.role) {
                case 1:role = "经销商";
                    break;
                case 2:
                    role = "水工";
                    break;
                case 3:
                    role = "试压员";
                    break;
            }
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAreaDetail(UserData.instance.area, (string area) => {
                userName.text = string.Format("{0}-{1}-{2}", UserData.instance.name, area, role); 
            });
           

            if (touXiang == null)
            {
                touXiang = transform.Find("userIcon").Find("RawImage").GetComponent<RawImage>();
            }
            LoadHeaderImage();
            //if (UserData.instance.avatar == "")
            //{
            //    touXiang.gameObject.SetActive(false);
            //}
            //else
            //{
            //    UIManager.instance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, delegate {

            //        touXiang.gameObject.SetActive(true);
            //        return touXiang;
            //    });
            //    //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, delegate {

            //    //    touXiang.gameObject.SetActive(true);
            //    //    return touXiang;
            //    //}));
            //}

            if(lanBi == null)
            {
                lanBi = transform.Find("left").Find("lanbi_label").GetComponent<Text>();
            }
            lanBi.text = UserData.instance.coin.ToString();

            if (tianShu == null)
            {
                tianShu = transform.Find("right").Find("lanbi_label").GetComponent<Text>();
            }
            tianShu.text = UserData.instance.totalSign+"天";
            if (levelmsg == null)
            {
                levelmsg= transform.Find("等级").Find("文本").GetComponent<Text>();
            }
            if (level == null)
            {
                level= transform.Find("等级").Find("图标").transform;
            }
            for (int i = 0; i < level.childCount; i++)
            {
                level.GetChild(i).gameObject.SetActive(false);

            }
           // Debug.Log(UserData.instance.level + "level");
           
            level.GetChild(UserData.instance.level).gameObject.SetActive(true);
            levelmsg.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            
            switch (UserData.instance.level)
            {
                case 0:

                    levelmsg.text = "新手";
                    break;

                case 1:
                    levelmsg.text = "工匠";

                    break;
                case 2:
                    levelmsg.text = "巧匠";

                    break;
                case 3:
                    levelmsg.text = "鲁班";

                    break;
                case 4: levelmsg.text = "大师";
                    break;
                case 5:
                    levelmsg.text = "中财权威认证";
                    levelmsg.GetComponent<RectTransform>().offsetMax = new Vector2(70, 0);
                    break;
            }


        }

       
        public void Reload()
        {
            this.Show();
        }

        public void ClearCache()
        {
            gameObject.SetActive(false);
            touXiang.texture = null;
        }
    }
}
