using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;
using DG.Tweening;
using GD;
using LitJson;

namespace LM_Workspace
{
	public class CreateAppoimentManager : SingletonMono<CreateAppoimentManager> 
	{
//		/// <summary>
//		/// 选择城市
//		/// </summary>
//		private Dropdown cityDropDown;

        [System.NonSerialized]
        public Text cityText;

		/// <summary>
		/// 小区名字
		/// </summary>
        [System.NonSerialized]
        public Text pNameText;

		/// <summary>
		/// 具体地址
		/// </summary>
        [System.NonSerialized]
        public InputField addressInput;

		/// <summary>
		/// 业主姓名
		/// </summary>
		private InputField personNameInput;

		/// <summary>
		/// 业主手机号
		/// </summary>
		private InputField telephoneInput;

        ///// <summary>
        ///// 预约上门时间
        ///// </summary>
        //private string dataTime = "0";

        /// <summary>
        /// 预约日期
        /// </summary>
        private string date;

        /// <summary>
        /// 预约时间段
        /// </summary>
        private int time_frame;

		/// <summary>
		/// 显示预约上门时间label
		/// </summary>
		private Text dataTimeText;

		/// <summary>
		/// 预约上门时间按钮(调用原生方法)
		/// </summary>
		private UIEventListener dataTimeBtn;

		/// <summary>
		/// 是否回填 0:否 1:是
		/// </summary>
		private string backCall = "0";

		/// <summary>
		/// 无回填按钮
		/// </summary>
		private UIEventListener backCallNoBtn;
		private GameObject backCallNoObj;

		/// <summary>
		/// 有回填按钮
		/// </summary>
		private UIEventListener backCallYesBtn;
		private GameObject backCallYesObj;

		/// <summary>
		/// 备注
		/// </summary>
		private InputField remarkInput;

		/// <summary>
		/// 预约按钮
		/// </summary>
		private UIEventListener appoimentBtn;
		//private Button appoimentBtn;

		/// <summary>
		/// 预约成功提示组件
		/// </summary>
		private GameObject tipsSuccess;

		/// <summary>
		/// 预约失败提示组件
		/// </summary>
		private GameObject tipsFailed;

		/// <summary>
		/// 预约失败提示Label
		/// </summary>
		private Text tipsFailedText;

		/// <summary>
		/// 预约成功回退按钮
		/// </summary>
		private UIEventListener tipsSuccessBtn;

		/// <summary>
		/// 预约失败回退按钮
		/// </summary>
		private UIEventListener tipsFailedBtn;

		/// <summary>
		/// 半透明遮罩
		/// </summary>
		private GameObject mask;

		/// <summary>
		/// 白色遮罩
		/// </summary>
		private GameObject maskWhite;

		/// <summary>
		/// 是否已经打开过ios时间组件
		/// </summary>
		[HideInInspector]
		public bool isCalledDataTime;

		/// <summary>
		/// 和ios时间组件一同显示的按钮
		/// </summary>
		private UIEventListener iosDataTimeBtn;

        /// <summary>
        /// 地图的UI
        /// </summary>
		[HideInInspector]
		public RectTransform mapRect;

        /// <summary>
        /// 大地图的UI
        /// </summary>
        [HideInInspector]
        public RectTransform mapRectBig;

        /// <summary>
        /// 搜索界面
        /// </summary>
        [System.NonSerialized]
        public GameObject mapSearch;

        /// <summary>
        /// 搜索界面返回按钮
        /// </summary>
        [System.NonSerialized]
        public UIEventListener mapSearchReturnBtn;

        /// <summary>
        /// 选择时间界面
        /// </summary>
        [System.NonSerialized]
        public GameObject selectTimePanel;

        /// <summary>
        /// 时间段
        /// </summary>
        [System.NonSerialized]
        public UIEventListener selectTime_1st;

        /// <summary>
        /// 时间段
        /// </summary>
        [System.NonSerialized]
        public UIEventListener selectTime_2nd;

        /// <summary>
        /// 时间段背景1st
        /// </summary>
        [System.NonSerialized]
        public Image selectTimeBG_1st;

        /// <summary>
        /// 时间段背景2nd
        /// </summary>
        [System.NonSerialized]
        public Image selectTimeBG_2nd;

        /// <summary>
        /// 时间段字体1st
        /// </summary>
        [System.NonSerialized]
        public Text selectTimeText_1st;

        /// <summary>
        /// 时间段字体1st
        /// </summary>
        [System.NonSerialized]
        public Text selectTimeText_2nd;

        /// <summary>
        /// 日期的父物体
        /// </summary>
        [System.NonSerialized]
        public Transform dayOfTimeParent;

        /// <summary>
        /// 时间选择界面的关闭按钮
        /// </summary>
        [System.NonSerialized]
        public UIEventListener selectTimeCloseBtn;

        /// <summary>
        /// 时间选择界面的确定按钮
        /// </summary>
        [System.NonSerialized]
        public UIEventListener commitTimeSelectBtn;


		void OnEnable()
		{
            //isCalledDataTime = false;
            GameObject pMain = Util.FindChildByName(gameObject, "p_main");
            pMain.SetActive(true);

            selectTimePanel = Util.FindChildByName(pMain, "p_select_time");
            dayOfTimeParent = Util.FindChildByName(selectTimePanel, "timeBG/time_date/dateParent").transform;

            selectTimeBG_1st = Util.FindChildByName(selectTimePanel, "timeBG/time_select/time_select1/BG").GetComponent<Image>();
            selectTimeBG_2nd = Util.FindChildByName(selectTimePanel, "timeBG/time_select/time_select2/BG").GetComponent<Image>();
            selectTimeText_1st = Util.FindChildByName(selectTimePanel, "timeBG/time_select/time_select1/Text").GetComponent<Text>();
            selectTimeText_2nd = Util.FindChildByName(selectTimePanel, "timeBG/time_select/time_select2/Text").GetComponent<Text>();

            selectTime_1st = selectTimeBG_1st.transform.parent.GetComponent<UIEventListener>();
            selectTime_2nd = selectTimeBG_2nd.transform.parent.GetComponent<UIEventListener>();

            selectTimeCloseBtn = Util.FindChildByName(selectTimePanel, "timeBG/closeBtn").GetComponent<UIEventListener>();
            commitTimeSelectBtn = Util.FindChildByName(selectTimePanel, "timeBG/commitBtn").GetComponent<UIEventListener>();
            InsertDateTime();

			//cityDropDown = GameObject.Find ("Dropdown_yuyue").GetComponent<Dropdown>();
			if(GameObject.Find("City_Label_yuyue") != null)
				cityText = GameObject.Find("City_Label_yuyue").GetComponent<Text>();
			
			if(GameObject.Find("map_yuyue") != null)
				mapRect = GameObject.Find ("map_yuyue").GetComponent<RectTransform> ();

            mapSearch = Util.FindChildByName(gameObject, "p_main/p_form/Map_Search");
            mapRectBig = Util.FindChildByName(mapSearch, "map_yuyue_Big").GetComponent<RectTransform>();
            mapSearchReturnBtn = Util.FindChildByName(mapSearch, "topbar/btn_back").GetComponent<UIEventListener>();

            //if (GameObject.Find("map_yuyue_Big") != null)
                //mapRectBig = GameObject.Find("map_yuyue_Big").GetComponent<RectTransform>();
			
            if(GameObject.Find("PNameText") != null)
                pNameText = GameObject.Find ("PNameText").GetComponent<Text> ();
            pNameText.text = "";
			
			if(GameObject.Find("addressInput_yuyue") != null)
				addressInput = GameObject.Find ("addressInput_yuyue").GetComponent<InputField> ();
			addressInput.text = "";
			
			if(GameObject.Find("personNameInput_yuyue") != null)
				personNameInput = GameObject.Find ("personNameInput_yuyue").GetComponent<InputField> ();
			personNameInput.text = "";

			if(GameObject.Find("telephoneInput_yuyue") != null)
				telephoneInput = GameObject.Find ("telephoneInput_yuyue").GetComponent<InputField> ();
				telephoneInput.text = "";

			if(GameObject.Find("remarkInput_yuyue") != null)
				remarkInput = GameObject.Find ("remarkInput_yuyue").GetComponent<InputField> ();
			remarkInput.text = "";

			//if(GameObject.Find("btn_yuyue") != null)
				//appoimentBtn = GameObject.Find ("btn_yuyue").GetComponent<Button> ();
			appoimentBtn = Util.FindChildByName(gameObject,"p_main/p_form/btn_yuyue").GetComponent<UIEventListener>();
			
			if(GameObject.Find("backCallNoBtn_yuyue") != null)
				backCallNoBtn = GameObject.Find ("backCallNoBtn_yuyue").GetComponent<UIEventListener> ();
			
			if(GameObject.Find("backCallYesBtn_yuyue") != null)
				backCallYesBtn = GameObject.Find ("backCallYesBtn_yuyue").GetComponent<UIEventListener> ();
			

				backCallNoObj = backCallNoBtn.transform.GetChild (0).gameObject;
			

				backCallYesObj = backCallYesBtn.transform.GetChild (0).gameObject;
			
			if(GameObject.Find("tipsSuccess_yuyue") != null)
				tipsSuccess = GameObject.Find ("tipsSuccess_yuyue");

			if(GameObject.Find("tipsfailed_yuyue") != null)
				tipsFailed = GameObject.Find ("tipsfailed_yuyue");

			tipsFailedText = tipsFailed.transform.Find ("label").GetComponent<Text> ();

			tipsSuccessBtn = tipsSuccess.transform.Find ("button").GetComponent<UIEventListener> ();
			tipsFailedBtn = tipsFailed.transform.Find ("button").GetComponent<UIEventListener> ();

			if(GameObject.Find("Mask_yuyue") != null)
			mask = GameObject.Find ("Mask_yuyue");

			if(GameObject.Find("MaskWhite_yuyue") != null)
			maskWhite = GameObject.Find ("MaskWhite_yuyue");

			if(GameObject.Find("dataTimeBtn_yuyue") != null)
			dataTimeBtn = GameObject.Find ("dataTimeBtn_yuyue").GetComponent<UIEventListener> ();

			if(GameObject.Find("closeIosTimeBtn_yuyue") != null)
			iosDataTimeBtn = GameObject.Find ("closeIosTimeBtn_yuyue").GetComponent<UIEventListener> ();

			if(GameObject.Find("dataTimeText_yuyue") != null)
			dataTimeText = GameObject.Find ("dataTimeText_yuyue").GetComponent<Text> ();
			dataTimeText.text = "";
            date = "";
            time_frame = 0;

			backCallYesObj.SetActive (false);
			tipsSuccess.SetActive (false);
			tipsFailed.SetActive (false);
			mask.SetActive (false);
			maskWhite.SetActive (false);
			iosDataTimeBtn.gameObject.SetActive (false);

            for (int i = 0; i < UtilManager.areaDataList.Count; i++) {
                if (UtilManager.areaDataList[i].id == UtilManager.instance.areaID) {
                    UtilManager.instance.areaName = UtilManager.areaDataList[i].title;
                }
            }
			cityText.text = UtilManager.getInstance.areaName;
            UniWebViewPluginManager.instance.LoadBaiDuMapInit (cityText.text);          

		}

		void Start()
		{



          //  Debug.Log(dayofweek);

			//Debug.Log (appoimentBtn.name);
			appoimentBtn.onClick = AppoimentBtnClick;
//			appoimentBtn.onClick.AddListener (delegate {
//				AppoimentBtnClick();
//			});
			backCallNoBtn.onClick = BackCallMethodNo;
			backCallYesBtn.onClick = BackCallMethodYes;
			tipsFailedBtn.onClick = AppoimentFailedClick;
			tipsSuccessBtn.onClick = AppoimentSuccessClick;
            DateTimeType timeType = dayOfTimeParent.GetChild(0).GetComponent<DateTimeType>();
            dataTimeBtn.onClick = delegate {
                DateTimeBtnCommit(timeType);
            };
            selectTimeCloseBtn.onClick = delegate {

                ChanelTime();
            };

            commitTimeSelectBtn.onClick = delegate {

                CommitTime();
            };

            selectTime_1st.onClick = delegate {
                
                SelectTimeDate(0);
            };
            selectTime_2nd.onClick = delegate {
                
                SelectTimeDate(1);
            };
            //if(Application.platform == RuntimePlatform.IPhonePlayer)
            //{
            //    // dataTimeBtn.onClick = CallIosDataTimeClick;
            //    // iosDataTimeBtn.onClick = CloseIosDataTimeClick;

            //}
            //else
            //{
            //   // dataTimeBtn.onClick = CallAndroidTimeClick;
            //}

            // addressInput.onValidateInput.
            pNameText.GetComponent<UIEventListener>().onClick = ReSizeMapView;
            mapSearchReturnBtn.onClick = ReturnMapSearch;
            //addressInput.onValueChanged.AddListener(delegate
            //{
            //    Debug.Log(addressInput.text + "名字");
            //    string url = "http://api.map.baidu.com/place/v2/search?query=" + addressInput.text + "&region=" + cityText.text + "&city_limit=true&output=json&page_size=20&ak=OMS2N5dxmG8p0FjSHz87Pqvw";
            //    StartCoroutine(PostMapMethod(url));
            //});

			//addressInput.onEndEdit.AddListener (delegate(string arg0) {
			//	UniWebViewPluginManager.instance.LoadBaiDuMap (addressInput.text, pNameInput.text, cityText.text);
			//});

			EventManager.instance.RegisterEvent(XMWorkspace.Event.CreateAppointment,OnCreateAppoiment);

           
			//GameObject.Find("Camera").GetComponent<InterfaceManager>().Login(1, "11223344556", "123456", "8888");
		}

        private void DateTimeBtnCommit(DateTimeType timeType)
        {
            SelectTimeDate(0);
            SelectDay(timeType.dayID, timeType.addDay);
            selectTimePanel.SetActive(true);
            // mapRect.gameObject.SetActive(false);
            UniWebViewPluginManager.getInstance.webView.Hide();
        }

        /// <summary>
        /// 取消时间
        /// </summary>
        private void ChanelTime()
        {
            if(dataTimeText.text.Length == 0)
            {
                date = "";
                time_frame = 0;
            }

            selectTimePanel.SetActive(false);
            //mapRect.gameObject.SetActive(true);
            UniWebViewPluginManager.getInstance.webView.Show();
        }

        /// <summary>
        /// 提交时间
        /// </summary>
        private void CommitTime()
        {
            if(time_frame == 1)
            {
                dataTimeText.text = date + "   " + "09:00-12:00";
            }
            else
            {
                dataTimeText.text = date + "   " + "12:00-17:30";
            }

            selectTimePanel.SetActive(false);
            UniWebViewPluginManager.getInstance.webView.Show();
           // mapRect.gameObject.SetActive(true);
        }

        /// <summary>
        /// 选择日期
        /// </summary>
        public void SelectDay(int _id,int _day)
        {
            for (int i = 0; i < dayOfTimeParent.childCount; i++)
            {
                dayOfTimeParent.GetChild(i).GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
             
            dayOfTimeParent.GetChild(_id).GetComponent<Image>().color = new Color32(255, 255, 255, 255);

            System.DateTime postDate = System.DateTime.Now.AddDays(_day);
            date = postDate.Year.ToString() + "-" + postDate.Month.ToString() + "-" + postDate.Day;
           // Debug.Log(date);
        }

        /// <summary>
        /// 选择时间段
        /// </summary>
        public void SelectTimeDate(int _type)
        {
            if(_type == 0)
            {
                selectTimeBG_1st.color = new Color32(221,245,248,255);
                selectTimeText_1st.color = new Color32(118, 172, 178, 255);

                selectTimeBG_2nd.color = new Color32(229,229,229,255);
                selectTimeText_2nd.color = new Color32(178, 178, 178, 255);

                time_frame = 1;
            }
            else
            {
                selectTimeBG_2nd.color = new Color32(221, 245, 248, 255);
                selectTimeText_2nd.color = new Color32(118, 172, 178, 255);

                selectTimeBG_1st.color = new Color32(229, 229, 229, 255);
                selectTimeText_1st.color = new Color32(178, 178, 178, 255);

                time_frame = 2;
            }
                
        }

        /// <summary>
        /// 设置可选日期
        /// </summary>
        void InsertDateTime()
        {
            System.DateTime _date = System.DateTime.Now;
           Debug.Log(_date.Hour + "小时");
            if(_date.Hour <= 17)
                for (int i = 0; i < dayOfTimeParent.childCount;i++)
                {
                dayOfTimeParent.GetChild(i).Find("Text").GetComponent<Text>().text = _date.AddDays(i + 1).Month.ToString() + "月" + _date.AddDays(i + 1).Day + "日";
                    dayOfTimeParent.GetChild(i).GetComponent<DateTimeType>().addDay = i + 1;

                }
            else
                for (int i = 0; i < dayOfTimeParent.childCount; i++)
                {
                dayOfTimeParent.GetChild(i).Find("Text").GetComponent<Text>().text = _date.AddDays(i + 2).Month.ToString() + "月" + _date.AddDays(i + 2).Day + "日";
                    dayOfTimeParent.GetChild(i).GetComponent<DateTimeType>().addDay = i + 2;
                }

        }

        void ReturnMapSearch(GameObject go, PointerEventData data)
        {
            UniWebViewPluginManager.getInstance.ReturnSearchMap(cityText.text);
        }

        void ReSizeMapView(GameObject go,PointerEventData data)
        {
            //mapRectBig.GetComponent<CanvasGroup>().DOFade(1f, 2f);
            //mapRectBig.GetComponent<Image>()
            mapSearch.SetActive(true);
            mapSearch.GetComponent<CanvasGroup>().DOPause();
            mapSearch.GetComponent<CanvasGroup>().DOFade(1f, 1f);
            UniWebViewPluginManager.instance.LoadSearchMap(cityText.text);
           // UniWebViewPluginManager.getInstance.webView.ReferenceRectTransform = mapRectBig;
        }

//        IEnumerator PostMapMethod(string url)
//        {
           
//            WWW www = new WWW(url);
           
//            yield return www;
//            if (www.error != null)
//            {
                
//                UIManager.instance.ShowLoggerMsg("www错误:" + www.error);
//            }
//            else
//            {
//#if UNITY_EDITOR
//                Debug.Log(www.text);
//#endif
        //        JsonData result = JsonMapper.ToObject(www.text);
        //        JsonData data = result["results"];
        //        List<string> resultList = new List<string>();
        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            string name = data[i]["name"].ToString();
        //            Debug.Log(name);
        //        }
        //        Resources.UnloadUnusedAssets();
        //        System.GC.Collect();
        //    }
        //}

		void AppoimentBtnClick(GameObject go, PointerEventData eventdata)
		//void AppoimentBtnClick()
		{
			//Debug.Log ("预约");
            if(pNameText.text.Length == 0)
            {
                TipsFailedMethod("请设置小区!");
                return;
            }
            else if (addressInput.text.Length == 0)
            {
                TipsFailedMethod("请输入详细地址!");
                return;
            }
            else if (personNameInput.text.Length == 0)
            {
                TipsFailedMethod("请输入业主姓名!");
                return;
            }
            else if (telephoneInput.text.Length == 0)
            {
                TipsFailedMethod("请输入业主电话!");
                return;
            }
            else if (dataTimeText.text.Length == 0)
            {
                TipsFailedMethod("请设置预约上门时间!");
                return;
            }
			else if (backCall == "1") {
				TipsFailedMethod(UtilManager.getInstance.CreateAppoimentHasBackCall);
				return;
			}
            else 
			{
				//Debug.Log (cityDropDown.captionText.text);
				//UserData.instance.token = "mwpVJ5K0tjojPkaL";
               // UtilManager.getInstance.timeStr = "2019-5-22 12:53:50";
				//if(UtilManager.getInstance.timeStr != "0")
				//	GameObject.Find ("Camera").GetComponent<InterfaceManager> ().CreateAppoiment (UtilManager.getInstance.areaID, (int)EnumManager.appoimentType.check,
    //                                                                                              pNameText.text, addressInput.text, personNameInput.text, telephoneInput.text, Util.DateTimeToTimeStamp (UtilManager.getInstance.timeStr), remarkInput.text);
				//else
					//GameObject.Find ("Camera").GetComponent<InterfaceManager> ().CreateAppoiment (UtilManager.getInstance.areaID, (int)EnumManager.appoimentType.check,
                                                                                                  //pNameText.text, addressInput.text, personNameInput.text, telephoneInput.text, int.Parse(UtilManager.getInstance.timeStr), remarkInput.text);
                GameObject.Find("Camera").GetComponent<InterfaceManager>().CreateAppoiment(UtilManager.getInstance.areaID, (int)EnumManager.appoimentType.check,
                                                                                           pNameText.text, addressInput.text, personNameInput.text, telephoneInput.text, date,time_frame,remarkInput.text);

			}
		}

		void OnCreateAppoiment(object[] data)
		{
			//Debug.Log (data [0] + "pppppppppppp");
			if (data [0].ToString() == "False")
			{
				//Debug.Log ("进入了false");
				TipsFailedMethod (UtilManager.instance.NormalError);
			} else if(data [0].ToString() == "True")
			{
				TipsSuccessMethod ();
			}
		}

		void BackCallMethodNo(GameObject go, PointerEventData eventdata)
		{
			if (!backCallNoObj.activeInHierarchy)
			{
				backCallNoObj.SetActive (true);
				backCallYesObj.SetActive (false);
				backCall = "0";
			}

		}

		void BackCallMethodYes(GameObject go, PointerEventData eventdata)
		{
			if (!backCallYesObj.activeInHierarchy)
			{
				backCallNoObj.SetActive (false);
				backCallYesObj.SetActive (true);
				backCall = "1";
			}
		}

		void TipsSuccessMethod()
		{
			tipsSuccess.SetActive (true);
			mask.SetActive (true);

            pNameText.text = "";
			addressInput.text = "";
			personNameInput.text = "";
			telephoneInput.text = "";
            dataTimeText.text = "";
			remarkInput.text = "";
            date = "";
            time_frame = 0;
		}

		void TipsFailedMethod(string _msg)
		{
			//Debug.Log ("进入了errorMsg");
			tipsFailed.SetActive (true);
			tipsFailedText.text = _msg;
			mask.SetActive (true);
		}

		void AppoimentSuccessClick(GameObject go, PointerEventData eventdata)
		{
			tipsSuccess.SetActive (false);
			mask.SetActive (false);
		}

		void AppoimentFailedClick(GameObject go, PointerEventData eventdata)
		{
			tipsFailed.SetActive (false);
			mask.SetActive (false);
		}

		/// <summary>
		/// 调用ios时间组件
		/// </summary>
		void CallIosDataTimeClick(GameObject go, PointerEventData eventdata)
		{
#if UNITY_IOS
            //Debug.Log ("callIos");
            maskWhite.SetActive (true);
			iosDataTimeBtn.gameObject.SetActive (true);
			if (!UtilManager.getInstance.isCalledDataTime)
				IosMethodManager.CallIosDataTime ();
			else
				IosMethodManager.ShowIosDataTime ();
#endif
		}

		/// <summary>
		/// 关闭ios时间组件
		/// </summary>
		void CloseIosDataTimeClick(GameObject go, PointerEventData eventdata)
		{
            #if UNITY_IOS
			IosMethodManager.HideIosDataTime ();
			maskWhite.SetActive (false);
			iosDataTimeBtn.gameObject.SetActive (false);
			SplitDataTime ();
#endif

		}

        void CallAndroidTimeClick(GameObject go,PointerEventData eventData)
        {
            AndroidMethodManager.CallAndroidDataTime();
        }

		/// <summary>
		/// 设置app端时间label
		/// </summary>
		public void SplitDataTime()
		{
			
			string year = UtilManager.getInstance.timeStr.Split(' ')[0].Split('-')[0];
			string month = UtilManager.getInstance.timeStr.Split(' ')[0].Split('-')[1];
			string day = UtilManager.getInstance.timeStr.Split(' ')[0].Split('-')[2];
			string hour = UtilManager.getInstance.timeStr.Split(' ')[1].Split('-')[0];
			string min = UtilManager.getInstance.timeStr.Split(' ')[1].Split('-')[1];
			string sec = UtilManager.getInstance.timeStr.Split(' ')[1].Split('-')[2];
			dataTimeText.text =  year + "年" + month + "月" + day + "日" + hour + "时" + min + "分";
		}


	}
}

