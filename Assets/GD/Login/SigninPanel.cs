using LM_Workspace;
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
    /// 注册界面
    /// </summary>
    public class SigninPanel : MonoBehaviour
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
        /// 协议按钮
        /// </summary>
         private UIEventListener xieYiBtn;
        /// <summary>
        /// 协议文本
        /// </summary>
         private GameObject xieYiDetail;
        /// <summary>
        /// 协议文本
        /// </summary>
        private RectTransform xieYiImage;
        /// <summary>
        /// 退出协议按钮
        /// </summary>
        private UIEventListener xieyiReturn;
        /// <summary>
        /// 手机号码
        /// </summary>
        private InputField phoneNum;
        /// <summary>
        ///验证码
        /// </summary>
        private InputField yanZhengNum;
        /// <summary>
        ///密码
        /// </summary>
        private InputField passWord;
        /// <summary>
        ///姓名
        /// </summary>
        private InputField userName;
        /// <summary>
        ///邀请码
        /// </summary>
        private InputField yaoQingMa;
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

                }else
                {
                    getYanZhengMa.GetComponent<SVGImporter.SVGImage>().color = new Color32(38, 149, 159, 255);
                }

            }

        }


        ///// <summary>
        ///// 用户类型选择界面
        ///// </summary>
        //private GameObject userTypeSelectPanel;
        private void Start()
        {

            backButton = UtilManager.getInstance.loginGo_signinPanel.transform.Find("backButton").GetComponent<UIEventListener>();
            backButton.onClick += GoBack;

            phoneNum = transform.Find("内容区").Find("input_phoneNumber").GetComponent<InputField>();
            yanZhengNum = transform.Find("内容区").Find("input_yanzhengNumber").GetComponent<InputField>();
            passWord = transform.Find("内容区").Find("input_passwordNumber").GetComponent<InputField>();
            userName = transform.Find("内容区").Find("input_userName").GetComponent<InputField>();
            yaoQingMa = transform.Find("内容区").Find("input_yao").GetComponent<InputField>();
           
            xieYiDetail =transform.Find("label_detail").gameObject;
            login = transform.Find("btn_login").GetComponent<UIEventListener>();
            getYanZhengMa = yanZhengNum.transform.Find("btn_yanzhengma").GetComponent<UIEventListener>();
            xieYiBtn = transform.Find("label_info").GetComponent<UIEventListener>();
            xieYiBtn.gameObject.SetActive(true);
            xieyiReturn = xieYiDetail.transform.Find("Back").GetComponent<UIEventListener>();
            xieYiImage = xieYiDetail.transform.Find("xieyi").GetComponent<RectTransform>();
            // userTypeSelectPanel = transform.parent.Find("p_userTypeSelect").gameObject;
            xieYiBtn.onClick = delegate {
               /// Debug.Log("111");
                xieYiDetail.SetActive(!xieYiDetail.activeSelf);
            };
            xieyiReturn.onClick = delegate {
               /// Debug.Log("222");
                xieYiDetail.SetActive(!xieYiDetail.activeSelf);
                //Debug.Log(xieYiImage.anchoredPosition);
                xieYiImage.anchoredPosition = new Vector2(0, -1629);
            };
            getYanZhengMa.onClick = GetYanZhengMa;
            login.onClick = Register;
            IsLock = false;
            lastTime = Time.time;
            EventManager.instance.RegisterEvent(XMWorkspace.Event.Regist, OnRegister);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetSmsCode, OnGetSmsCode);
            
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
            if(xieYiBtn != null)
                xieYiBtn.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (xieYiBtn != null)
                xieYiBtn.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (IsLock)
            {

                if (Time.time - lastTime >60)
                {
                    IsLock = false;
                    lastTime = Time.time;
                }
            }
           


        }

        private void GoBack(GameObject go,PointerEventData data)
        {
            IsLock = false;
            lastTime = Time.time;
            UtilManager.getInstance.loginGo_selectPanel.SetActive(true);
            this.gameObject.SetActive(false);


        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void Register(GameObject go, PointerEventData data)
        {

            if (passWord.text.Length < 6)
            {
                UIManager.instance.ShowLoggerMsg("密码不能长度低于六位");
            }
            else if (userName.text == "")
            {
                UIManager.instance.ShowLoggerMsg("请输入姓名");
            }
            else if (yanZhengNum.text == "")
            {
                UIManager.instance.ShowLoggerMsg("请输入验证码");
            }
            else if (phoneNum.text .Length!=11)
            {
                UIManager.instance.ShowLoggerMsg("请输入有效的手机号");
            }
            else
            {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().Register(phoneNum.text, yanZhengNum.text, passWord.text, userName.text, yaoQingMa.text);
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
                Debug.Log(phoneNum.text);
                UIManager.instance.ShowLoggerMsg("请输入有效的手机号");
                return;
            }
            if (!IsLock)
            {
                IsLock = true;
                lastTime = Time.time;
                GameObject.Find("Camera").GetComponent<InterfaceManager>().getSmsCode(phoneNum.text, 1);
            }
        }
        /// <summary>
        /// 注册回调
        /// </summary>
        /// <param name="data"></param>
        private void OnRegister(object[] data)
        {
            if (!gameObject.activeSelf) return;
            if ((bool)data[0])
            {
                ResetData();
                UIManager.instance.GoToRoleSelectPanel(gameObject);
                // userTypeSelectPanel.SetActive(true);
            }else
            {
                if ((int)data[1] == -1)
                {
                    UIManager.instance.ShowLoggerMsg("注册失败");

                }
                else if((int)data[1] == -2)
                {
                    UIManager.instance.ShowLoggerMsg("注册失败,请检查输入格式");
                }else
                {
                    UIManager.instance.ShowLoggerMsg("验证码错误");
                }
            }
        }

        private void ResetData()
        {
            phoneNum.text = "";
            yanZhengNum.text = "";
            passWord.text = "";
            userName.text = "";
            yaoQingMa.text = "";
           // xieYiDetail.SetActive(false);
            //lastTime = Time.time;
            //IsLock = false;
        }
    }
}
