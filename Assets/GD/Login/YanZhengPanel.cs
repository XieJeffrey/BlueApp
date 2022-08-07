using System;
using System.Collections;
using System.Collections.Generic;
using LM_Workspace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XMWorkspace;
namespace GD
{

    /// <summary>
    /// 短信验证登陆
    /// </summary>
    public class YanZhengPanel : MonoBehaviour
    {

        /// <summary>
        /// 登陆按钮
        /// </summary>
        private UIEventListener login;

        /// <summary>
        /// 验证码获取
        /// </summary>
        private UIEventListener getYanZhengMa;
        /// <summary>
        /// 手机号码
        /// </summary>
        private InputField phoneNum;
        /// <summary>
        /// 密码
        /// </summary>
        private InputField yanZhengNum;
        /// <summary>
        ///回退按钮
        /// </summary>
        private UIEventListener backButton;

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
       
        /// <summary>
        /// 用户类型选择界面
        /// </summary>
        // private GameObject userTypeSelectPanel;
        private void Start()
        {
            backButton = UtilManager.getInstance.loginGo_yanzhengPanel.transform.Find("backButton").GetComponent<UIEventListener>();
            backButton.onClick += GoBack;

            phoneNum = transform.Find("内容区").Find("input_phoneNumber").GetComponent<InputField>();
            yanZhengNum = transform.Find("内容区").Find("input_yanzhengNumber").GetComponent<InputField>();

            login = transform.Find("btn_login").GetComponent<UIEventListener>();
            getYanZhengMa = yanZhengNum.transform.Find("btn_yanzhengma").GetComponent<UIEventListener>();

            //  userTypeSelectPanel = transform.parent.Find("p_userTypeSelect").gameObject;
            getYanZhengMa.onClick = GetYanZhengMa;
            login.onClick = Login;
            IsLock = false;
            lastTime =Time.time;
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetSmsCode, OnGetSmsCode);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.Login, OnLogin);
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
                }
            }


        }

        private void OnLogin(object[] data)
        {
            ResetData();
            if ((bool)data[0])
            {
                if (UserData.instance.role == 0)
                {
                    UIManager.instance.GoToRoleSelectPanel(gameObject);

                }
                else if (UserData.instance.area == 0)
                {
                    UIManager.instance.GoToAreaSelectPanel(gameObject);
                }
                else
                {
                    UIManager.instance.OpenMainPanel(gameObject);

                }
            }
            else
            {
                IsLock = false;
                lastTime = Time.time;
                UIManager.instance.GoToSelectPanel(gameObject);

            }
        }
        private void ResetData()
        {
            phoneNum.text = "";
            yanZhengNum.text = "";


        }
        /// <summary>
        /// 验证码登陆
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void Login(GameObject go, PointerEventData data)
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().Login(2, phoneNum.text, "", yanZhengNum.text);
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
                GameObject.Find("Camera").GetComponent<InterfaceManager>().getSmsCode(phoneNum.text, 2);
            }
         


        }

        private void GoBack(GameObject go, PointerEventData data)
        {
            IsLock = false;
            lastTime = Time.time;
            UtilManager.getInstance.loginGo_selectPanel.SetActive(true);
            this.gameObject.SetActive(false);


        }
    }
}
