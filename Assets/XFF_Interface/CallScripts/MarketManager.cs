using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;
using GD;

namespace LM_Workspace
{
    public class MarketManager : SingletonMono<MarketManager>
    {
        private GameObject mainPaenl;
        private GameObject detailPanel;
        private GameObject orderPanel;
        private GameObject statePanel;
        private InterfaceManager interfaceManager;

        private UIEventListener closeBtn;
        private UIEventListener gongjuBtn;
        private UIEventListener jiajuBtn;
        private UIEventListener liquanBtn;
        private GameObject sliderGo;
        private GameObject itemParent;
        private GameObject goodsItem;
        private List<GameObject> goodItemList = new List<GameObject>();

		public int curTapIdx = 1;
        private int curPage = 1;//第几页
        private int totalPage = 1;//每页多少条
        private int total = 0;//总共的商品数


        private UIEventListener detailCloseBtn;//详情面板关闭按钮
        private RawImage goodImage;//图片
        private Text goodTitle;//商品标题
        private Text goodPrice;//商品价格
        private Text refrencePrice;//参考价格
        private UIEventListener exchangeBtn;//兑换按钮
        private Good detailGood;//展示的商品对象

        private UIEventListener orderCloseBtn;//订单界面关闭按钮
        private Text pointLabel;
        private InputField nameLabel;
        private InputField addressLabel;
        private InputField phoneLabel;
        private InputField remarkLabel;
        private UIEventListener sendOrderBtn;

        private UIEventListener closeStateBtn;//关闭订单状态按钮
        private UIEventListener successBtn;//兑换成功按钮

		private bool isReturn;



        // Use this for initialization
        protected override void Awake()
        {
            mainPaenl = Util.FindChildByName(gameObject, "p_chaoshi");
            detailPanel = Util.FindChildByName(gameObject, "p_details");
            orderPanel = Util.FindChildByName(gameObject, "p_orderInfo");
            statePanel = Util.FindChildByName(gameObject, "p_state");
            interfaceManager = GameObject.Find("Camera").GetComponent<InterfaceManager>();

            #region mainPanel
            closeBtn = Util.FindChildByName(mainPaenl, "header/btn_back").GetComponent<UIEventListener>();
            gongjuBtn = Util.FindChildByName(mainPaenl, "docPanel/menubar/btngroup/btn_gongju").GetComponent<UIEventListener>();
            jiajuBtn = Util.FindChildByName(mainPaenl, "docPanel/menubar/btngroup/btn_jiaju").GetComponent<UIEventListener>();
            liquanBtn = Util.FindChildByName(mainPaenl, "docPanel/menubar/btngroup/btn_liquan").GetComponent<UIEventListener>();

            sliderGo = Util.FindChildByName(mainPaenl, "docPanel/menubar/slider");
            itemParent = Util.FindChildByName(mainPaenl, "docPanel/scrolloffset/content");

			goodsItem = Resources.Load ("shangpin_item") as GameObject;

            gongjuBtn.onClick = OnClickGongJuTap;
            jiajuBtn.onClick = OnClickJiaJuTap;
            liquanBtn.onClick = OnClickLiQuanTap;
            closeBtn.onClick = OnClickCloseBtn;

            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetGoodsList, ShowGoodsList);
            #endregion

            #region detailPanel
            detailCloseBtn = Util.FindChildByName(detailPanel, "header/btn_back").GetComponent<UIEventListener>();
            goodImage = Util.FindChildByName(detailPanel, "docPanel/shangpin_item/mask/RawImage").GetComponent<RawImage>();
            goodTitle = Util.FindChildByName(detailPanel, "docPanel/title").GetComponent<Text>();
            goodPrice = Util.FindChildByName(detailPanel, "docPanel/shangpin_item/Text").GetComponent<Text>();
            refrencePrice = Util.FindChildByName(detailPanel, "docPanel/subTitle").GetComponent<Text>();
            exchangeBtn = Util.FindChildByName(detailPanel, "docPanel/buttonPrefab").GetComponent<UIEventListener>();
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetGoodsDetail, ShowGoodDetial);
            detailCloseBtn.onClick = OnClickCloseDetail;
            exchangeBtn.onClick = OnClickExchange;
            #endregion

            #region orderPanel
            orderCloseBtn = Util.FindChildByName(orderPanel, "header/btn_back").GetComponent<UIEventListener>();
            sendOrderBtn = Util.FindChildByName(orderPanel, "docPanel/buttonPrefab").GetComponent<UIEventListener>();
            pointLabel = Util.FindChildByName(orderPanel, "docPanel/topbar/layout/subTitle").GetComponent<Text>();

            nameLabel = Util.FindChildByName(orderPanel, "docPanel/infoLayout/item_0/input").GetComponent<InputField>();
            addressLabel = Util.FindChildByName(orderPanel, "docPanel/infoLayout/item_1/input").GetComponent<InputField>();
            phoneLabel = Util.FindChildByName(orderPanel, "docPanel/infoLayout/item_2/input").GetComponent<InputField>();
            remarkLabel = Util.FindChildByName(orderPanel, "docPanel/infoLayout/item_3/input").GetComponent<InputField>();

            orderCloseBtn.onClick = OnClickCloseOrderPanel;
            sendOrderBtn.onClick = OnClickSendOrder;
            #endregion

            closeStateBtn = Util.FindChildByName(statePanel, "header/btn_back").GetComponent<UIEventListener>();
            successBtn = Util.FindChildByName(statePanel, "header/btn_back").GetComponent<UIEventListener>();

            successBtn.onClick = OnClickStateCloseBtn;
            closeStateBtn.onClick = OnClickStateCloseBtn;

			EventManager.instance.RegisterEvent(XMWorkspace.Event.ExchangeGood, OnExchangeGood);


        }

        private void OnEnable()
        {
            StopRefreshGood();
            mainPaenl.SetActive(true);
            detailPanel.SetActive(false);
            orderPanel.SetActive(false);
            statePanel.SetActive(false);

            curPage = 1;
            curTapIdx = 1;
			interfaceManager.GetGoodsList(1, curPage, totalPage,true);

			isReturn = false;
        }

		private void OnDisable()
		{
			StopRefreshGood ();
		}

        /// <summary>
        /// 刷新商品列表
        /// </summary>
        /// <param name="data"></param>
        void ShowGoodsList(params object[] data)
        {
            bool state = bool.Parse(data[0].ToString());
            if (state)
            {
				List<Good> goodList = (List<Good>)data[1];
                total = int.Parse(data[2].ToString());
				if(gameObject.activeInHierarchy)
                	StartCoroutine(ShowGoodList(goodList));
            }
        }

        /// <summary>
        /// 刷新商品列表
        /// </summary>
        /// <param name="goodList"></param>
        /// <returns></returns>
        IEnumerator ShowGoodList(List<Good> goodList)
        {
			
				for (int i = 0; i < goodList.Count; i++)
			{
				if (goodList [i].catId != curTapIdx)
					break;
					GameObject go = GetGoodItem ();
					//go.GetComponent<GoodItemHelper> ().carId = goodList [i].catId;
					ShowGoodItem (go, goodList [i]);
					goodItemList.Add (go);
					yield return new WaitForEndOfFrame ();
					//yield return new WaitForSeconds(0.06f);

				}
				//刷新完这批数据还有的话继续向服务器请求
				if (curPage * totalPage < total) 
				{
					curPage++;
				interfaceManager.GetGoodsList (curTapIdx, curPage, totalPage, true);
				}


//			for (int a = 0; a < itemParent.transform.childCount; a++) 
//			{
//				if (itemParent.transform.GetChild (a).GetComponent<GoodItemHelper> ().carId != curTapIdx)
//				{
//					Destroy (itemParent.transform.GetChild (a).gameObject);
//					goodItemList.Remove (itemParent.transform.GetChild (a).gameObject);
//				}
//			}
        }

        /// <summary>
        /// 显示单独的商品
        /// </summary>
        void ShowGoodItem(GameObject go, Good good)
        {
            Util.FindChildByName(go, "title").GetComponent<Text>().text = good.title;
            Util.FindChildByName(go, "Text").GetComponent<Text>().text = good.price.ToString() + " 积分";
            RawImage image = Util.FindChildByName(go, "mask/RawImage").GetComponent<RawImage>();
            ClickGoodItem(go, good.id);
			image.GetComponent<SelfImageHelper> ().imageUrl = good.thumb;
//            StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(good.thumb, 298, 298, delegate
//             {
//					//goodItemList.Remove(go);
//                 return image;
//             }));
        }

        /// <summary>
        /// 获取商品的预制体
        /// </summary>
        /// <returns></returns>
        GameObject GetGoodItem()
        {
            GameObject tmp = GameObject.Instantiate(goodsItem);
            tmp.transform.SetParent(itemParent.transform);
            tmp.transform.localScale = Vector3.one;
            tmp.transform.localPosition = Vector3.zero;
            tmp.SetActive(true);
            return tmp;
        }

        /// <summary>
        /// 点击商品
        /// </summary>
        /// <param name="go"></param>
        /// <param name="goodId"></param>
        void ClickGoodItem(GameObject go, int goodId)
        {
            go.GetComponent<UIEventListener>().onClick = delegate {
                detailPanel.SetActive(true);
                interfaceManager.GetGoodsDetail(goodId);
            };
        }

        /// <summary>
        /// 显示商品详情
        /// </summary>
        /// <param name="data"></param>
        void ShowGoodDetial(params object[] data)
        {
            bool state = bool.Parse(data[0].ToString());
            if (state)
            {
                Good good = (Good)data[1];
                detailGood = good;
                goodTitle.text = good.title;
                goodPrice.text = good.price + " 积分";
                refrencePrice.text = "市场参考价:" + good.referencePrice + "元";

                //UIManager.instance.LoadImageFromUrl(good.details, 700, 0, delegate {
                //    return goodImage;
                //}, delegate {
                //    return goodImage.GetComponent<LayoutElement>();

                //});

                StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(good.details +
                    "?imageView2/2/w/590/h/550", 590, 550, delegate
                {
                    return goodImage;
                }));
            }
        }

        /// <summary>
        /// 关闭商品详情界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickCloseDetail(GameObject go,PointerEventData data)
        {
			//goodImage.texture = null;
            detailPanel.SetActive(false);

        }

        void OnClickExchange(GameObject go, PointerEventData data)
        {
//            orderPanel.SetActive(true);
//            return;
			Debug.Log(UserData.instance.point);
            if (UserData.instance.point < detailGood.price)
            {
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "积分不足!");
                return;
            }
            else
            {
                orderPanel.SetActive(true);
                pointLabel.text ="合计:"+ detailGood.price.ToString()+" 积分";
            }
        }

        void OnClickSendOrder(GameObject go, PointerEventData data)
        {
            string userName = nameLabel.text;
            if (userName == "")
            {
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "收件人不能为空!");
                return;
            }

            string address = addressLabel.text;
            if (address == "")
            {
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "地址不能为空!");
                return;
            }

            string phone = phoneLabel.text;
            if (phone == "")
            {
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "联系方式不能为空!");
                return;
            }
            if (phone.Length!=11)
            {
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "手机号码长度不符合格式!");
                return;
            }

            string remark = remarkLabel.text;

			interfaceManager.ExchangeGood(detailGood.id, userName, address, phone, remark);
        }

		/// <summary>
		/// 显示兑换结果
		/// </summary>
		/// <param name="data"></param>
		void OnExchangeGood(object[] data)
		{
			bool state = bool.Parse (data [0].ToString ());
			if (state) 
			{
				//statePanel.SetActive(true);
				Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "兑换成功,等待快递来敲门!");
			}
			else
				Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "兑换失败!");
			orderPanel.SetActive (false);
		}

        /// <summary>
        /// 点击关闭积分超市
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickCloseBtn(GameObject go, PointerEventData data)
        {

        }

        /// <summary>
        /// 点击工具标签
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickGongJuTap(GameObject go, PointerEventData data)
        {
            if (curTapIdx == 1)
                return;
            curTapIdx = 1;
            curPage = 1;
            StopRefreshGood();

            gongjuBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.9f, 0.9f, 0.9f);
            jiajuBtn.GetComponent<SVGImporter.SVGImage>().color = Color.white;
            liquanBtn.GetComponent<SVGImporter.SVGImage>().color = Color.white;

			interfaceManager.GetGoodsList(1, curPage, totalPage,true);
            (sliderGo.transform as RectTransform).anchoredPosition = new Vector3(-230, -75, 0);
        }

        /// <summary>
        /// 点击家具标签
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickJiaJuTap(GameObject go, PointerEventData data)
        {
            if (curTapIdx == 2)
                return;
            curTapIdx = 2;
            curPage = 1;
            StopRefreshGood();

            gongjuBtn.GetComponent<SVGImporter.SVGImage>().color = Color.white;
            jiajuBtn.GetComponent<SVGImporter.SVGImage>().color = new Color(0.9f, 0.9f, 0.9f);
            liquanBtn.GetComponent<SVGImporter.SVGImage>().color = Color.white;

			interfaceManager.GetGoodsList(2, curPage, totalPage,true);
            (sliderGo.transform as RectTransform).anchoredPosition = new Vector3(0, -75, 0);
        }

        /// <summary>
        /// 点击礼券标签
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickLiQuanTap(GameObject go, PointerEventData data)
        {
            Debug.Log("onclickLiquanTap");
            if (curTapIdx == 3)
                return;
            curTapIdx = 3;
            curPage = 1;
            StopRefreshGood();


			gongjuBtn.GetComponent<SVGImporter.SVGImage> ().color = Color.white;
			jiajuBtn.GetComponent<SVGImporter.SVGImage> ().color = Color.white;
			liquanBtn.GetComponent<SVGImporter.SVGImage> ().color = new Color (0.9f, 0.9f, 0.9f);
			interfaceManager.GetGoodsList (3, curPage, totalPage, true);
			(sliderGo.transform as RectTransform).anchoredPosition = new Vector3 (230, -75, 0);

        }
        
        /// <summary>
        /// 停止刷新商品列表
        /// </summary>
		void StopRefreshGood()
        {
			isReturn = true;
            StopAllCoroutines();
            for (int i = 0; i < goodItemList.Count; i++)
            {
				GameObject.Destroy(goodItemList[i]);
            }
            goodItemList.Clear();

        }

//        /// <summary>
//        /// 商品兑换结果
//        /// </summary>
//        /// <param name="data"></param>
//        void OnExchangeGood(params object[] data)
//        {
//            bool state = (bool)data[0];
//            if (state)
//            {
//                statePanel.SetActive(true);
//            }
//            else
//            {
//                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "兑换失败!");
//            }
//        }

        /// <summary>
        /// 点击关闭订单结果按钮
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        void OnClickStateCloseBtn(GameObject go, PointerEventData data)
        {
            OnEnable();
        }

        void OnClickCloseOrderPanel(GameObject go, PointerEventData data)
        {
            orderPanel.SetActive(false);
        }
      
    }
}
