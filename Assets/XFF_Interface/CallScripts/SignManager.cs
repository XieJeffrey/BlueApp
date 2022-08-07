using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;

namespace LM_Workspace {
    public class SignManager : SingletonMono<SignManager> {
        private GameObject closeBtn;
        private GameObject signStatePanel;
        private List<GameObject> signStateArray=new List<GameObject>();
        private InterfaceManager interfaceManager;

        private GameObject signBtn;
        private GameObject weekSignBtn;
        private GameObject createForumBtn;
        private GameObject commentForumBtn;
        private GameObject setStarBtn;
        private GameObject goDrawBtn;

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

        // Use this for initialization
        void Start() {
            interfaceManager = GameObject.Find("Camera").GetComponent<InterfaceManager>();

            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetLastWeekSign, ShowSignState);
            //EventManager.instance.RegisterEvent(XMWorkspace.Event.SetSign, OnSign);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.CheckSevenSign, OnWeekSign);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.CheckCreateForum, OnCreateForum);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.CheckReplyForum, OnCommentForum);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.CheckSetStar, OnSetStar);

            infoObj = Util.FindChildByName(gameObject, "p_main/infoBG");
            infoOpenBtn = Util.FindChildByName(gameObject, "p_main/header/infoOpenBtn").GetComponent<UIEventListener>();
            infoCloseBtn = Util.FindChildByName(infoObj, "infoImage/Btn").GetComponent<UIEventListener>();

            infoOpenBtn.onClick = InfoOpenBtnClick;
            infoCloseBtn.onClick = InfoCloseBtnClick;

            closeBtn = Util.FindChildByName(gameObject, "p_main/header/btn_back");
            signStatePanel = Util.FindChildByName(gameObject, "p_main/dragPanel/offset/content/item_0/layout");

            signBtn = Util.FindChildByName(gameObject, "p_main/dragPanel/offset/content/item_1/btn_lingqu");
            weekSignBtn = Util.FindChildByName(gameObject, "p_main/dragPanel/offset/content/item_2/btn_lingqu");
            createForumBtn = Util.FindChildByName(gameObject, "p_main/dragPanel/offset/content/item_3/btn_lingqu");
            commentForumBtn = Util.FindChildByName(gameObject, "p_main/dragPanel/offset/content/item_4/btn_lingqu");
            setStarBtn = Util.FindChildByName(gameObject, "p_main/dragPanel/offset/content/item_5/btn_lingqu");
			goDrawBtn = Util.FindChildByName(gameObject, "p_main/dragPanel/offset/content/btn/buttonPrefab");

            signBtn.GetComponent<UIEventListener>().onClick = OnClickSign;
            weekSignBtn.GetComponent<UIEventListener>().onClick = OnClickWeekSign;
            createForumBtn.GetComponent<UIEventListener>().onClick = OnClickCreateForum;
            commentForumBtn.GetComponent<UIEventListener>().onClick = OnClickCommentBtn;
            setStarBtn.GetComponent<UIEventListener>().onClick = OnClickSetStarBtn;
			goDrawBtn.GetComponent<UIEventListener>().onClick = OnClickGoDraw;

            closeBtn.GetComponent<UIEventListener>().onClick = OnClickCloseBtn;

            for (int i = 0; i < 7; i++)
            {
                signStateArray.Add(signStatePanel.transform.GetChild(i).gameObject);
            }

            Refresh();

        }

        void Refresh()
        {
            #region 默认的签到表现
            System.TimeSpan timeSpan = new System.TimeSpan(24, 0, 0);
            System.DateTime tmp = System.DateTime.Now - timeSpan;
            System.DateTime date = System.DateTime.Now;
            for (int i = 6; i >= 0; i--)
            {
                string dayofweek = date.DayOfWeek.ToString();
                switch (dayofweek)
                {
                    case "Saturday":
                        Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "周六";
                        break;
                    case "Friday":
                        Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "周五";
                        break;
                    case "Thursday":
                        Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "周四";
                        break;
                    case "Wednesday":
                        Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "周三";
                        break;
                    case "Tuesday":
                        Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "周二";
                        break;
                    case "Monday":
                        Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "周一";
                        break;
                    case "Sunday":
                        Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "周日";
                        break;
                }
                Util.FindChildByName(signStateArray[i], "datelabel").GetComponent<Text>().text = date.Month + "/" + date.Day;
                date = date - timeSpan;
            }
            #endregion            
           
            //已登陆的情况下请求过去7天签到数据
            if (UserData.instance.token != "")
                interfaceManager.GetLastWeekSign();          
        }

        /// <summary>
        /// 刷新签到情况
        /// </summary>
        void ShowSignState(params object[] param)
        {
            for (int i = 0; i < UserData.instance.lastWeekSign.Count; i++)
            {
                if (UserData.instance.lastWeekSign[i] == 0)
                {
                    Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "未签";
                    Util.FindChildByName(signStateArray[i], "background").GetComponent<SVGImporter.SVGImage>().color = new Color(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    Util.FindChildByName(signStateArray[i], "label").GetComponent<Text>().text = "已签";
                    Util.FindChildByName(signStateArray[i], "background").GetComponent<SVGImporter.SVGImage>().color = new Color(0.15f, 0.58f, 0.62f);
                }
            }        
        }

        /// <summary>
        /// 点击返回按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickCloseBtn(GameObject go, PointerEventData data)
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 点击签到按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickSign(GameObject go, PointerEventData data)
        {
            Debug.Log("点击签到");
            if (UserData.instance.token != "")
                interfaceManager.SetSign();
            else
            {
                //消息提示
                Debug.LogError("未登录");               
            }
        }

        /// <summary>
        /// 签到结果
        /// </summary>
        /// <param name="param"></param>
        void OnSign(params object[] param)
        {
            //Util.FindChildByName(signBtn, "label").GetComponent<Text>().text = "已领取";
            //signBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.5f, 0.5f, 0.5f);
            bool state = bool.Parse(param[0].ToString());
            if (state == true)
            {
				Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取成功!");
               // Debug.Log("积分领取成功");               
            }
            else
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取失败!");
               // Debug.Log("积分领取失败");                
            }
        }

        /// <summary>
        /// 点击每周签到按钮任务按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickWeekSign(GameObject go, PointerEventData data)
        {
            Debug.Log("点击每周签到");
            if (UserData.instance.token != "")
                interfaceManager.Check7Sign();
            else
            {
                //消息提示
                Debug.LogError("未登录");
            }
        }

        /// <summary>
        /// 每周签到结果
        /// </summary>
        /// <param name="param"></param>
        void OnWeekSign(params object[] param)
        {
            bool state = bool.Parse(param[0].ToString());
            if (state == true)
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取成功!");
                //Util.FindChildByName(weekSignBtn, "label").GetComponent<Text>().text = "已领取";
                //weekSignBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取失败!");
                //Util.FindChildByName(weekSignBtn, "label").GetComponent<Text>().text = "领取";
                //weekSignBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.15f, 0.58f, 0.62f);
            }
        }

        /// <summary>
        /// 点击发帖任务按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickCreateForum(GameObject go, PointerEventData data)
        {
            Debug.Log("点击发帖");
            if (UserData.instance.token != "")
                interfaceManager.CheckCreateForum();
            else
            {
                //消息提示
                Debug.LogError("未登录");
            }
        }

        /// <summary>
        /// 发帖任务返回结果
        /// </summary>
        void OnCreateForum(params object[]param)
        {
            bool state = bool.Parse(param[0].ToString());
            if (state == true)
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取成功!");
                //Util.FindChildByName(createForumBtn, "label").GetComponent<Text>().text = "已领取";
                //createForumBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取失败!");
                //Util.FindChildByName(createForumBtn, "label").GetComponent<Text>().text = "领取";
                //createForumBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.15f, 0.58f, 0.62f);
            }
        }

        /// <summary>
        /// 点击回复任务按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickCommentBtn(GameObject go, PointerEventData data)
        {
            Debug.Log("点击回帖");
            if (UserData.instance.token != "")
                interfaceManager.CheckReplyForum();
            else
            {
                //消息提示
                Debug.LogError("未登录");
            }
        }

        /// <summary>
        /// 回复任务返回结果
        /// </summary>
        /// <param name="param"></param>
        void OnCommentForum(params object[] param)
        {
            bool state = bool.Parse(param[0].ToString());
            if (state == true)
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取成功!");
                //Util.FindChildByName(commentForumBtn, "label").GetComponent<Text>().text = "已领取";
                //commentForumBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取失败!");
                //Util.FindChildByName(commentForumBtn, "label").GetComponent<Text>().text = "领取";
                //commentForumBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.15f, 0.58f, 0.62f);
            }
        }

        /// <summary>
        /// 点击点赞任务按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickSetStarBtn(GameObject go, PointerEventData data)
        {
            Debug.Log("点击点赞");
            if (UserData.instance.token != "")
                interfaceManager.CheckSetStar();
            else
            {
                //消息提示
                Debug.LogError("未登录");
            }
        }

        /// <summary>
        /// 点赞任务返回
        /// </summary>
        /// <param name="param"></param>
        void OnSetStar(params object[] param)
        {
            bool state = bool.Parse(param[0].ToString());
            if (state == true)
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取成功!");
                //Util.FindChildByName(setStarBtn, "label").GetComponent<Text>().text = "已领取";
                //setStarBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "蓝币领取失败!");
                //Util.FindChildByName(setStarBtn, "label").GetComponent<Text>().text = "领取";
                //setStarBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.15f, 0.58f, 0.62f);
            }
        }

        /// <summary>
        /// 点击跳转
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickGoDraw(GameObject go, PointerEventData data)
        {
			UtilManager.getInstance.drawGo.SetActive (true);
			UtilManager.getInstance.signInGo.SetActive (false);
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
