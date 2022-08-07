using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XMWorkspace;
using UnityEngine.UI;
using LM_Workspace;

namespace GD
{
    /// <summary>
    /// 积分界面
    /// </summary>
    public class JiFenPanel : YaoQingPanel
    {

        /// <summary>
        /// 兑换界面
        /// </summary>
        private UIEventListener duiHuan;

        /// <summary>
        /// 积分
        /// </summary>
        private Text userPoint;

        /// <summary>
        /// 说明信息按钮
        /// </summary>
        private UIEventListener infoOpenBtn;

        /// <summary>
        /// 说明信息关闭按钮
        /// </summary>
        private UIEventListener infoCloseBtn;

        /// <summary>
        /// 说明信息界面
        /// </summary>
        private GameObject infoObj;

        void Start()
        {
            GetCompent();
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetUserPoint, OnGetUserPoint);
           
            infoObj = Util.FindChildByName(gameObject, "infoBG");
            infoOpenBtn = Util.FindChildByName(gameObject, "header/infoOpenBtn").GetComponent<UIEventListener>();
            infoCloseBtn = Util.FindChildByName(infoObj, "infoImage/Btn").GetComponent<UIEventListener>();

            infoOpenBtn.onClick = InfoOpenBtnClick;
            infoCloseBtn.onClick = InfoCloseBtnClick;
        }
       

        new  void GetCompent()
        {
            
            base.GetCompent();
            duiHuan = transform.Find("btn_duihuan").GetComponent<UIEventListener>();
            duiHuan.onClick = GoToDuiHuanPanel;
            userPoint = transform.Find("text").Find("label").GetComponent<Text>();
        }
        /// <summary>
        /// 跳转至兑换界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToDuiHuanPanel(GameObject go, PointerEventData data)
        {
            UtilManager.getInstance.marketGo.SetActive(true);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetUserPoint();
           // gameObject.SetActive(true);
        }

        /// <summary>
        /// 获取积分后的回调
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetUserPoint(object[] obj)
        {
           
            if ((bool)obj[0])
            {
                Debug.Log(obj[1].ToString());
                userPoint.text = obj[1].ToString();
            }
          
            
        }

        void InfoOpenBtnClick(GameObject go, PointerEventData eventdata)
        {
            infoObj.SetActive(true);

        }
        void InfoCloseBtnClick(GameObject go, PointerEventData eventdata)
        {
            infoObj.SetActive(false);

        }
    }


}