using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFrame;
using XMWorkspace;
namespace GD
{
    public class SelectPanel : MonoBehaviour
    {
        /// <summary>
        /// 登陆按钮
        /// </summary>
        private UIEventListener login;
      
        /// <summary>
        /// 注册按钮
        /// </summary>
        private UIEventListener signin;
       // /// <summary>
       // /// 登陆界面
       // /// </summary>
       // private GameObject loginPanel;
       ///// <summary>
       ///// 注册界面
       ///// </summary>
       // private GameObject signinPanel;
        private void Start()
        {
            login = transform.Find("btn_login").GetComponent<UIEventListener>();
            signin = transform.Find("btn_signin").GetComponent<UIEventListener>();
           // loginPanel = transform.parent.Find("p_login").gameObject;
           // signinPanel = transform.parent.Find("p_signin").gameObject;
            login.onClick=delegate {
                // loginPanel.SetActive(true);
                UIManager.instance.GoToLoginPanel(gameObject);
            };

            signin.onClick=delegate {
                // signinPanel.SetActive(true);
                UIManager.instance.GoToZhuCePanel(gameObject);
            };
        }

    }
}

