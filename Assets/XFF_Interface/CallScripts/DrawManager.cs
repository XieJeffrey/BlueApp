using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;

namespace LM_Workspace
{
    public class DrawManager : SingletonMono<DrawManager> {

        private List<Text> drawItemText = new List<Text>();

        private List<float> rotation = new List<float>();

        private UIEventListener drawBtn;//抽奖按钮
        private UIEventListener closeResultBtn;//关闭抽奖结果面板
        private UIEventListener closeBtn;
        private Text acountText;
        private Text resultText;
        private GameObject resultPanel;//抽奖结果面板
        private GameObject rotatePanel;//旋转面板
        private GameObject lightPanel;//灯灯面板
        private InterfaceManager interfaceManager;

        private int drawId = 0;
        private bool isDraw = false;
        private float drawTime = 0;
        private float rotateSpeed = 250;
		private int drawType = 0;


        private List<Award> awardList = new List<Award>();

        protected override void Awake()
        {
            drawBtn = Util.FindChildByName(gameObject, "content/image/center").GetComponent<UIEventListener>();
            closeBtn = Util.FindChildByName(gameObject, "header/btn_back").GetComponent<UIEventListener>();
            closeResultBtn = Util.FindChildByName(gameObject, "info/Image").GetComponent<UIEventListener>();
            resultPanel = Util.FindChildByName(gameObject, "info");
            rotatePanel = Util.FindChildByName(gameObject, "content/image/rotate");
            lightPanel = Util.FindChildByName(gameObject, "content/image/lights");
            acountText = Util.FindChildByName(gameObject, "content/top/numberLabel").GetComponent<Text>();
            resultText = Util.FindChildByName(gameObject, "info/Image/Text").GetComponent<Text>();

            for (var i = 0; i < 8; i++)
            {
                drawItemText.Add(rotatePanel.transform.GetChild(i).Find("Text").GetComponent<Text>());
            }

//            rotation.Add(337.5f);
//            rotation.Add(22.5f);
//            rotation.Add(67.5f);
//            rotation.Add(112.5f);
//            rotation.Add(157.5f);
//            rotation.Add(202.5f);
//            rotation.Add(247.5f);
//            rotation.Add(292.5f);

			rotation.Add(337.5f);
			rotation.Add(292.5f);
			rotation.Add(247.5f);
			rotation.Add(202.5f);
			rotation.Add(157.5f);
			rotation.Add(112.5f);
			rotation.Add(67.5f);
			rotation.Add(22.5f);

            drawBtn.onClick = OnClickDrawBtn;
            closeBtn.onClick = OnClickCloseBtn;
            closeResultBtn.onClick = OnClickResultCloseBtn;

            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetBaseInfo, OnGetBaseInfo);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.PlayWithLottery, playDrawAniamtion);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetAwardList, GetAwardList);

        }

        void OnEnable()
        {
            acountText.text = UserData.instance.coin.ToString();
            interfaceManager = GameObject.Find("Camera").GetComponent<InterfaceManager>();
            interfaceManager.GetBaseInfo();
            interfaceManager.GetAwardList();
        }

         void Update()
        {
            if (isDraw)
            {
                Debug.Log("drawing");
                rotatePanel.transform.localEulerAngles +=new Vector3(0,0,rotateSpeed) * Time.deltaTime;
                lightPanel.transform.localEulerAngles += new Vector3(0, 0, rotateSpeed) * Time.deltaTime;
                rotateSpeed += 100 * Time.deltaTime;
                if (rotateSpeed >= 550)
                    rotateSpeed = 550;
                drawTime -= Time.deltaTime;
                if (drawTime <= 0)
                {
                    isDraw = false;
					if (drawType != 3)
                    {
						Debug.Log (drawId + "drawId");
                        resultPanel.SetActive(true);
                        resultText.text = "恭喜获得"+ awardList[drawId-1].title;

						//Debug.Log ("wqueiqweuqwioe");
                    }
						interfaceManager.GetBaseInfo();
                    rotatePanel.transform.localEulerAngles = new Vector3(0, 0, rotation[drawId - 1]);
                    lightPanel.transform.localEulerAngles = new Vector3(0, 0, rotation[drawId - 1]);
                }

            }
        }

        /// <summary>
        /// 点击抽奖按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickDrawBtn(GameObject go,PointerEventData data)
        {
			if (isDraw)
				return;
            //interfaceManager.PlayWithLottery();
            //return;
            rotatePanel.transform.localEulerAngles = new Vector3(0, 0,0);
            lightPanel.transform.localEulerAngles = new Vector3(0, 0, 0);

            if(awardList.Count==0)
            {
                //Debug.LogError("获取奖品列表中....");
				Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "暂无奖品!");
                return;
            }

            if (UserData.instance.coin >= 50)
                interfaceManager.PlayWithLottery();
            else
				Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "用户蓝币不足!");
               // Debug.LogError("用户蓝币不足");
        }

        /// <summary>
        /// 获取奖品列表
        /// </summary>
        /// <param name="data"></param>
        void GetAwardList(params object[] data)
        {
            bool state = bool.Parse(data[0].ToString());
            if (state)
            {
                awardList = DataManager.instance.systemAwardList;
                for (int i = 0; i < drawItemText.Count; i++)
                {
                    drawItemText[i].text = awardList[i].title;
                }
            }
        }

        /// <summary>
        /// 播放抽奖动画
        /// </summary>
        /// <param name="data"></param>
        void playDrawAniamtion(params object[]data)
        {
            //drawId = 5;// ((Award)data[1]).id;
            //isDraw = true;
            //drawTime = 5f;
            //rotateSpeed = 50f;
            //return;
            bool state = bool.Parse(data[0].ToString());
			Debug.Log (state + "抽奖结果");
            if (state)
            {
                drawId = ((Award)data[1]).id;
				drawType = ((Award)data[1]).type;
                isDraw = true;
                drawTime = 5f;
                rotateSpeed = 50f;
				acountText.text = UserData.instance.coin.ToString();
            }
            else
            {
                Debug.LogError("抽奖失败");
            }
        }

        /// <summary>
        /// 关闭抽奖结果界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickResultCloseBtn(GameObject go, PointerEventData data)
        {
			
            resultPanel.SetActive(false);
        }

        void OnClickCloseBtn(GameObject go, PointerEventData data)
        {
			isDraw = false;
			rotatePanel.transform.localEulerAngles = new Vector3(0, 0,0);
			lightPanel.transform.localEulerAngles = new Vector3(0, 0, 0);
			resultPanel.SetActive(false);
			UtilManager.getInstance.signInGo.SetActive (true);
            gameObject.SetActive(false);

        }

        /// <summary>
        /// 刷新玩家信息
        /// </summary>
        /// <param name="data"></param>
        void OnGetBaseInfo(params object[] data)
        {
			
            bool state = bool.Parse(data[0].ToString());
			Debug.Log (state);
            if (state)
                acountText.text = UserData.instance.coin.ToString();
        }

    }
}
