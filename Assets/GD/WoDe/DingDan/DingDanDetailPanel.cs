using LM_Workspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;
using System.Linq;

namespace GD
{
    /// <summary>
    /// 订单细节显示界面
    /// 
    /// </summary>
    public class DingDanDetailPanel : MonoBehaviour
    {    /// <summary>
         /// 显示的订单信息
         /// </summary>
        protected Order currentShowOrder;


        private UIEventListener back;
        private UIEventListener detail_1;
        private UIEventListener detail_2;
        private GameObject detail_1_panel;
        private GameObject detail_2_panel;

        #region 细节图1
        private UIEventListener menpaiBtn;

        private UIEventListener stateBtn;

        private UIEventListener chufangBtn;

        private UIEventListener weishengjianBtn;

        private UIEventListener ketingBtn;

        private UIEventListener guodaoBtn;

        private UIEventListener yangtaiBtn;

        private UIEventListener otherBtn;

        private UIEventListener pingzhengBtn;

        private UIEventListener buhegeBtn;

        private UIEventListener next;

        private Transform buhegeLabel;

        private Text buhegeText;

        #region 图片父物体
        /// <summary>
        /// 门牌
        /// </summary>
        private Transform menpaiParent;

        /// <summary>
        /// 试压仪状态
        /// </summary>
        private Transform stateParent;

        /// <summary>
        /// 厨房
        /// </summary>
        private Transform chufangParent;

        /// <summary>
        /// 卫生间
        /// </summary>
        private Transform weishengjianParent;

        /// <summary>
        /// 客厅
        /// </summary>
        private Transform ketingParent;

        /// <summary>
        /// 过道
        /// </summary>
        private Transform guodaoParent;

        /// <summary>
        /// 阳台
        /// </summary>
        private Transform yangtaiParent;

        /// <summary>
        /// 其他
        /// </summary>
        private Transform otherParent;

        /// <summary>
        /// 凭证
        /// </summary>
        private Transform pingzhengParent;

        /// <summary>
        /// 不合格
        /// </summary>
        private Transform buhegeParent;
        #endregion
        #endregion
        #region 细节图2
        private InputField yeZhuName;
        private InputField yeZhuPhone;
        private InputField yeZhuAddress;
        private InputField gouMaidiDian;

        private MyTriggle anZhuangYuYue;
        private InputField anZhuangName;
        private InputField anZhuangPhone;
        private Text fangWuLeiXing;
        private Text chu;
        private Text wei;
        private InputField kaiFaShang;
        private InputField jiaZhuang;
        private InputField nuanTong;
        private InputField kongTiao;
        private Text chanPinType;
        private InputField anZhuangChangDu;

        private GameObject wuDing;
        private GameObject diMian;
        private GameObject qiangMian;

        private GameObject xianChangGanJing;
        private GameObject guanDaoGuDing;
        private GameObject guanDaoWuYaMai;
        private InputField beiZhu;

        private GameObject guiFan1;
        private GameObject guiFan2;
        private GameObject guiFan3;

        private GameObject jieKouShenLou1;
        private GameObject jieKouShenLou2;
        private GameObject jieKouShenLou3;
        private GameObject jieKouShenLou4;
        private GameObject jieKouShenLou5;

        private GameObject hanDianShenLou1;
        private GameObject hanDianShenLou2;
        private GameObject hanDianShenLou3;
        private GameObject hanDianShenLou4;
        private GameObject hanDianShenLou5;

        private GameObject hanDianFanBian1;
        private GameObject hanDianFanBian2;
        private GameObject hanDianFanBian3;
        private GameObject hanDianFanBian4;
        private GameObject hanDianFanBian5;

        private InputField baoYaStartTime;
        private InputField baoYaEndTime;



        private Text yunXingYaLi;
        private Text jianCeYaLi;
        private InputField shiYaName;
        private InputField shiYaPhone;

        private GameObject zhuYi1;
        private GameObject zhuYi2;
        private GameObject zhuYi3;
        private GameObject zhuYi4;


        private GameObject gongjiangCardItem_43;
        private GameObject gongjiangCardItem_44;
        private GameObject gongjiangCard1;
        private GameObject gongjiangCard2;
        private GameObject gongjiangCard3;

        private GameObject heGe;
        private GameObject buHeGe;
        //private MyTriggle buHeGe;

        private UIEventListener enter;

        #endregion

        /// <summary>
        /// 图片数组的prefab
        /// </summary>
        private GameObject imageArrayItem;

        //RawImage currentImage;
        //Dictionary<string, string> photoKey = new Dictionary<string, string>();
        void Init()
        {
           // EventManager.instance.RegisterEvent(XMWorkspace.Event.CheckBuilder, OnCheckBuilder);
          //  EventManager.instance.RegisterEvent(XMWorkspace.Event.TakeTest, OnTakeTestPressure);
            back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = delegate { Hide(); };

            detail_1 = transform.Find("menu_bar").Find("layout").Find("btn_1").GetComponent<UIEventListener>();
            detail_2 = transform.Find("menu_bar").Find("layout").Find("btn_2").GetComponent<UIEventListener>();


            detail_1_panel = transform.Find("menu_1_detail").gameObject;
            detail_2_panel = transform.Find("menu_2_detail").gameObject;
            detail_1.onClick = delegate { ChangeDetailPanel(0); };
            detail_2.onClick = delegate { ChangeDetailPanel(1); };

            //next = detail_1_panel.transform.Find("content").Find("button").Find("buttonPrefab").GetComponent<UIEventListener>();
            //next.onClick = delegate { ChangeDetailPanel(1); };
            #region 界面2
           // enter = transform.Find("menu_2_detail").Find("content").Find("button").Find("buttonPrefab").GetComponent<UIEventListener>();

          //  enter.onClick = PosOrderInfo;

            #endregion


            menpaiBtn = transform.Find("menu_1_detail").Find("content").Find("item_1").GetComponent<UIEventListener>();
            stateBtn = transform.Find("menu_1_detail").Find("content").Find("item_2").GetComponent<UIEventListener>();
            chufangBtn = transform.Find("menu_1_detail").Find("content").Find("item_3").GetComponent<UIEventListener>();
            weishengjianBtn = transform.Find("menu_1_detail").Find("content").Find("item_4").GetComponent<UIEventListener>();
            ketingBtn = transform.Find("menu_1_detail").Find("content").Find("item_5").GetComponent<UIEventListener>();
            guodaoBtn = transform.Find("menu_1_detail").Find("content").Find("item_6").GetComponent<UIEventListener>();
            yangtaiBtn = transform.Find("menu_1_detail").Find("content").Find("item_7").GetComponent<UIEventListener>();
            otherBtn = transform.Find("menu_1_detail").Find("content").Find("item_8").GetComponent<UIEventListener>();
            pingzhengBtn = transform.Find("menu_1_detail").Find("content").Find("item_9").GetComponent<UIEventListener>();
            buhegeBtn = transform.Find("menu_1_detail").Find("content").Find("item_10").GetComponent<UIEventListener>();


            menpaiParent = Util.FindChildByName(gameObject, "menu_1_detail/content/menpaiArray").transform;
            stateParent = Util.FindChildByName(gameObject, "menu_1_detail/content/stateArray").transform;
            chufangParent = Util.FindChildByName(gameObject, "menu_1_detail/content/chufangArray").transform;
            weishengjianParent = Util.FindChildByName(gameObject, "menu_1_detail/content/weishengjianArray").transform;
            ketingParent = Util.FindChildByName(gameObject, "menu_1_detail/content/ketingArray").transform;
            guodaoParent = Util.FindChildByName(gameObject, "menu_1_detail/content/guodaoArray").transform;
            yangtaiParent = Util.FindChildByName(gameObject, "menu_1_detail/content/yangtaiArray").transform;
            otherParent = Util.FindChildByName(gameObject, "menu_1_detail/content/otherArray").transform;
            pingzhengParent = Util.FindChildByName(gameObject, "menu_1_detail/content/pingzhengArray").transform;
            buhegeParent = Util.FindChildByName(gameObject, "menu_1_detail/content/buhegeArray").transform;

            imageArrayItem = Resources.Load("imageArrayItem") as GameObject;

            buhegeLabel = transform.Find("menu_1_detail").Find("content").Find("item_10_Label");
            buhegeText = buhegeLabel.GetChild(0).GetComponent<Text>();
        }


        private void DestroyChild(Transform _parent)
        {
            foreach (RectTransform trans in _parent)
            {
                Destroy(trans.gameObject);
            }
        }



        /// <summary>
        /// 退出详情界面
        /// </summary>
        void Hide()
        {
            gameObject.SetActive(false);

            ChangeDetailPanel(0);
            DestroyChild(menpaiParent);
            DestroyChild(stateParent);
            DestroyChild(chufangParent);
            DestroyChild(weishengjianParent);
            DestroyChild(ketingParent);
            DestroyChild(guodaoParent);
            DestroyChild(yangtaiParent);
            DestroyChild(otherParent);
            DestroyChild(pingzhengParent);
            DestroyChild(buhegeParent);
        }

        void ShowOrHideImage(Transform trans,UIEventListener icon )
        {
            if (trans.gameObject.activeSelf)
            {
                trans.gameObject.SetActive(false);
                icon.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            }
            else
            {
                trans.gameObject.SetActive(true);
                icon.transform.Find("btn_open").localEulerAngles = Vector3.zero;

            }

        }

        void LoadImage(Transform _parent,string url)
        {
           // Debug.Log(url + "99999");
            UtilManager.getInstance.loadingPanel.SetActive(true);
            Transform itemTrans = Instantiate(imageArrayItem).transform;
            itemTrans.SetParent(_parent);
            RectTransform rect = itemTrans.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.anchoredPosition3D = Vector3.zero;
            RawImage view = itemTrans.GetComponentInChildren<RawImage>();
            UIManager.instance.LoadImageFromUrl(url, 700, 0, delegate {
                UtilManager.getInstance.loadingPanel.SetActive(false);
                return view;
                // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            },delegate {

                return view.transform.parent.GetComponent<LayoutElement>();
            });
            ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
            //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(type, 700, 300, delegate {

            //    return view;
            //    // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            //}));

        }

        void ForeachImageList(List<string> _list,Transform _parent)
        {
            if (_list.Count <= 0)
                return;
            foreach (string item in _list)
            {
                LoadImage(_parent, item);
            }
        }

        private IEnumerator ForEachImage(Order data)
        {
            ForeachImageList(data.menpaiList, menpaiParent);
            ForeachImageList(data.stateList, stateParent);
            ForeachImageList(data.chufangList, chufangParent);
            ForeachImageList(data.weishengjianList, weishengjianParent);
            ForeachImageList(data.ketingList, ketingParent);
            ForeachImageList(data.guodaoList, guodaoParent);
            ForeachImageList(data.yangtaiList, yangtaiParent);
            ForeachImageList(data.otherList, otherParent);
            ForeachImageList(data.pingzhengList, pingzhengParent);
            ForeachImageList(data.buhegeList, buhegeParent);


            yield return new WaitForEndOfFrame();
            //UtilManager.getInstance.loadingPanel.SetActive(false);

        }
       public  void Show(Order data)
        {
            currentShowOrder = data;
            gameObject.SetActive(true);

            Init();

            DestroyChild(menpaiParent);
            DestroyChild(stateParent);
            DestroyChild(chufangParent);
            DestroyChild(weishengjianParent);
            DestroyChild(ketingParent);
            DestroyChild(guodaoParent);
            DestroyChild(yangtaiParent);
            DestroyChild(otherParent);
            DestroyChild(pingzhengParent);
            DestroyChild(buhegeParent);

            UtilManager.getInstance.isBigImageShowDestroyBtn = false;
            UtilManager.getInstance.loadingText.text = UtilManager.getInstance.LoadingPostStr;

            //Debug.Log("cardType   " + currentShowOrder.cardType);
           

            #region 界面1

           // Debug.Log(menpaiBtn);
            menpaiBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            stateBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            chufangBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            weishengjianBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            ketingBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            guodaoBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            yangtaiBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            otherBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            pingzhengBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;
            buhegeBtn.transform.Find("btn_open").localEulerAngles = Vector3.forward * 90;

            menpaiParent.gameObject.SetActive(false);
            stateParent.gameObject.SetActive(false);
            chufangParent.gameObject.SetActive(false);
            weishengjianParent.gameObject.SetActive(false);
            ketingParent.gameObject.SetActive(false);
            guodaoParent.gameObject.SetActive(false);
            yangtaiParent.gameObject.SetActive(false);
            otherParent.gameObject.SetActive(false);
            pingzhengParent.gameObject.SetActive(false);
            buhegeParent.gameObject.SetActive(false);


            menpaiBtn.onClick = delegate {
                ShowOrHideImage(menpaiParent, menpaiBtn);
            };
            stateBtn.onClick = delegate {
                ShowOrHideImage(stateParent, stateBtn);
            };
            chufangBtn.onClick = delegate {
                ShowOrHideImage(chufangParent, chufangBtn);
            };
            weishengjianBtn.onClick = delegate {
                ShowOrHideImage(weishengjianParent, weishengjianBtn);
            };
            ketingBtn.onClick = delegate {
                ShowOrHideImage(ketingParent, ketingBtn);
            };
            guodaoBtn.onClick = delegate {
                ShowOrHideImage(guodaoParent, guodaoBtn);
            };
            yangtaiBtn.onClick = delegate {
                ShowOrHideImage(yangtaiParent, yangtaiBtn);
            };
            otherBtn.onClick = delegate {
                ShowOrHideImage(otherParent, otherBtn);
            };
            pingzhengBtn.onClick = delegate {
                ShowOrHideImage(pingzhengParent, pingzhengBtn);
            };
            buhegeBtn.onClick = delegate {
                ShowOrHideImage(buhegeParent, buhegeBtn);
            };

            buhegeText.text = currentShowOrder.buhegeLabel;


            #endregion


            //业主姓名
            if (yeZhuName == null)
            {
                yeZhuName = transform.Find("menu_2_detail").Find("content").Find("item_1").Find("InputField").GetComponent<InputField>();
                yeZhuName.interactable = false;
            }
            yeZhuName.text = currentShowOrder.name;
            //业主电话
            if (yeZhuPhone == null)
            {
                yeZhuPhone = transform.Find("menu_2_detail").Find("content").Find("item_2").Find("InputField").GetComponent<InputField>();
            }
            yeZhuPhone.interactable = false;
            yeZhuPhone.text = currentShowOrder.phone;
            //业主地址
            if (yeZhuAddress == null)
            {
                yeZhuAddress = transform.Find("menu_2_detail").Find("content").Find("item_3").Find("InputField").GetComponent<InputField>();
            }
            yeZhuAddress.interactable = false;
            yeZhuAddress.text = currentShowOrder.address;

            //购买地点
            if (gouMaidiDian == null)
            {
                gouMaidiDian = transform.Find("menu_2_detail").Find("content").Find("item_4").Find("InputField").GetComponent<InputField>();
            }
            gouMaidiDian.interactable = false;
            gouMaidiDian.text = currentShowOrder.testBuyPlace;


            if (anZhuangName == null)
            {
                anZhuangName = transform.Find("menu_2_detail").Find("content").Find("item_5").Find("InputField").GetComponent<InputField>();

            }
            anZhuangName.interactable = false;
            anZhuangName.text = currentShowOrder.buildName;
            if (anZhuangPhone == null)
            {
                anZhuangPhone = transform.Find("menu_2_detail").Find("content").Find("item_6").Find("InputField").GetComponent<InputField>();

            }
            anZhuangPhone.interactable = false;
            anZhuangPhone.text = currentShowOrder.buildPhone;


            if (fangWuLeiXing == null)
                fangWuLeiXing = transform.Find("menu_2_detail").Find("content").Find("item_7").Find("dropdown").Find("Text").GetComponent<Text>();

            switch (currentShowOrder.testHouseType)
            {
                case "1":
                    fangWuLeiXing.text = "别墅"; break;

                case "2": fangWuLeiXing.text = "商品房"; break;
                case "3": fangWuLeiXing.text = "安置房"; break;
                case "4": fangWuLeiXing.text = "车库"; break;
            }

            if (chu == null)
                chu = transform.Find("menu_2_detail").Find("content").Find("item_8").Find("dropdown_chu").Find("Text").GetComponent<Text>();
            chu.text = currentShowOrder.testKitchenType;

            if (wei == null)
                wei = transform.Find("menu_2_detail").Find("content").Find("item_8").Find("dropdown_wei").Find("Text").GetComponent<Text>();
            wei.text = currentShowOrder.testToiletType;

            if (kaiFaShang == null)
                kaiFaShang = transform.Find("menu_2_detail").Find("content").Find("item_9").Find("InputField").GetComponent<InputField>();
            kaiFaShang.interactable = false;
            kaiFaShang.text = currentShowOrder.testDeveloper;

            if (jiaZhuang == null)
                jiaZhuang = transform.Find("menu_2_detail").Find("content").Find("item_10").Find("InputField").GetComponent<InputField>();
            jiaZhuang.interactable = false;
            jiaZhuang.text = currentShowOrder.testDecoration;
            if (nuanTong == null)
                nuanTong = transform.Find("menu_2_detail").Find("content").Find("item_11").Find("InputField").GetComponent<InputField>();
            nuanTong.interactable = false;
            nuanTong.text = currentShowOrder.testHvac;
            if (kongTiao == null)
                kongTiao = transform.Find("menu_2_detail").Find("content").Find("item_12").Find("InputField").GetComponent<InputField>();
            kongTiao.interactable = false;
            kongTiao.text = currentShowOrder.testAir;

            if (chanPinType == null)
                chanPinType = transform.Find("menu_2_detail").Find("content").Find("item_13").Find("dropdown").Find("Text").GetComponent<Text>();
            switch (currentShowOrder.testProductType)
            {
                case "1":
                    chanPinType.text = "纯净蓝";
                    break;
                case "2":
                    chanPinType.text = "3米白色精品家装管";
                    break;
                case "3":
                    chanPinType.text = "普通咖喱色PPR";
                    break;
                case "4":
                    chanPinType.text = "Z-HOME PPR";
                    break;
                case "5":
                    chanPinType.text = "无忧抗冻管";
                    break;
                case "6":
                    chanPinType.text = "SW康居管";
                    break;
                case "7":
                    chanPinType.text = "其他";
                    break;
                case "8":
                    chanPinType.text = "绿色家装管";
                    break;
                case "9":
                    chanPinType.text = "纯净蓝健康芯";
                    break;
                case "10":
                    chanPinType.text = "中财地暖";
                    break;                    
            }

            if (anZhuangChangDu == null)
                anZhuangChangDu = transform.Find("menu_2_detail").Find("content").Find("item_14").Find("InputField").GetComponent<InputField>();
            anZhuangChangDu.interactable = false;
            if (currentShowOrder.testLength == "0")
            {
                anZhuangChangDu.text = "";

            }
            else
            {
                anZhuangChangDu.text = currentShowOrder.testLength;
            }

            if (wuDing == null)
            {
                wuDing = transform.Find("menu_2_detail").Find("content").Find("item_15").Find("toggle_1").Find("circle").Find("state").gameObject;
            }
            if (diMian == null)
            {
                diMian = transform.Find("menu_2_detail").Find("content").Find("item_15").Find("toggle_2").Find("circle").Find("state").gameObject;
            }
            if (qiangMian == null)
            {
                qiangMian = transform.Find("menu_2_detail").Find("content").Find("item_15").Find("toggle_3").Find("circle").Find("state").gameObject;
            }
            switch (currentShowOrder.testLayingType)
            {
                case "1":
                    wuDing.SetActive(true);
                    diMian.SetActive(false);
                    qiangMian.SetActive(false);
                    break;
                case "2":
                    wuDing.SetActive(false);
                    diMian.SetActive(true);
                    qiangMian.SetActive(false);
                    break;
                case "3":
                    wuDing.SetActive(false);
                    diMian.SetActive(false);
                    qiangMian.SetActive(true);
                    break;
            }


            if (xianChangGanJing == null)
                xianChangGanJing = transform.Find("menu_2_detail").Find("content").Find("item_17").Find("layout").Find("toggle_1").Find("circle").Find("state").gameObject;
            if (guanDaoGuDing == null)
                guanDaoGuDing = transform.Find("menu_2_detail").Find("content").Find("item_17").Find("layout").Find("toggle_2").Find("circle").Find("state").gameObject;
            if (guanDaoWuYaMai == null)
                guanDaoWuYaMai = transform.Find("menu_2_detail").Find("content").Find("item_17").Find("layout").Find("toggle_3").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testPipeline[0] == '1')
            {
                xianChangGanJing.SetActive(true);
            } else
            {
                xianChangGanJing.SetActive(false);
            }

            if (currentShowOrder.testPipeline[1] == '1')
            {
                guanDaoGuDing.SetActive(true);
            }
            else
            {
                guanDaoGuDing.SetActive(false);
            }

            if (currentShowOrder.testPipeline[2] == '1')
            {
                guanDaoWuYaMai.SetActive(true);
            }
            else
            {
                guanDaoWuYaMai.SetActive(false);
            }


            if (beiZhu == null)
                beiZhu = transform.Find("menu_2_detail").Find("content").Find("item_18").Find("InputField").GetComponent<InputField>();
            beiZhu.interactable = false;
            beiZhu.text = currentShowOrder.testRemark;

            if (guiFan1 == null)
                guiFan1 = transform.Find("menu_2_detail").Find("content").Find("item_22").Find("toggle").Find("circle").Find("state").gameObject;

            if (currentShowOrder.testAssess[0] == '1')
                guiFan1.SetActive(true);
            else guiFan1.SetActive(false);

            if (guiFan2 == null)
                guiFan2 = transform.Find("menu_2_detail").Find("content").Find("item_23").Find("toggle").Find("circle").Find("state").gameObject;

            if (currentShowOrder.testAssess[1] == '1')
                guiFan2.SetActive(true);
            else guiFan2.SetActive(false);
            if (guiFan3 == null)
                guiFan3 = transform.Find("menu_2_detail").Find("content").Find("item_24").Find("toggle").Find("circle").Find("state").gameObject;

            if (currentShowOrder.testAssess[2] == '1')
                guiFan3.SetActive(true);
            else guiFan3.SetActive(false);


            if (jieKouShenLou1 == null)
                jieKouShenLou1 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_1").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testCompress[0] == '1')
                jieKouShenLou1.SetActive(true);
            else jieKouShenLou1.SetActive(false);

            if (jieKouShenLou2 == null)
                jieKouShenLou2 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_2").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testCompress[1] == '1')
                jieKouShenLou2.SetActive(true);
            else jieKouShenLou2.SetActive(false);
            if (jieKouShenLou3 == null)
                jieKouShenLou3 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_3").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testCompress[2] == '1')
                jieKouShenLou3.SetActive(true);
            else jieKouShenLou3.SetActive(false);
            if (jieKouShenLou4 == null)
                jieKouShenLou4 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_4").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testCompress[3] == '1')
                jieKouShenLou4.SetActive(true);
            else jieKouShenLou4.SetActive(false);
            if (jieKouShenLou5 == null)
                jieKouShenLou5 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_5").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testCompress[4] == '1')
                jieKouShenLou5.SetActive(true);
            else jieKouShenLou5.SetActive(false);


            if (hanDianShenLou1 == null)
                hanDianShenLou1 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_1").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeld[0] == '1')
                hanDianShenLou1.SetActive(true);
            else hanDianShenLou1.SetActive(false);

            if (hanDianShenLou2 == null)
                hanDianShenLou2 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_2").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeld[1] == '1')
                hanDianShenLou2.SetActive(true);
            else hanDianShenLou2.SetActive(false);

            if (hanDianShenLou3 == null)
                hanDianShenLou3 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_3").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeld[2] == '1')
                hanDianShenLou3.SetActive(true);
            else hanDianShenLou3.SetActive(false);

            if (hanDianShenLou4 == null)
                hanDianShenLou4 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_4").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeld[3] == '1')
                hanDianShenLou4.SetActive(true);
            else hanDianShenLou4.SetActive(false);
            if (hanDianShenLou5 == null)
                hanDianShenLou5 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_5").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeld[4] == '1')
                hanDianShenLou5.SetActive(true);
            else hanDianShenLou5.SetActive(false);



            if (hanDianFanBian1 == null)
                hanDianFanBian1 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_1").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeldCheck[0] == '1')
                hanDianFanBian1.SetActive(true);
            else hanDianFanBian1.SetActive(false);

            if (hanDianFanBian2 == null)
                hanDianFanBian2 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_2").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeldCheck[1] == '1')
                hanDianFanBian2.SetActive(true);
            else hanDianFanBian2.SetActive(false);

            if (hanDianFanBian3 == null)
                hanDianFanBian3 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_3").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeldCheck[2] == '1')
                hanDianFanBian3.SetActive(true);
            else hanDianFanBian3.SetActive(false);

            if (hanDianFanBian4 == null)
                hanDianFanBian4 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_4").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeldCheck[3] == '1')
                hanDianFanBian4.SetActive(true);
            else hanDianFanBian4.SetActive(false);

            if (hanDianFanBian5 == null)
                hanDianFanBian5 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_5").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testWeldCheck[4] == '1')
                hanDianFanBian5.SetActive(true);
            else hanDianFanBian5.SetActive(false);



            if (baoYaStartTime == null)
                baoYaStartTime = transform.Find("menu_2_detail").Find("content").Find("item_32").Find("layout").Find("left").Find("InputField").GetComponent<InputField>();
            baoYaStartTime.interactable = false;
            // baoYaStartTime.text = Util.StampToTimeString( currentShowOrder.testKeepStart);
            baoYaStartTime.text = currentShowOrder.testKeepStart;
            if (baoYaEndTime == null)
                baoYaEndTime = transform.Find("menu_2_detail").Find("content").Find("item_32").Find("layout").Find("right").Find("InputField").GetComponent<InputField>();
            baoYaEndTime.interactable = false;
            // baoYaEndTime.text = Util.StampToTimeString(currentShowOrder.testKeepEnd);
            baoYaEndTime.text = currentShowOrder.testKeepEnd;


            if (jianCeYaLi == null)
                jianCeYaLi = transform.Find("menu_2_detail").Find("content").Find("item_33").Find("dropdown").Find("Text").GetComponent<Text>();
            

            switch (currentShowOrder.testCheckPressure)
            {
                case "1": jianCeYaLi.text = "1兆帕"; break;
                case "2": jianCeYaLi.text = "0.8兆帕"; break;
                case "3": jianCeYaLi.text = "1.2兆帕"; break;
            }

            if (yunXingYaLi == null)
                yunXingYaLi = transform.Find("menu_2_detail").Find("content").Find("item_42").Find("dropdown").Find("Text").GetComponent<Text>();

            switch (currentShowOrder.testOperatePressure)
            {
                case "1": yunXingYaLi.text = "0.3兆帕"; break;
                case "2": yunXingYaLi.text = "0.4兆帕"; break;
                case "3": yunXingYaLi.text = "0.5兆帕"; break;
            }

            if (shiYaName == null)
                shiYaName = transform.Find("menu_2_detail").Find("content").Find("item_34").Find("InputField").GetComponent<InputField>();
            shiYaName.text = currentShowOrder.testUserName;
            shiYaName.interactable = false;

            if (shiYaPhone == null)
                shiYaPhone = transform.Find("menu_2_detail").Find("content").Find("item_35").Find("InputField").GetComponent<InputField>();
            shiYaPhone.text = currentShowOrder.testUserPhone;
            shiYaPhone.interactable = false;

            if (zhuYi1 == null)
                zhuYi1 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_1").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
           // Debug.Log(currentShowOrder.testNotice[2] + "hahaha");
            if (currentShowOrder.testNotice[0] == '1')
                zhuYi1.SetActive(true);
            else zhuYi1.SetActive(false);

            if (zhuYi2 == null)
                zhuYi2 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_2").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testNotice[1] == '1')
                zhuYi2.SetActive(true);
            else zhuYi2.SetActive(false);
            if (zhuYi3 == null)
                zhuYi3 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_3").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testNotice[2] == '1')
                zhuYi3.SetActive(true);
            else zhuYi3.SetActive(false);
            if (zhuYi4 == null)
                zhuYi4 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_4").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
            if (currentShowOrder.testNotice[3] == '1')
                zhuYi4.SetActive(true);
            else zhuYi4.SetActive(false);

            if (gongjiangCardItem_43 == null)
                gongjiangCardItem_43 = transform.Find("menu_2_detail").Find("content").Find("item_43").gameObject;
            if (gongjiangCardItem_44 == null)
                gongjiangCardItem_44 = transform.Find("menu_2_detail").Find("content").Find("item_44").gameObject;

            if (currentShowOrder.cardSwitch == "1")
            {
                gongjiangCardItem_43.SetActive(true);
                gongjiangCardItem_44.SetActive(true);
               
                if (gongjiangCard1 == null)
                    gongjiangCard1 = gongjiangCardItem_44.transform.Find("layout").Find("item_1").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
                // Debug.Log(currentShowOrder.testNotice[2] + "hahaha");
                if (currentShowOrder.cardType == "1")
                    gongjiangCard1.SetActive(true);
                else gongjiangCard1.SetActive(false);

                if (gongjiangCard2 == null)
                    gongjiangCard2 = gongjiangCardItem_44.transform.Find("layout").Find("item_2").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
                // Debug.Log(currentShowOrder.testNotice[2] + "hahaha");
                if (currentShowOrder.cardType == "2")
                    gongjiangCard2.SetActive(true);
                else gongjiangCard2.SetActive(false);

                if (gongjiangCard3 == null)
                    gongjiangCard3 = gongjiangCardItem_44.transform.Find("layout").Find("item_3").Find("layout").Find("lower").Find("toggle").Find("circle").Find("state").gameObject;
                // Debug.Log(currentShowOrder.testNotice[2] + "hahaha");
                if (currentShowOrder.cardType == "3")
                    gongjiangCard3.SetActive(true);
                else gongjiangCard3.SetActive(false);
            }
            if (currentShowOrder.cardSwitch == "0" || currentShowOrder.cardType == "0")
            {
                gongjiangCardItem_43.SetActive(false);
                gongjiangCardItem_44.SetActive(false);
            }

            if (heGe == null)
            {
                heGe = transform.Find("menu_2_detail").Find("content").Find("item_39").gameObject;
            }
            if (buHeGe == null)
            {
                buHeGe = transform.Find("menu_2_detail").Find("content").Find("item_40").gameObject;
            }
            if (currentShowOrder.orderStatus== "3")
            {
                heGe.SetActive(true);
                buHeGe.SetActive(false);
                HideBuHeGeOrder();
            }
            else
            {
                heGe.SetActive(false);
                buHeGe.SetActive(true);
                ShowBuHeGeOrder();
            }
            ChangeDetailPanel(0);
            StartCoroutine(ForEachImage(data));
        }

        int currentPanel = 0;
        /// <summary>
        /// 切换细节界面
        /// </summary>
        /// <param name="index"></param>
        void ChangeDetailPanel(int index)
        {
            if (currentPanel == index) return;

            currentPanel = index;
            if (index == 0)
            {
                detail_1_panel.transform.Find("content").GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                detail_1_panel.gameObject.SetActive(true);
                detail_2_panel.gameObject.SetActive(false);
                detail_1.transform.Find("state").GetComponent<Image>().color = new Color32(38, 149, 159, 255);
                detail_1.transform.Find("label").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                detail_2.transform.Find("state").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                detail_2.transform.Find("label").GetComponent<Text>().color = new Color32(50, 50, 50, 255);
            }
            else
            {
                detail_2_panel.transform.Find("content").GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                detail_1_panel.gameObject.SetActive(false);
                detail_2_panel.gameObject.SetActive(true);
                detail_2.transform.Find("state").GetComponent<Image>().color = new Color32(38, 149, 159, 255);
                detail_2.transform.Find("label").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                detail_1.transform.Find("state").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                detail_1.transform.Find("label").GetComponent<Text>().color = new Color32(50, 50, 50, 255);
            }


        }

        void ShowBuHeGeOrder()
        {
            buhegeBtn.gameObject.SetActive(true);
            buhegeLabel.gameObject.SetActive(true);
            //buhegeParent.gameObject.SetActive(true);
        }

        void HideBuHeGeOrder()
        {
            buhegeBtn.gameObject.SetActive(false);
            buhegeLabel.gameObject.SetActive(false);
           // buhegeParent.gameObject.SetActive(false);
        }
    }

}
