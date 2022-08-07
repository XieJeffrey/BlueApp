using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 修改密码界面
    /// </summary>
    public class XiuGaiMimaPanel : Panel
    {
        /// <summary>
        /// 返回按钮
        /// </summary>
        private UIEventListener back;
        /// <summary>
        /// 确认修改
        /// </summary>
        private UIEventListener enter;
        /// <summary>
        /// 获取验证码
        /// </summary>
        private UIEventListener getYanZhengMa;
        /// <summary>
        /// 手机号
        /// </summary>
        private InputField phoneNum;
        /// <summary>
        /// 验证码
        /// </summary>
        private InputField yanZhengMa;
        /// <summary>
        /// 密码
        /// </summary>
        private InputField password1;
        /// <summary>
        /// 确认密码
        /// </summary>
        private InputField password2;

        private void Start()
        {

            back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
            enter = transform.Find("内容区").Find("btn_login").GetComponent<UIEventListener>();
            phoneNum = transform.Find("内容区").Find("input_phoneNumber").GetComponent<InputField>();
            yanZhengMa = transform.Find("内容区").Find("input_yanzhengNumber").GetComponent<InputField>();
            password1 = transform.Find("内容区").Find("input_passwordNumber").GetComponent<InputField>();
            password2 = transform.Find("内容区").Find("input_passwordNumber1").GetComponent<InputField>();
            getYanZhengMa = yanZhengMa.transform.Find("btn_yanzhengma").GetComponent<UIEventListener>();

            back.onClick = delegate { Hide(); };
            enter.onClick = XiuGaiMiMa;
            IsLock = false;
            lastTime = Time.time;
            getYanZhengMa.onClick = GetYanZhengMa;
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetSmsCode, OnGetSmsCode);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.ModifyPassword, OnXiuGaiMiMa);

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="obj"></param>
        private void OnXiuGaiMiMa(object[] obj)
        {
            if ((bool)obj[0])
            {
                UIManager.instance.ShowLoggerMsg("修改密码成功","提示", Hide);
               // Hide();
            }else
            {
                UIManager.instance.ShowLoggerMsg("修改密码失败");
            }
        }

        float lastTime = 0;
        bool isLock;
        private bool IsLock
        {
            get { return isLock; }
            set
            {
                isLock = value;
                if (isLock)
                {
                    getYanZhengMa.GetComponent<SVGImporter.SVGImage>().color = new Color32(200, 200, 200, 255);

                }
                else
                {
                    getYanZhengMa.GetComponent<SVGImporter.SVGImage>().color = new Color32(38, 149, 159, 255);
                }

            }

        }
        private void OnEnable()
        {
            if (getYanZhengMa)
            {
                IsLock = false;
                lastTime = Time.time;
            }
        }
        private void Update()
        {
            if (IsLock)
            {

                if (Time.time - lastTime > 60)
                {
                    IsLock = false;
                    lastTime = Time.time;
                }
            }



        }
        private void OnGetSmsCode(object[] obj)
        {
            if (gameObject.activeSelf)
            {
                if (!(bool)obj[0])
                {
                    IsLock = false;
                    lastTime = Time.time;
                }

            }
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GetYanZhengMa(GameObject go, PointerEventData data)
        {

            if (phoneNum.text.Length != 11)
            {
                UIManager.instance.ShowLoggerMsg("请输入有效的手机号");
                return;
            }
            if (!IsLock)
            {
                IsLock = true;
                lastTime = Time.time;
                GameObject.Find("Camera").GetComponent<InterfaceManager>().getSmsCode(phoneNum.text,3);
            }



        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void XiuGaiMiMa(GameObject go, PointerEventData data)
        {
            if (password1.text != password2.text)
            {
                UIManager.instance.ShowLoggerMsg("两次输入的密码不相同");

            }
            else if (password1.text.Length < 6)
            {
                UIManager.instance.ShowLoggerMsg("新密码少于6位");
            }

            else
            {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().ModifyPassword(phoneNum.text, yanZhengMa.text, password1.text);
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
        }


       
    }



}