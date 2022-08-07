using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;
using LitJson;
using System;
using UnityEngine.UI;

namespace LM_Workspace
{
	public class UtilManager : SingletonMono<UtilManager> 
	{
		
		/// <summary>
		/// 蓝圈模块
		/// </summary>
		[HideInInspector]
		public GameObject forumGo;


		/// <summary>
		/// 排行模块
		/// </summary>
		[HideInInspector]
		public GameObject foremanRankGo;


		/// <summary>
		/// 预约模块
		/// </summary>
		[HideInInspector]
		public GameObject createAppoimentGo;


		/// <summary>
		/// 超市模块
		/// </summary>
		[HideInInspector]
		public GameObject marketGo;

		/// <summary>
		/// 我的模块
		/// </summary>
		[HideInInspector]
		public GameObject myselfGo;

		/// <summary>
		/// 抽奖模块
		/// </summary>
		[HideInInspector]
		public GameObject drawGo;

		/// <summary>
		/// 签到模块
		/// </summary>
		[HideInInspector]
		public GameObject signInGo;

		/// <summary>
		/// canvas组件(作为其他模块的父节点)
		/// </summary>
		private GameObject canvasP;


		[HideInInspector]
		public GameObject messageAlert;
        [HideInInspector]
        public GameObject messageCommit;

		/// <summary>
		/// 是否需要弹出 联网提示
		/// </summary>
		private bool isNetWorkAlert;

        /// <summary>
        /// 注册登录模块
        /// </summary>
        [HideInInspector]
        public GameObject loginGo;

        /// <summary>
        /// 注册登录模块/selectPanel
        /// </summary>
        [HideInInspector]
        public GameObject loginGo_selectPanel;

        /// <summary>
        /// 注册登录模块/signinPanel
        /// </summary>
        [HideInInspector]
        public GameObject loginGo_signinPanel;

        /// <summary>
        /// 注册登录模块/yanzhengPanel
        /// </summary>
        [HideInInspector]
        public GameObject loginGo_yanzhengPanel;

        /// <summary>
        /// 注册登录模块/loginPanel
        /// </summary>
        [HideInInspector]
        public GameObject loginGo_loginPanel;

        /// <summary>
        /// loading界面
        /// </summary>
        [HideInInspector]
        public GameObject loadingPanel;

        /// <summary>
        /// loading文本
        /// </summary>
        [HideInInspector]
        public Text loadingText;

        /// <summary>
        /// 大图片界面
        /// </summary>
        [HideInInspector]
        public GameObject bigImagePanel;

        /// <summary>
        /// 大图片
        /// </summary>
        [HideInInspector]
        public RawImage bigRawImage;

        /// <summary>
        /// 服务器地址
        /// </summary>
        [HideInInspector]
        public string serverIP;

        [HideInInspector]
        public Dictionary<string, string> menpaiDic;
        [HideInInspector]
        public Dictionary<string, string> stateDic;
        [HideInInspector]
        public Dictionary<string, string> chufangDic;
        [HideInInspector]
        public Dictionary<string, string> weishengjianDic;
        [HideInInspector]
        public Dictionary<string, string> ketingDic;
        [HideInInspector]
        public Dictionary<string, string> guodaoDic;
        [HideInInspector]
        public Dictionary<string, string> yangtaiDic;
        [HideInInspector]
        public Dictionary<string, string> otherDic;
        [HideInInspector]
        public Dictionary<string, string> pingzhengDic;
        [HideInInspector]
        public Dictionary<string, string> buhegeDic;

        [HideInInspector]
        public Dictionary<string, Dictionary<string, string>> photoKey;

        private GameObject dingdanshiyaDetailPanel;

        #region 填写订单界面图片父物体
        /// <summary>
        /// 门牌
        /// </summary>
        [System.NonSerialized]
        public Transform menpaiParent;

        /// <summary>
        /// 试压仪状态
        /// </summary>
        [System.NonSerialized]
        public Transform stateParent;

        /// <summary>
        /// 厨房
        /// </summary>
        [System.NonSerialized]
        public Transform chufangParent;

        /// <summary>
        /// 卫生间
        /// </summary>
        [System.NonSerialized]
        public Transform weishengjianParent;

        /// <summary>
        /// 客厅
        /// </summary>
        [System.NonSerialized]
        public Transform ketingParent;

        /// <summary>
        /// 过道
        /// </summary>
        [System.NonSerialized]
        public Transform guodaoParent;

        /// <summary>
        /// 阳台
        /// </summary>
        [System.NonSerialized]
        public Transform yangtaiParent;

        /// <summary>
        /// 其他
        /// </summary>
        [System.NonSerialized]
        public Transform otherParent;

        /// <summary>
        /// 凭证
        /// </summary>
        [System.NonSerialized]
        public Transform pingzhengParent;

        /// <summary>
        /// 不合格
        /// </summary>
        [System.NonSerialized]
        public Transform buhegeParent;
        #endregion

        [System.NonSerialized]
        /// <summary>
        /// 大图片界面是否显示删除按钮
        /// </summary>
        public bool isBigImageShowDestroyBtn;

		protected override void Awake ()
		{
			base.Awake ();
#if RELEASE_MODEL
            serverIP = "api.zccjl.com";
#else
            serverIP = "test.zccjl.com";
#endif
			timeStr = "0";
			canvasP = GameObject.Find ("Canvas");
			messageAlert =Util.FindChildByName(canvasP, "Message_Alert");
            messageCommit = Util.FindChildByName(canvasP, "Message_Commit");
            loadingPanel = Util.FindChildByName(canvasP, "LoadingPanel");
            loadingText = Util.FindChildByName(loadingPanel, "Text").GetComponent<Text>();

            bigImagePanel = Util.FindChildByName(canvasP, "BigImage");
            bigRawImage = Util.FindChildByName(bigImagePanel, "ImagePanel/content/RawImage").GetComponent<RawImage>();

			        forumGo = Util.FindChildByName (canvasP, "- forum -");
			        foremanRankGo = Util.FindChildByName (canvasP, "- paihang -");
			        createAppoimentGo = Util.FindChildByName (canvasP, "- yuyue -");
			        marketGo = Util.FindChildByName (canvasP, "- chaoshi -");
			        myselfGo = Util.FindChildByName (canvasP, "- wode -");
			        drawGo = Util.FindChildByName (canvasP, "choujiang");
			        signInGo = Util.FindChildByName (canvasP, "- qiandao -");
            loginGo = Util.FindChildByName(canvasP, "- login & signin -");
            loginGo_selectPanel = Util.FindChildByName(loginGo, "p_select");
            loginGo_signinPanel = Util.FindChildByName(loginGo, "p_signin");
            loginGo_yanzhengPanel = Util.FindChildByName(loginGo, "p_yanzheng");
            loginGo_loginPanel = Util.FindChildByName(loginGo, "p_login");

            dingdanshiyaDetailPanel = Util.FindChildByName(myselfGo, "p_dingdanshiya_detail");

            menpaiDic = new Dictionary<string, string>();
            stateDic = new Dictionary<string, string>();
            chufangDic = new Dictionary<string, string>();
            weishengjianDic = new Dictionary<string, string>();
            ketingDic = new Dictionary<string, string>();
            guodaoDic = new Dictionary<string, string>();
            yangtaiDic = new Dictionary<string, string>();
            otherDic = new Dictionary<string, string>();
            pingzhengDic = new Dictionary<string, string>();
            buhegeDic = new Dictionary<string, string>();
            photoKey = new Dictionary<string, Dictionary<string, string>>();

            menpaiParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/menpaiArray").transform;
            stateParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/stateArray").transform;
            chufangParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/chufangArray").transform;
            weishengjianParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/weishengjianArray").transform;
            ketingParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/ketingArray").transform;
            guodaoParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/guodaoArray").transform;
            yangtaiParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/yangtaiArray").transform;
            otherParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/otherArray").transform;
            pingzhengParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/pingzhengArray").transform;
            buhegeParent = Util.FindChildByName(dingdanshiyaDetailPanel, "menu_1_detail/content/buhegeArray").transform;

            isNetWorkAlert = true;

            //List<string> testList = new List<string>();
            //testList.Add("11");
            //testList.Add("22");
            //string str = "[";
            //for (int i = 0; i < testList.Count;i++)
            //{
            //    if (i < testList.Count - 1)
            //        str += "\"" + testList[i] + "\"" + ",";
            //    else
            //        str += "\"" + testList[i] + "\"";
            //}
            //str += "]";
            //Debug.Log(str);


            //拓展高登的功能


//			if(messageAlert != null)
//				messageAlert.SetActive (false);

			//UserData.instance.token = "mwpVJ5K0tjojPkaL";

			//Debug.Log (UserData.instance.token + "token");
		}

		[HideInInspector]
		public string NormalError = "填入信息不符合要求,请核实!";

		[HideInInspector]
		public string ApplicationError = "系统异常,请稍候再试!";

		[HideInInspector]
		public string CreateAppoimentHasBackCall = "订单已回填,无法预约!";

        [System.NonSerialized]
		public string NetWorkError = "无网络接入,请恢复网络信号后再试!";

        [HideInInspector]
        public string LoadingPostStr = "正在加载订单数据，请稍候!";

        [HideInInspector]
        public string LoadingNormalStr = "Loading...";

		[HideInInspector]
		public Color32 greenColor = new Color32(38,149,159,255);

		[HideInInspector]
		public Color32 grayColor = new Color32 (195, 204, 208, 255);

		[HideInInspector]
		public int headImage_foreamRank_SizeX = 93;

		[HideInInspector]
		public int headImage_foreamRank_SizeY = 94;

		[HideInInspector]
		public string areaName;

		[HideInInspector]
		public int areaID;

        [HideInInspector]
        public string cardRemark;

        [HideInInspector]
		public string timeStr;

		[HideInInspector]
		public bool isCalledDataTime = false;


        public static List<Area> provinceDataList = new List<Area>();
        public static List<Area> cityDataList = new List<Area>();
        public static List<Area> areaDataList = new List<Area>();

        /// <summary>
        /// 接收ios返回的时间信息
        /// </summary>
        public void GetIosDataTime(string _dataTime)
		{
//			string year = _dataTime.Split(' ')[0].Split('-')[0];
//			string month = _dataTime.Split(' ')[0].Split('-')[1];
//			string day = _dataTime.Split(' ')[0].Split('-')[2];
//			string hour = _dataTime.Split(' ')[1].Split('-')[0];
//			string min = _dataTime.Split(' ')[1].Split('-')[1];
//			string sec = _dataTime.Split(' ')[1].Split('-')[2];
			timeStr = _dataTime;
			UtilManager.getInstance.isCalledDataTime = true;
			//dataTimeText.text =  year + "年" + month + "月" + day + "日" + hour + "时" + min + "分";
		}

        public void GetAndroidDataTime(string _dataTime)
        {
            timeStr = _dataTime;
            //Util.ShowAlert(UtilManager.getInstance.messageAlert, "时间", timeStr);
            if(CreateAppoimentManager.getInstance != null)
                CreateAppoimentManager.getInstance.SplitDataTime();
            if(EventManager.instance != null)
                EventManager.instance.NotifyEvent(XMWorkspace.Event.CallAndroidTimeGD);


        }

        public void CallOtherAppError(string _errorStr,Action cb=null)
        {
            Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", _errorStr, delegate
            {
                
                UtilManager.getInstance.messageAlert.SetActive(false);
                cb?.Invoke();

            });
        }

        public bool CheckInternet()
        {
            bool isInternet;
            if (Application.internetReachability == NetworkReachability.NotReachable && isNetWorkAlert)
            {
              //  Debug.Log("弹窗");
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", NetWorkError, delegate
                {
                  //  Debug.Log(NetWorkError);
                    UtilManager.getInstance.messageAlert.SetActive(false);

                });
                isInternet = false;
            }
            else
            {
                isInternet = true;
            }
            return isInternet;
        }

		void Update()
		{
			if (Application.internetReachability== NetworkReachability.NotReachable && isNetWorkAlert)              
			{ 
                if(loadingPanel.activeInHierarchy)
                {
                    Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "图片上传失败，请稍候再试!", delegate
                    {
                        //  Debug.Log(NetWorkError);
                        UtilManager.getInstance.messageAlert.SetActive(false);
                        loadingPanel.SetActive(false);
                    });
                }                    
			}
		}

        public static void InitRoleAreaData(JsonData data) {
            if (((int)data["status"]) == 1) {
                JsonData allAreaData = data["data"];
                provinceDataList.Clear();
                cityDataList.Clear();
                areaDataList.Clear();

                for (int i = 0; i < allAreaData.Count; i++) {
                    Area tmpArea = new Area();
                    tmpArea.id = ((int)allAreaData[i]["id"]);
                    tmpArea.areaType = (AreaType)((int)allAreaData[i]["type"]);
                    tmpArea.createTime = ((string)allAreaData[i]["create_time"]);
                    tmpArea.parent = ((int)allAreaData[i]["parent_id"]);
                    tmpArea.title = ((string)allAreaData[i]["title"]);

                    switch (tmpArea.areaType) {
                        case AreaType.Province:
                            provinceDataList.Add(tmpArea);
                            break;
                        case AreaType.City:
                            cityDataList.Add(tmpArea);
                            break;
                        case AreaType.Area:
                            areaDataList.Add(tmpArea);
                            break;
                    }

                    if (tmpArea.id == UserData.instance.area) {
                        instance.areaName = tmpArea.title;
                        instance.areaID = tmpArea.id;
                        Debug.Log(tmpArea.id);                       
                    }
                }
            }
        }

    }
}
