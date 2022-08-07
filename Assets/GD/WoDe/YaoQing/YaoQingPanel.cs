using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 邀请
    /// </summary>
    public class YaoQingPanel : Panel
    {
        protected UIEventListener back;

        private Text yaoqing;
        private bool isOn;
        float startTime;
        private void Start()
        {
            GetCompent();
            yaoqing = transform.Find("info").Find("label_yaoqing").GetComponent<Text>();
            yaoqing.GetComponent<DragItem>().OnEnter = delegate {  startTime = Time.time; isOn = true;  };
            yaoqing.GetComponent<DragItem>().OnExit = delegate {  isOn = false; };
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetInviteCode, OnGetInviteCode);
        }
        /// <summary>
        /// 给属性赋值
        /// </summary>
        protected void GetCompent()
        {
            back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = delegate {
                Hide();
            };
         
        }

        private void Update()
        {
            if (isOn)
            {
                if(Time.time- startTime > 0.5f)
                {

                  
                    isOn = false;
                    if (Application.platform == RuntimePlatform.IPhonePlayer) {
#if UNITY_IOS
                        IosMethodManager.CopyText(yaoqing.text);            
#endif
                    }
                    else
                        AndroidMethodManager.CopyText(yaoqing.text);
                   UIManager.instance.ShowLoggerMsg("复制成功");

                }
            }



        }
        /// <summary>
        /// 接口回调
        /// </summary>
        /// <param name="obj"></param>
        protected void OnGetInviteCode(object[] obj)
        {
            if ((bool)obj[0])
            {
               // gameObject.SetActive(true);
                yaoqing.text = obj[1].ToString();

            }
            else
            {


            }

        }

        public override void Hide()
        {
            gameObject.SetActive(false);

            UIManager.instance.BackToLastPanel();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            isOn = false;
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetInviteCode();
          
        }
    }
}

