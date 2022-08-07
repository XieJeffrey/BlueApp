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
    public class LoginPanel : MonoBehaviour
    {

        /// <summary>
        /// 手机号码
        /// </summary>
        private InputField phoneNum;
        /// <summary>
        /// 密码
        /// </summary>
        private InputField password;
        /// <summary>
        /// 登陆按钮
        /// </summary>
        private UIEventListener login;

        /// <summary>
        /// 验证码登陆
        /// </summary>
        private UIEventListener yanZhengMa;
        /// <summary>
        /// 验证码界面
        /// </summary>
      //  private GameObject yanZhengMaPanel;
        /// <summary>
        /// 用户类型选择界面
        /// </summary>
        // private GameObject userTypeSelectPanel;
        /// <summary>
        ///回退按钮
        /// </summary>
        private UIEventListener backButton;
        void Start()
        {
            backButton = UtilManager.getInstance.loginGo_loginPanel.transform.Find("backButton").GetComponent<UIEventListener>();
            backButton.onClick += GoBack;

            phoneNum = transform.Find("内容区").Find("input_phoneNumber").GetComponent<InputField>();
            password = transform.Find("内容区").Find("input_passwordNumber").GetComponent<InputField>();

            login = transform.Find("btn_login").GetComponent<UIEventListener>();
            yanZhengMa = transform.Find("btn_yanzhengma").GetComponent<UIEventListener>();

           // yanZhengMaPanel = transform.parent.Find("p_yanzheng").gameObject;
            // userTypeSelectPanel= transform.parent.Find("p_userTypeSelect").gameObject;

            login.onClick = Login;
            yanZhengMa.onClick = YanZhengLogin;
            EventManager.instance.RegisterEvent(XMWorkspace.Event.Login, OnLogin);
        }
        private void YanZhengLogin(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToYanZhengLoginPanel(gameObject);
           // yanZhengMaPanel.SetActive(true);
        }

        private void Login(GameObject go, PointerEventData data)
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().Login(1, phoneNum.text, password.text);

        }
        /// <summary>
        /// 判断登陆结果
        /// </summary>
        /// <param name="data"></param>
        void OnLogin(object[] data)
        {
            phoneNum.text = "";
            password.text = "";
            if ((bool)data[0])
            {
                //  userTypeSelectPanel.SetActive(true);
                if (UserData.instance.role == 0)
                {
                    UIManager.instance.GoToRoleSelectPanel(gameObject);

                }else if ( UserData.instance.area==0)
                {
                    UIManager.instance.GoToAreaSelectPanel(gameObject);
                }else
                {
                    UIManager.instance.OpenMainPanel(gameObject);

                }
            }
            else
            {
                UIManager.instance.ShowLoggerMsg("登录失败,账号或密码错误", "提示", delegate { UIManager.instance.GoToSelectPanel(gameObject); });
                
            }
        }

        private void GoBack(GameObject go, PointerEventData data)
        {
            UtilManager.getInstance.loginGo_selectPanel.SetActive(true);
            this.gameObject.SetActive(false);


        }
    }
}

