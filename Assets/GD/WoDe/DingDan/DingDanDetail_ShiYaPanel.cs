using LitJson;
using LM_Workspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;
using System;
using UnityEngine.EventSystems;
using System.Linq;

namespace GD {

    /// <summary>
    /// 订单细节界面 试压员
    /// </summary>
    public class DingDanDetail_ShiYaPanel : MonoBehaviour {     /// <summary>
                                                                /// 显示的订单信息
                                                                /// </summary>
        protected Order currentShowOrder;


        private UIEventListener back;
        private UIEventListener detail_1;
        private UIEventListener detail_2;
        private GameObject detail_1_panel;
        private GameObject detail_2_panel;

        private GameObject mask;
        private UIEventListener getTime;
        private InputField currentSet;
        private string startTime;
        private string endTime;
        #region 细节图1
        private UIEventListener menpai;
        private UIEventListener state;
        private UIEventListener chufang;
        private UIEventListener weishengjian;
        private UIEventListener keting;
        private UIEventListener guodao;
        private UIEventListener yangtai;
        private UIEventListener other;
        private UIEventListener pingzheng;
        private UIEventListener buhege;
        private InputField buhegeInput;
        private UIEventListener next;
        private UIEventListener callOtherApp;

        #endregion

        #region 细节图2
        private InputField yeZhuName;
        private InputField yeZhuPhone;
        private InputField yeZhuAddress;
        private InputField gouMaidiDian;

        private MyTriggle anZhuangYuYue;
        private InputField anZhuangName;
        private InputField anZhuangPhone;
        private Dropdown fangWuLeiXing;
        private Dropdown chu;
        private Dropdown wei;
        private InputField kaiFaShang;
        private InputField jiaZhuang;
        private InputField nuanTong;
        private InputField kongTiao;
        private Dropdown chanPinType;
        private InputField anZhuangChangDu;

        private Toggle wuDing;
        private Toggle diMian;
        private Toggle qiangMian;

        private Toggle xianChangGanJing;
        private Toggle guanDaoGuDing;
        private Toggle guanDaoWuYaMai;
        private InputField beiZhu;

        private Toggle guiFan1;
        private Toggle guiFan2;
        private Toggle guiFan3;

        private Toggle jieKouShenLou1;
        private Toggle jieKouShenLou2;
        private Toggle jieKouShenLou3;
        private Toggle jieKouShenLou4;
        private Toggle jieKouShenLou5;

        private Toggle hanDianShenLou1;
        private Toggle hanDianShenLou2;
        private Toggle hanDianShenLou3;
        private Toggle hanDianShenLou4;
        private Toggle hanDianShenLou5;

        private Toggle hanDianFanBian1;
        private Toggle hanDianFanBian2;
        private Toggle hanDianFanBian3;
        private Toggle hanDianFanBian4;
        private Toggle hanDianFanBian5;

        private InputField baoYaStartTime;
        private InputField baoYaEndTime;



        private Dropdown yunXingYaLi;
        private Dropdown jianCeYaLi;
        private InputField shiYaName;
        private InputField shiYaPhone;

        private Toggle zhuYi1;
        private Toggle zhuYi2;
        private Toggle zhuYi3;
        private Toggle zhuYi4;


        private GameObject gongjiangCardItem_43;
        private GameObject gongjiangCardItem_44;
        private Toggle gongjiangCard1;
        private Toggle gongjiangCard2;
        private Toggle gongjiangCard3;


        private MyTriggle heGe;
        private MyTriggle buHeGe;

        private UIEventListener enter;

        /// <summary>
        /// 图片数组的prefab
        /// </summary>
        private GameObject imageArrayItem;



        #endregion

        /// <summary>
        /// 图片获取方式选择
        /// </summary>
        private GameObject seleftPanel;

        private UIEventListener cameraBtn;
        private UIEventListener imageBtn;
        private UIEventListener cancelBtn;
        private UIEventListener cancelBGBtn;

        private string currentImageStr;

        RawImage currentImage;
        // 

        private int imageCount;


        void Start() {
#if UNITY_EDITOR
            UtilManager.getInstance.menpaiDic.Add("1", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.stateDic.Add("2", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.chufangDic.Add("3", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.weishengjianDic.Add("4", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.ketingDic.Add("5", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.guodaoDic.Add("6", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.yangtaiDic.Add("7", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.otherDic.Add("8", "http://cjl.milinshiguang.com/178366249.jpg");
            UtilManager.getInstance.pingzhengDic.Add("9", "http://cjl.milinshiguang.com/178366249.jpg");

            UtilManager.getInstance.photoKey.Add("menpai", UtilManager.getInstance.menpaiDic);
            UtilManager.getInstance.photoKey.Add("state", UtilManager.getInstance.stateDic);
            UtilManager.getInstance.photoKey.Add("chufang", UtilManager.getInstance.chufangDic);
            UtilManager.getInstance.photoKey.Add("weishengjian", UtilManager.getInstance.weishengjianDic);
            UtilManager.getInstance.photoKey.Add("keting", UtilManager.getInstance.ketingDic);
            UtilManager.getInstance.photoKey.Add("guodao", UtilManager.getInstance.guodaoDic);
            UtilManager.getInstance.photoKey.Add("yangtai", UtilManager.getInstance.yangtaiDic);
            UtilManager.getInstance.photoKey.Add("other", UtilManager.getInstance.otherDic);
            UtilManager.getInstance.photoKey.Add("pingzheng", UtilManager.getInstance.pingzhengDic);
#endif
            if (gongjiangCard1 != null)
                gongjiangCard1.onValueChanged.AddListener((arg0) => {
                    if (gongjiangCard1.isOn) {
                        gongjiangCard2.isOn = false;
                        gongjiangCard3.isOn = false;
                    }
                });
            if (gongjiangCard2 != null)
                gongjiangCard2.onValueChanged.AddListener((arg0) => {
                    if (gongjiangCard2.isOn) {
                        gongjiangCard1.isOn = false;
                        gongjiangCard3.isOn = false;
                    }
                });
            if (gongjiangCard3 != null)
                gongjiangCard3.onValueChanged.AddListener((arg0) => {
                    if (gongjiangCard3.isOn) {
                        gongjiangCard1.isOn = false;
                        gongjiangCard2.isOn = false;
                    }
                });
            imageCount = 9;

            EventManager.instance.RegisterEvent(XMWorkspace.Event.CheckBuilder, OnCheckBuilder);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.TakeTest, OnTakeTestPressure);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.PostImagePlugin, OnLocalPhoto);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.CallAndroidTimeGD, RegisterAndroidTime);

            imageArrayItem = Resources.Load("imageArrayItem") as GameObject;

            back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = delegate { Hide(); };

            detail_1 = transform.Find("menu_bar").Find("layout").Find("btn_1").GetComponent<UIEventListener>();
            detail_2 = transform.Find("menu_bar").Find("layout").Find("btn_2").GetComponent<UIEventListener>();


            detail_1_panel = transform.Find("menu_1_detail").gameObject;
            detail_2_panel = transform.Find("menu_2_detail").gameObject;
            detail_1.onClick = delegate { ChangeDetailPanel(0); };
            detail_2.onClick = delegate { ChangeDetailPanel(1); };



            seleftPanel = Util.FindChildByName(gameObject.transform.parent.gameObject, "SelectPanel");
            cameraBtn = Util.FindChildByName(seleftPanel, "CameraBtn").GetComponent<UIEventListener>();
            imageBtn = Util.FindChildByName(seleftPanel, "ImageBtn").GetComponent<UIEventListener>();
            cancelBtn = Util.FindChildByName(seleftPanel, "CancelBtn").GetComponent<UIEventListener>();
            cancelBGBtn = Util.FindChildByName(seleftPanel, "BG").GetComponent<UIEventListener>();

            #region 界面1
            next = detail_1_panel.transform.Find("content").Find("button").Find("buttonPrefab").GetComponent<UIEventListener>();
            next.onClick = delegate { ChangeDetailPanel(1); };

            callOtherApp = detail_1_panel.transform.Find("content").Find("callOtherAppBtn").Find("buttonPrefab").GetComponent<UIEventListener>();
            callOtherApp.onClick = delegate { CallOtherApp(); };

            menpai = detail_1_panel.transform.Find("content").Find("item_1").Find("btn_add").GetComponent<UIEventListener>();
            menpai.onClick = delegate {
                if (UtilManager.getInstance.menpaiParent.childCount >= 1) {
                    UIManager.instance.ShowLoggerMsg("门牌照片只允许添加1张!");

                    return;
                }
                currentImageStr = "menpai";
                seleftPanel.SetActive(true);
                // // currentImage = jinChang.transform.GetComponentInChildren<RawImage>();
                // // currentImage.color = Color.white;
                // #if UNITY_EDITOR
                // // PosImage("jinchang", "头像.png");
                // // PostImageManager.getInstance.PostImage(2, "menpai");
                //#else
                //// PostImageManager.getInstance.PostImage (2, "menpai");

                //#endif

                // PosImage("jinchang");
            };
            state = detail_1_panel.transform.Find("content").Find("item_2").Find("btn_add").GetComponent<UIEventListener>();

            state.onClick = delegate {
                if (UtilManager.getInstance.stateParent.childCount >= 1) {
                    UIManager.instance.ShowLoggerMsg("智能试压仪状态图只允许添加1张!");

                    return;
                }
                currentImageStr = "state";
                seleftPanel.SetActive(true);
                //               // currentImage = shiYa.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;
                //#if UNITY_EDITOR
                //                PosImage("shiya", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage (1, "shiya");
                //#endif
                //// PosImage("shiya");
            };

            chufang = detail_1_panel.transform.Find("content").Find("item_3").Find("btn_add").GetComponent<UIEventListener>();
            chufang.onClick = delegate {
                if (UtilManager.getInstance.chufangParent.childCount >= imageCount) {
                    UIManager.instance.ShowLoggerMsg("厨房照片最多允许添加" + imageCount.ToString() + "张!");

                    return;
                }
                currentImageStr = "chufang";
                seleftPanel.SetActive(true);
                //               // currentImage = keTing.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;
                //#if UNITY_EDITOR
                //                PosImage("keting", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage (1, "keting");
                //#endif
                ////  PosImage("keting");
            };
            weishengjian = detail_1_panel.transform.Find("content").Find("item_4").Find("btn_add").GetComponent<UIEventListener>();
            weishengjian.onClick = delegate {
                if (UtilManager.getInstance.weishengjianParent.childCount >= imageCount) {
                    UIManager.instance.ShowLoggerMsg("卫生间照片最多允许添加" + imageCount.ToString() + "张!");

                    return;
                }
                currentImageStr = "weishengjian";
                seleftPanel.SetActive(true);
                //               // currentImage = woShi.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;
                //#if UNITY_EDITOR
                //                PosImage("woshi", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage (1, "woshi");
                //#endif

                //// PosImage("woshi");
            };
            keting = detail_1_panel.transform.Find("content").Find("item_5").Find("btn_add").GetComponent<UIEventListener>();
            keting.onClick = delegate {
                if (UtilManager.getInstance.ketingParent.childCount >= imageCount) {
                    UIManager.instance.ShowLoggerMsg("客厅照片最多允许添加" + imageCount.ToString() + "张!");

                    return;
                }
                currentImageStr = "keting";
                seleftPanel.SetActive(true);
                //               // currentImage = weiShengJian.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;

                //#if UNITY_EDITOR
                //                PosImage("weishengjian", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage (1, "weishengjian");
                //#endif
                //// PosImage("weishengjian");
            };
            guodao = detail_1_panel.transform.Find("content").Find("item_6").Find("btn_add").GetComponent<UIEventListener>();
            guodao.onClick = delegate {
                if (UtilManager.getInstance.guodaoParent.childCount >= imageCount) {
                    UIManager.instance.ShowLoggerMsg("过道照片最多允许添加" + imageCount.ToString() + "张!");

                    return;
                }
                currentImageStr = "guodao";
                seleftPanel.SetActive(true);
                //               // currentImage = chuFang.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;

                //#if UNITY_EDITOR
                //                PosImage("chufang", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage (1, "chufang");
                //#endif
                ////  PosImage("chufang");
            };
            yangtai = detail_1_panel.transform.Find("content").Find("item_7").Find("btn_add").GetComponent<UIEventListener>();
            yangtai.onClick = delegate {
                if (UtilManager.getInstance.yangtaiParent.childCount >= imageCount) {
                    UIManager.instance.ShowLoggerMsg("阳台照片最多允许添加" + imageCount.ToString() + "张!");

                    return;
                }
                currentImageStr = "yangtai";
                seleftPanel.SetActive(true);
                //                currentImage = yangTai.transform.GetComponentInChildren<RawImage>();
                //                currentImage.color = Color.white;
                //#if UNITY_EDITOR
                //                PosImage("yangtai", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage (1, "yangtai");
                //#endif
                ////PosImage("yangtai");
            };
            other = detail_1_panel.transform.Find("content").Find("item_8").Find("btn_add").GetComponent<UIEventListener>();
            other.onClick = delegate {
                if (UtilManager.getInstance.otherParent.childCount >= imageCount) {
                    UIManager.instance.ShowLoggerMsg("其他照片最多允许添加" + imageCount.ToString() + "张!");

                    return;
                }
                currentImageStr = "other";
                seleftPanel.SetActive(true);
                //               // currentImage = zouLang.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;

                //#if UNITY_EDITOR
                //                PosImage("zoulang", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage(1, "zoulang");
                //#endif
                //// PosImage("zoulang");
            };
            pingzheng = detail_1_panel.transform.Find("content").Find("item_9").Find("btn_add").GetComponent<UIEventListener>();
            pingzheng.onClick = delegate {
                if (UtilManager.getInstance.pingzhengParent.childCount >= 1) {
                    UIManager.instance.ShowLoggerMsg("凭证照片只允许添加1张!");

                    return;
                }

                currentImageStr = "pingzheng";
                seleftPanel.SetActive(true);
                //               // currentImage = zouLang.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;

                //#if UNITY_EDITOR
                //                PosImage("zoulang", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage(1, "zoulang");
                //#endif
                //// PosImage("zoulang");
            };
            buhegeInput = detail_1_panel.transform.Find("content").Find("item_10_InPut").GetComponent<InputField>();
            buhege = detail_1_panel.transform.Find("content").Find("item_10").Find("btn_add").GetComponent<UIEventListener>();

            buhege.onClick = delegate {
                if (UtilManager.getInstance.buhegeParent.childCount >= 3) {
                    UIManager.instance.ShowLoggerMsg("不合格照片最多允许添加3张!");

                    return;
                }

                currentImageStr = "buhege";
                seleftPanel.SetActive(true);
                //               // currentImage = zouLang.transform.GetComponentInChildren<RawImage>();
                //               // currentImage.color = Color.white;

                //#if UNITY_EDITOR
                //                PosImage("zoulang", "头像.png");

                //#else
                //             PostImageManager.getInstance.PostImage(1, "zoulang");
                //#endif
                //// PosImage("zoulang");
            };
            #endregion

            cameraBtn.onClick = delegate {

                PostImageManager.getInstance.PostImage(2, currentImageStr);

            };
            imageBtn.onClick = delegate {
                PostImageManager.getInstance.PostImage(1, currentImageStr);

            };
            cancelBtn.onClick = delegate {

                seleftPanel.SetActive(false);
            };
            cancelBGBtn.onClick = delegate {

                seleftPanel.SetActive(false);
            };

            #region 界面2
            enter = transform.Find("menu_2_detail").Find("content").Find("button").Find("buttonPrefab").GetComponent<UIEventListener>();

            enter.onClick = PosOrderInfo;

            #endregion





            //UserData.instance.token = "49n2v5rEtEdgxbe8";


        }

        //private void CheckImageNum(int _type,string _imageStr)
        //{
        //    if(UtilManager.getInstance.menpaiParent.childCount >= 1)
        //    {
        //        UIManager.instance.ShowLoggerMsg("门牌照片只允许添加1张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.stateParent.childCount >= 1)
        //    {
        //        UIManager.instance.ShowLoggerMsg("智能试压仪状态图只允许添加1张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.chufangParent.childCount >= imageCount)
        //    {
        //        UIManager.instance.ShowLoggerMsg("厨房照片最多允许添加" + imageCount.ToString() +"张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.weishengjianParent.childCount >= imageCount)
        //    {
        //        UIManager.instance.ShowLoggerMsg("卫生间照片最多允许添加" + imageCount.ToString() + "张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.ketingParent.childCount >= imageCount)
        //    {
        //        UIManager.instance.ShowLoggerMsg("客厅照片最多允许添加" + imageCount.ToString() + "张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.guodaoParent.childCount >= imageCount)
        //    {
        //        UIManager.instance.ShowLoggerMsg("过道照片最多允许添加" + imageCount.ToString() + "张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.yangtaiParent.childCount >= imageCount)
        //    {
        //        UIManager.instance.ShowLoggerMsg("阳台照片最多允许添加" + imageCount.ToString() + "张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.otherParent.childCount >= imageCount)
        //    {
        //        UIManager.instance.ShowLoggerMsg("其他照片最多允许添加" + imageCount.ToString() + "张!");

        //        return;
        //    }
        //    if (UtilManager.getInstance.pingzhengParent.childCount >= 1)
        //    {
        //        UIManager.instance.ShowLoggerMsg("凭证照片只允许添加1张!");

        //        return;
        //    }


        //}

        private void RegisterAndroidTime(object[] param) {
            if (!gameObject.activeInHierarchy)
                return;
            string dataTime = UtilManager.getInstance.timeStr;
            string year = dataTime.Split(' ')[0].Split('-')[0];
            string month = dataTime.Split(' ')[0].Split('-')[1];
            string day = dataTime.Split(' ')[0].Split('-')[2];
            string hour = dataTime.Split(' ')[1].Split('-')[0];
            string min = dataTime.Split(' ')[1].Split('-')[1];
            string sec = dataTime.Split(' ')[1].Split('-')[2];
            currentSet.text = year + "/" + month + "/" + day + " " + hour + ":" + min;
        }

        private void DestroyChild(Transform _parent) {
            foreach (RectTransform trans in _parent) {
                Destroy(trans.gameObject);
            }
        }

        private void CallOtherApp() {
            if (Application.platform == RuntimePlatform.Android)
                UIManager.instance.ShowCommitMsg("是否确认连接智能试压仪？", "提示", delegate { AndroidMethodManager.CallOtherApp(); });
            else {
#if UNITY_IOS
                UIManager.instance.ShowCommitMsg("是否确认连接智能试压仪？", "提示", delegate { IosMethodManager.CallOtherApp(); });
#endif
            }
        }

        /// <summary>
        /// 退出详情界面
        /// </summary>
        void Hide() {
            gameObject.SetActive(false);
            ChangeDetailPanel(0);
            DestroyChild(UtilManager.getInstance.menpaiParent);
            DestroyChild(UtilManager.getInstance.stateParent);
            DestroyChild(UtilManager.getInstance.chufangParent);
            DestroyChild(UtilManager.getInstance.weishengjianParent);
            DestroyChild(UtilManager.getInstance.ketingParent);
            DestroyChild(UtilManager.getInstance.guodaoParent);
            DestroyChild(UtilManager.getInstance.yangtaiParent);
            DestroyChild(UtilManager.getInstance.otherParent);
            DestroyChild(UtilManager.getInstance.pingzhengParent);
            DestroyChild(UtilManager.getInstance.buhegeParent);

        }
        private void OnDisable() {
            DestroyChild(UtilManager.getInstance.menpaiParent);
            DestroyChild(UtilManager.getInstance.stateParent);
            DestroyChild(UtilManager.getInstance.chufangParent);
            DestroyChild(UtilManager.getInstance.weishengjianParent);
            DestroyChild(UtilManager.getInstance.ketingParent);
            DestroyChild(UtilManager.getInstance.guodaoParent);
            DestroyChild(UtilManager.getInstance.yangtaiParent);
            DestroyChild(UtilManager.getInstance.otherParent);
            DestroyChild(UtilManager.getInstance.pingzhengParent);
            DestroyChild(UtilManager.getInstance.buhegeParent);

        }
        public void Show(Order data) {
            gameObject.SetActive(true);
            UtilManager.getInstance.isBigImageShowDestroyBtn = true;
            UtilManager.getInstance.photoKey.Clear();
            UtilManager.getInstance.menpaiDic.Clear();
            UtilManager.getInstance.stateDic.Clear();
            UtilManager.getInstance.chufangDic.Clear();
            UtilManager.getInstance.weishengjianDic.Clear();
            UtilManager.getInstance.ketingDic.Clear();
            UtilManager.getInstance.guodaoDic.Clear();
            UtilManager.getInstance.yangtaiDic.Clear();
            UtilManager.getInstance.otherDic.Clear();
            UtilManager.getInstance.pingzhengDic.Clear();
            UtilManager.getInstance.buhegeDic.Clear();
            ChangeDetailPanel(0);
            currentShowOrder = data;
            DestroyChild(UtilManager.getInstance.menpaiParent);
            DestroyChild(UtilManager.getInstance.stateParent);
            DestroyChild(UtilManager.getInstance.chufangParent);
            DestroyChild(UtilManager.getInstance.weishengjianParent);
            DestroyChild(UtilManager.getInstance.ketingParent);
            DestroyChild(UtilManager.getInstance.guodaoParent);
            DestroyChild(UtilManager.getInstance.yangtaiParent);
            DestroyChild(UtilManager.getInstance.otherParent);
            DestroyChild(UtilManager.getInstance.pingzhengParent);
            DestroyChild(UtilManager.getInstance.buhegeParent);

            //业主姓名
            if (yeZhuName == null) {
                yeZhuName = transform.Find("menu_2_detail").Find("content").Find("item_1").Find("InputField").GetComponent<InputField>();
            }
            yeZhuName.interactable = false;
            yeZhuName.text = currentShowOrder.name;
            //业主电话
            if (yeZhuPhone == null) {
                yeZhuPhone = transform.Find("menu_2_detail").Find("content").Find("item_2").Find("InputField").GetComponent<InputField>();
            }
            yeZhuPhone.interactable = false;
            yeZhuPhone.text = currentShowOrder.phone;
            //业主地址
            if (yeZhuAddress == null) {
                yeZhuAddress = transform.Find("menu_2_detail").Find("content").Find("item_3").Find("InputField").GetComponent<InputField>();
            }
            yeZhuAddress.interactable = false;
            yeZhuAddress.text = currentShowOrder.address;

            //购买地点
            if (gouMaidiDian == null) {
                gouMaidiDian = transform.Find("menu_2_detail").Find("content").Find("item_4").Find("InputField").GetComponent<InputField>();
            }
            //gouMaidiDian.interactable = false;
            gouMaidiDian.text = currentShowOrder.testBuyPlace;

            if (anZhuangYuYue == null) {
                anZhuangYuYue = transform.Find("menu_2_detail").Find("content").Find("item_41").GetComponent<MyTriggle>();
            }
            anZhuangYuYue.IsOn = false;
            if (anZhuangName == null)
                anZhuangName = transform.Find("menu_2_detail").Find("content").Find("item_5").Find("InputField").GetComponent<InputField>();
            anZhuangName.text = "";
            anZhuangName.interactable = true;
            if (anZhuangPhone == null) {
                anZhuangPhone = transform.Find("menu_2_detail").Find("content").Find("item_6").Find("InputField").GetComponent<InputField>();

                // 
            }
            anZhuangPhone.onEndEdit.AddListener(CheckPhoneNum);
            anZhuangPhone.text = "";
            anZhuangPhone.interactable = true;

            anZhuangYuYue.onValueChange = OnAnZhuangYuYueChange;


            if (fangWuLeiXing == null)
                fangWuLeiXing = transform.Find("menu_2_detail").Find("content").Find("item_7").Find("dropdown").GetComponent<Dropdown>();
            fangWuLeiXing.value = 0;
            if (chu == null)
                chu = transform.Find("menu_2_detail").Find("content").Find("item_8").Find("dropdown_chu").GetComponent<Dropdown>();
            chu.value = 0;
            if (wei == null)
                wei = transform.Find("menu_2_detail").Find("content").Find("item_8").Find("dropdown_wei").GetComponent<Dropdown>();
            wei.value = 0;
            if (kaiFaShang == null)
                kaiFaShang = transform.Find("menu_2_detail").Find("content").Find("item_9").Find("InputField").GetComponent<InputField>();
            kaiFaShang.text = "";
            if (jiaZhuang == null)
                jiaZhuang = transform.Find("menu_2_detail").Find("content").Find("item_10").Find("InputField").GetComponent<InputField>();
            jiaZhuang.text = "";
            if (nuanTong == null)
                nuanTong = transform.Find("menu_2_detail").Find("content").Find("item_11").Find("InputField").GetComponent<InputField>();
            nuanTong.text = "";
            if (kongTiao == null)
                kongTiao = transform.Find("menu_2_detail").Find("content").Find("item_12").Find("InputField").GetComponent<InputField>();
            kongTiao.text = "";

            if (chanPinType == null)
                chanPinType = transform.Find("menu_2_detail").Find("content").Find("item_13").Find("dropdown").GetComponent<Dropdown>();
            chanPinType.value = 0;
            if (anZhuangChangDu == null)
                anZhuangChangDu = transform.Find("menu_2_detail").Find("content").Find("item_14").Find("InputField").GetComponent<InputField>();
            anZhuangChangDu.text = "";

            if (wuDing == null) {
                wuDing = transform.Find("menu_2_detail").Find("content").Find("item_15").Find("toggle_1").GetComponent<Toggle>();

                wuDing.onValueChanged.AddListener(OnChooseWuDing);

            }
            wuDing.isOn = false;
            if (diMian == null) {
                diMian = transform.Find("menu_2_detail").Find("content").Find("item_15").Find("toggle_2").GetComponent<Toggle>();

                diMian.onValueChanged.AddListener(OnChooseDiMian);
            }
            diMian.isOn = false;
            if (qiangMian == null) {

                qiangMian = transform.Find("menu_2_detail").Find("content").Find("item_15").Find("toggle_3").GetComponent<Toggle>();
                qiangMian.onValueChanged.AddListener(OnChooseQiangMian);
            }
            qiangMian.isOn = false;

            if (xianChangGanJing == null)
                xianChangGanJing = transform.Find("menu_2_detail").Find("content").Find("item_17").Find("layout").Find("toggle_1").GetComponent<Toggle>();
            xianChangGanJing.isOn = false;
            if (guanDaoGuDing == null)
                guanDaoGuDing = transform.Find("menu_2_detail").Find("content").Find("item_17").Find("layout").Find("toggle_2").GetComponent<Toggle>();
            guanDaoGuDing.isOn = false;
            if (guanDaoWuYaMai == null)
                guanDaoWuYaMai = transform.Find("menu_2_detail").Find("content").Find("item_17").Find("layout").Find("toggle_3").GetComponent<Toggle>();
            guanDaoWuYaMai.isOn = false;

            if (beiZhu == null)
                beiZhu = transform.Find("menu_2_detail").Find("content").Find("item_18").Find("InputField").GetComponent<InputField>();
            beiZhu.text = "";

            if (guiFan1 == null)
                guiFan1 = transform.Find("menu_2_detail").Find("content").Find("item_22").GetComponent<Toggle>();
            guiFan1.isOn = false;
            if (guiFan2 == null)
                guiFan2 = transform.Find("menu_2_detail").Find("content").Find("item_23").GetComponent<Toggle>();
            guiFan2.isOn = false;
            if (guiFan3 == null)
                guiFan3 = transform.Find("menu_2_detail").Find("content").Find("item_24").GetComponent<Toggle>();
            guiFan3.isOn = false;

            if (jieKouShenLou1 == null)
                jieKouShenLou1 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_1").GetComponent<Toggle>();
            jieKouShenLou1.isOn = false;
            if (jieKouShenLou2 == null)
                jieKouShenLou2 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_2").GetComponent<Toggle>();
            jieKouShenLou2.isOn = false;
            if (jieKouShenLou3 == null)
                jieKouShenLou3 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_3").GetComponent<Toggle>();
            jieKouShenLou3.isOn = false;
            if (jieKouShenLou4 == null)
                jieKouShenLou4 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_4").GetComponent<Toggle>();
            jieKouShenLou4.isOn = false;
            if (jieKouShenLou5 == null)
                jieKouShenLou5 = transform.Find("menu_2_detail").Find("content").Find("item_26").Find("layout").Find("item_5").GetComponent<Toggle>();
            jieKouShenLou5.isOn = false;


            if (hanDianShenLou1 == null)
                hanDianShenLou1 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_1").GetComponent<Toggle>();
            hanDianShenLou1.isOn = false;
            if (hanDianShenLou2 == null)
                hanDianShenLou2 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_2").GetComponent<Toggle>();
            hanDianShenLou2.isOn = false;
            if (hanDianShenLou3 == null)
                hanDianShenLou3 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_3").GetComponent<Toggle>();
            hanDianShenLou3.isOn = false;
            if (hanDianShenLou4 == null)
                hanDianShenLou4 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_4").GetComponent<Toggle>();
            hanDianShenLou4.isOn = false;
            if (hanDianShenLou5 == null)
                hanDianShenLou5 = transform.Find("menu_2_detail").Find("content").Find("item_28").Find("layout").Find("item_5").GetComponent<Toggle>();
            hanDianShenLou5.isOn = false;


            if (hanDianFanBian1 == null)
                hanDianFanBian1 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_1").GetComponent<Toggle>();
            hanDianFanBian1.isOn = false;
            if (hanDianFanBian2 == null)
                hanDianFanBian2 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_2").GetComponent<Toggle>();
            hanDianFanBian2.isOn = false;
            if (hanDianFanBian3 == null)
                hanDianFanBian3 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_3").GetComponent<Toggle>();
            hanDianFanBian3.isOn = false;
            if (hanDianFanBian4 == null)
                hanDianFanBian4 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_4").GetComponent<Toggle>();
            hanDianFanBian4.isOn = false;
            if (hanDianFanBian5 == null)
                hanDianFanBian5 = transform.Find("menu_2_detail").Find("content").Find("item_30").Find("layout").Find("item_5").GetComponent<Toggle>();
            hanDianFanBian5.isOn = false;

            if (Application.platform == RuntimePlatform.IPhonePlayer) {
                #if UNITY_IOS
                if (mask == null) {
                    mask = transform.Find("MaskWhite_yuyue").gameObject;
                    getTime = mask.transform.Find("closeIosTimeBtn_yuyue").GetComponent<UIEventListener>();
                    getTime.onClick = delegate {
                        mask.SetActive(false);

                        IosMethodManager.HideIosDataTime();
                        // startTime = UtilManager.getInstance.timeStr;
                        // string[] strs = startTime.Split('-');
                        string dataTime = UtilManager.getInstance.timeStr;
                        string year = dataTime.Split(' ')[0].Split('-')[0];
                        string month = dataTime.Split(' ')[0].Split('-')[1];
                        string day = dataTime.Split(' ')[0].Split('-')[2];
                        string hour = dataTime.Split(' ')[1].Split('-')[0];
                        string min = dataTime.Split(' ')[1].Split('-')[1];
                        string sec = dataTime.Split(' ')[1].Split('-')[2];
                        currentSet.text = year + "/" + month + "/" + day + " " + hour + ":" + min;

                    };

                }
#endif

                mask.SetActive(false);
            }
            if (baoYaStartTime == null)
                baoYaStartTime = transform.Find("menu_2_detail").Find("content").Find("item_32").Find("layout").Find("left").Find("InputField").GetComponent<InputField>();
            baoYaStartTime.interactable = false;
            baoYaStartTime.text = "";

            baoYaStartTime.GetComponent<UIEventListener>().onClick = delegate {


                currentSet = baoYaStartTime;

#if UNITY_EDITOR
                baoYaStartTime.text = "2020/10/1 11:30";
#endif
                if (Application.platform == RuntimePlatform.IPhonePlayer) {
#if UNITY_IOS

                    mask.SetActive(true);
                    if (UtilManager.getInstance.isCalledDataTime) {
                        IosMethodManager.ShowIosDataTime();
                    }
                    else {
                            IosMethodManager.CallIosDataTime();

                        }
#endif

                }
                else
                    AndroidMethodManager.CallAndroidDataTime();

            };


            if (baoYaEndTime == null)
                baoYaEndTime = transform.Find("menu_2_detail").Find("content").Find("item_32").Find("layout").Find("right").Find("InputField").GetComponent<InputField>();
            baoYaEndTime.interactable = false;
            baoYaEndTime.text = "";


            baoYaEndTime.GetComponent<UIEventListener>().onClick = delegate {

                currentSet = baoYaEndTime;

#if UNITY_EDITOR
                baoYaEndTime.text = "2020/10/2 12:30";
#endif
                if (Application.platform == RuntimePlatform.IPhonePlayer) {
                    #if UNITY_IOS
                    mask.SetActive(true);
                    if (UtilManager.getInstance.isCalledDataTime) {
                        IosMethodManager.ShowIosDataTime();
                    }
                    else {
                        IosMethodManager.CallIosDataTime();

                    }
                    #endif
                }
                else
                    AndroidMethodManager.CallAndroidDataTime();
            };



            if (yunXingYaLi == null)
                yunXingYaLi = transform.Find("menu_2_detail").Find("content").Find("item_42").Find("dropdown").GetComponent<Dropdown>();
            yunXingYaLi.value = 0;

            if (jianCeYaLi == null)
                jianCeYaLi = transform.Find("menu_2_detail").Find("content").Find("item_33").Find("dropdown").GetComponent<Dropdown>();
            jianCeYaLi.value = 0;


            if (shiYaName == null)
                shiYaName = transform.Find("menu_2_detail").Find("content").Find("item_34").Find("InputField").GetComponent<InputField>();
            shiYaName.text = currentShowOrder.testUserName;
            shiYaName.interactable = false;

            if (shiYaPhone == null)
                shiYaPhone = transform.Find("menu_2_detail").Find("content").Find("item_35").Find("InputField").GetComponent<InputField>();
            shiYaPhone.text = currentShowOrder.testUserPhone;
            shiYaPhone.interactable = false;

            if (zhuYi1 == null)
                zhuYi1 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_1").GetComponent<Toggle>();
            zhuYi1.isOn = false;
            if (zhuYi2 == null)
                zhuYi2 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_2").GetComponent<Toggle>();
            zhuYi2.isOn = false;
            if (zhuYi3 == null)
                zhuYi3 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_3").GetComponent<Toggle>();
            zhuYi3.isOn = false;
            if (zhuYi4 == null)
                zhuYi4 = transform.Find("menu_2_detail").Find("content").Find("item_37").Find("layout").Find("item_4").GetComponent<Toggle>();
            zhuYi4.isOn = false;

#region 新增工匠卡
            if (gongjiangCardItem_43 == null)
                gongjiangCardItem_43 = transform.Find("menu_2_detail").Find("content").Find("item_43").gameObject;
            if (gongjiangCardItem_44 == null)
                gongjiangCardItem_44 = transform.Find("menu_2_detail").Find("content").Find("item_44").gameObject;
            if (data.cardSwitch == "1") {
                gongjiangCardItem_43.SetActive(true);
                gongjiangCardItem_44.SetActive(true);
                if (gongjiangCard1 == null)
                    gongjiangCard1 = gongjiangCardItem_44.transform.Find("layout").Find("item_1").GetComponent<Toggle>();
                gongjiangCard1.isOn = false;
                if (gongjiangCard2 == null)
                    gongjiangCard2 = gongjiangCardItem_44.transform.Find("layout").Find("item_2").GetComponent<Toggle>();
                gongjiangCard2.isOn = false;
                if (gongjiangCard3 == null)
                    gongjiangCard3 = gongjiangCardItem_44.transform.Find("layout").Find("item_3").GetComponent<Toggle>();
                gongjiangCard3.isOn = false;
            }
            else {
                gongjiangCardItem_43.SetActive(false);
                gongjiangCardItem_44.SetActive(false);
            }

#endregion

            if (heGe == null) {
                heGe = transform.Find("menu_2_detail").Find("content").Find("item_39").GetComponent<MyTriggle>();
                heGe.onValueChange = OnChooseHeGe;

            }
            heGe.IsOn = true;
            if (buHeGe == null) {
                buHeGe = transform.Find("menu_2_detail").Find("content").Find("item_40").GetComponent<MyTriggle>();
                buHeGe.onValueChange = OnChooseBuHeGe;

            }
            buHeGe.IsOn = false;

            HideBuHeGeArray();
        }

        void OnLocalPhoto(object[] data) {
            if (gameObject.activeSelf) {
                string typeName = data[0].ToString();
                string path = data[1].ToString();
                //UserData.instance.token = "mwpVJ5K0tjojPkaL";
                //StartCoroutine(GameObject.Find ("Camera").GetComponent<InterfaceManager> ().PostImage (path, "测试图片",typeName, "image/jpg", OnLocalPhotoJson));
                PosImage(typeName, path);
            }

        }
        /// <summary>
        /// 提交试压信息回调
        /// </summary>
        /// <param name="obj"></param>
        private void OnTakeTestPressure(object[] obj) {
            // Debug.Log("二次");
            //if ((bool)obj[0])
            //{

            //    UIManager.instance.GoToShiYaJieGuo(heGe.IsOn);

            //}
            //else
            //{
            //    ///提示提交失败
            //   // UIManager.instance.ShowLoggerMsg("提交试压结果失败");

            //}

            if (heGe.IsOn) {
                //  Debug.Log("三次");
                UIManager.instance.GoToShiYaJieGuo(true);
            }
            else {
                //  Debug.Log("四次");
                UIManager.instance.GoToShiYaJieGuo(false);
            }
        }

        /// <summary>
        /// 提交试压信息
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void PosOrderInfo(GameObject go, PointerEventData data) {
            Debug.Log(buildId + " bulidID");

            PostTest param = new PostTest();
            param.appointment_id = currentShowOrder.id;
            param.cardType = "0";
            Dictionary<string, Dictionary<string, string>> photoKey = UtilManager.getInstance.photoKey;
            // Debug.Log(param.testHouseType);

            if (photoKey.ContainsKey("menpai")) {
                foreach (var item in photoKey["menpai"]) {
                    //Debug.Log(item.Value + "0000000000");
                    param.menpaiList.Add(item.Value);
                }

            }
            else {
                UIManager.instance.ShowLoggerMsg("未添加门牌照片!");

                return;
            }
            if (photoKey.ContainsKey("state")) {
                foreach (var item in photoKey["state"]) {
                    param.stateList.Add(item.Value);
                }
            }
            else {
                UIManager.instance.ShowLoggerMsg("未添加智能试压仪状态图!");

                return;

            }
            if (photoKey.ContainsKey("chufang")) {
                foreach (var item in photoKey["chufang"]) {
                    param.chufangList.Add(item.Value);
                }
            }
            else {

                param.chufangList.Add("");

            }
            if (photoKey.ContainsKey("weishengjian")) {
                foreach (var item in photoKey["weishengjian"]) {
                    param.weishengjianList.Add(item.Value);
                }
            }
            else {
                param.weishengjianList.Add("");

            }
            if (photoKey.ContainsKey("keting")) {
                foreach (var item in photoKey["keting"]) {
                    param.ketingList.Add(item.Value);
                }
            }
            //else
            //{
            //    param.ketingList.Add("");

            //}
            if (photoKey.ContainsKey("guodao")) {
                foreach (var item in photoKey["guodao"]) {
                    param.guodaoList.Add(item.Value);
                }
            }
            //else
            //{
            //    param.guodaoList.Add("");

            //}

            if (photoKey.ContainsKey("yangtai")) {
                foreach (var item in photoKey["yangtai"]) {
                    param.yangtaiList.Add(item.Value);
                }
            }
            //else
            //{
            //    param.yangtaiList.Add("");

            //}
            if (photoKey.ContainsKey("other")) {
                foreach (var item in photoKey["other"]) {
                    param.otherList.Add(item.Value);
                }
            }
            //else
            //{
            //    param.otherList.Add("");

            //}
            if (photoKey.ContainsKey("pingzheng")) {
                foreach (var item in photoKey["pingzheng"]) {
                    param.pingzhengList.Add(item.Value);
                }
            }
            else {
                UIManager.instance.ShowLoggerMsg("未添加凭证照片!");

                return;

            }
            if (buHeGe.IsOn) {
                if (photoKey.ContainsKey("buhege")) {
                    foreach (var item in photoKey["buhege"]) {
                        param.buhegeList.Add(item.Value);
                    }
                }

                if (buhegeInput.text == "") {
                    UIManager.instance.ShowLoggerMsg("不合格备注不能为空!");

                    return;
                }

                param.buhegeStr = buhegeInput.text;
            }

            param.buildId = buildId.ToString();
#if UNITY_EDITOR
            Debug.Log("buildId:" + buildId);
#endif
            if (anZhuangName.text == "") {
                UIManager.instance.ShowLoggerMsg("安装人姓名不能为空!");

                return;
            }
            param.buildName = anZhuangName.text;
            if (anZhuangPhone.text == "") {
                UIManager.instance.ShowLoggerMsg("安装人手机不能为空!");

                return;
            }
            param.buildPhone = anZhuangPhone.text;

            if (gouMaidiDian.text == "") {
                gouMaidiDian.text = " ";
            }
            param.testBuyPlace = gouMaidiDian.text;

            param.testHouseType = (fangWuLeiXing.value + 1).ToString();

            param.testKitchenType = chu.value.ToString();
            param.testToiletType = wei.value.ToString();

            if (kaiFaShang.text == "") {
                kaiFaShang.text = " ";
            }
            param.testDeveloper = kaiFaShang.text;
            if (jiaZhuang.text == "") {
                jiaZhuang.text = " ";
            }
            param.testDecoration = jiaZhuang.text;
            if (nuanTong.text == "") {
                nuanTong.text = " ";
            }
            param.testHvac = nuanTong.text;
            if (kongTiao.text == "") {
                kongTiao.text = " ";
            }
            param.testAir = kongTiao.text;

            int value = chanPinType.value;
            int[] m_type = new int[] { 1, 2, 3, 4, 5, 6, 8, 9,10, 7 };
            param.testProducyType = m_type[value].ToString();


            Debug.Log("aaaa " + param.testProducyType);

            if (anZhuangChangDu.text == "") {
                anZhuangChangDu.text = " ";
            }
            param.testLength = anZhuangChangDu.text;


            if (wuDing.isOn)
                param.testLayingType = "1";
            else if (diMian.isOn)
                param.testLayingType = "2";
            else if (qiangMian.isOn)
                param.testLayingType = "3";
            else {

                UIManager.instance.ShowLoggerMsg("请选择管道铺设方案!");

                return;
            }

            param.testPipeline = "";

            //if(!xianChangGanJing.isOn && !guanDaoGuDing.isOn && !guanDaoWuYaMai.isOn)
            //{
            //    UIManager.instance.ShowLoggerMsg("请选择现场管道保护");
            //}

            if (xianChangGanJing.isOn) {
                param.testPipeline += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("现场管道保护未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testPipeline += 0;
            }

            if (guanDaoGuDing.isOn) {
                param.testPipeline += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("现场管道保护未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testPipeline += 0;
            }
            if (guanDaoWuYaMai.isOn) {
                param.testPipeline += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("现场管道保护未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testPipeline += 0;
            }

            if (beiZhu.text == "") {
                beiZhu.text = " ";
            }
            param.testRemark = beiZhu.text;

            param.testAssess = "";

            if (guiFan1.isOn) {
                param.testAssess += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("安装规范评定未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testAssess += 0;
            }

            if (guiFan2.isOn) {
                param.testAssess += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("安装规范评定未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testAssess += 0;
            }
            if (guiFan3.isOn) {
                param.testAssess += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("安装规范评定未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testAssess += 0;
            }

            param.testCompress = "";

            if (jieKouShenLou1.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("加压1.0兆帕接口有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testCompress += 1;
            }
            else {
                param.testCompress += 0;
            }

            if (jieKouShenLou2.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("加压1.0兆帕接口有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testCompress += 1;
            }
            else {
                param.testCompress += 0;
            }
            if (jieKouShenLou3.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("加压1.0兆帕接口有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testCompress += 1;
            }
            else {
                param.testCompress += 0;
            }
            if (jieKouShenLou4.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("加压1.0兆帕接口有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testCompress += 1;
            }
            else {
                param.testCompress += 0;
            }
            if (jieKouShenLou5.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("加压1.0兆帕接口有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testCompress += 1;
            }
            else {
                param.testCompress += 0;
            }

            param.testWeld = "";

            if (hanDianShenLou1.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头点敲击检查有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testWeld += 1;
            }
            else {
                param.testWeld += 0;
            }

            if (hanDianShenLou2.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头点敲击检查有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testWeld += 1;
            }
            else {
                param.testWeld += 0;
            }
            if (hanDianShenLou3.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头点敲击检查有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testWeld += 1;
            }
            else {
                param.testWeld += 0;
            }
            if (hanDianShenLou4.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头点敲击检查有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testWeld += 1;
            }
            else {
                param.testWeld += 0;
            }
            if (hanDianShenLou5.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头点敲击检查有渗漏，不能视为合格!");

                    return;
                }
                else
                    param.testWeld += 1;
            }
            else {
                param.testWeld += 0;
            }


            param.testWeldCheck = "";

            if (hanDianFanBian1.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头有翻边现象，不能视为合格!");

                    return;
                }
                else
                    param.testWeldCheck += 1;
            }
            else {
                param.testWeldCheck += 0;
            }

            if (hanDianFanBian2.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头有翻边现象，不能视为合格!");

                    return;
                }
                else
                    param.testWeldCheck += 1;
            }
            else {
                param.testWeldCheck += 0;
            }
            if (hanDianFanBian3.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头有翻边现象，不能视为合格!");

                    return;
                }
                else
                    param.testWeldCheck += 1;
            }
            else {
                param.testWeldCheck += 0;
            }
            if (hanDianFanBian4.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头有翻边现象，不能视为合格!");

                    return;
                }
                else
                    param.testWeldCheck += 1;
            }
            else {
                param.testWeldCheck += 0;
            }
            if (hanDianFanBian5.isOn) {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("焊接接头有翻边现象，不能视为合格!");

                    return;
                }
                else
                    param.testWeldCheck += 1;
            }
            else {
                param.testWeldCheck += 0;
            }
            if (baoYaStartTime.text == "") {
                UIManager.instance.ShowLoggerMsg("保压开始时间未填写");

                return;
            }
            //  Debug.Log(baoYaStartTime.text);
            string timeStart = baoYaStartTime.text;
            int nian = int.Parse(timeStart.Split('/')[0]);
            // timeStart = timeStart.Split('/')[1];
            int yue = int.Parse(timeStart.Split('/')[1]);
            // Debug.Log(timeStart);
            timeStart = timeStart.Split('/')[2];
            int ri = int.Parse(timeStart.Split(' ')[0]);
            timeStart = timeStart.Split(' ')[1];
            int shi = int.Parse(timeStart.Split(':')[0]);
            timeStart = timeStart.Split(':')[1];
            // int fen = int.Parse(timeStart.Split('分')[0]);
            int fen = int.Parse(timeStart);
            param.testKeepStart = Util.DateTimeToTimeStamp(nian, yue, ri, shi, fen);
            if (baoYaEndTime.text == "") {
                UIManager.instance.ShowLoggerMsg("保压结束时间未填写");

                return;
            }
            timeStart = baoYaEndTime.text;
            nian = int.Parse(timeStart.Split('/')[0]);
            // timeStart = timeStart.Split('/')[1];
            yue = int.Parse(timeStart.Split('/')[1]);
            timeStart = timeStart.Split('/')[2];
            ri = int.Parse(timeStart.Split(' ')[0]);
            timeStart = timeStart.Split(' ')[1];
            shi = int.Parse(timeStart.Split(':')[0]);
            timeStart = timeStart.Split(':')[1];
            fen = int.Parse(timeStart);
            // nian = int.Parse(timeStart.Split('年')[0]);
            // timeStart = timeStart.Split('年')[1];
            // yue = int.Parse(timeStart.Split('月')[0]);
            // timeStart = timeStart.Split('月')[1];
            // ri = int.Parse(timeStart.Split('日')[0]);
            // timeStart = timeStart.Split('日')[1];
            //shi = int.Parse(timeStart.Split('时')[0]);
            // timeStart = timeStart.Split('时')[1];
            //  fen = int.Parse(timeStart.Split('分')[0]);
            param.testKeepEnd = Util.DateTimeToTimeStamp(nian, yue, ri, shi, fen);

            param.testOperatePressure = yunXingYaLi.value + 1;
            param.testCheckPressure = jianCeYaLi.value + 1;

            param.testNotice = "";
            if (zhuYi1.isOn) {
                param.testNotice += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("管道使用注意事项告知未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testNotice += 0;
            }
            if (zhuYi2.isOn) {
                param.testNotice += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("管道使用注意事项告知未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testNotice += 0;
            }
            if (zhuYi3.isOn) {
                param.testNotice += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("管道使用注意事项告知未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testNotice += 0;
            }
            if (zhuYi4.isOn) {
                param.testNotice += 1;
            }
            else {
                if (heGe.IsOn) {
                    UIManager.instance.ShowLoggerMsg("管道使用注意事项告知未全部填写，不能视为合格!");

                    return;
                }
                else
                    param.testNotice += 0;
            }
            if (gongjiangCard1 != null)
                if (gongjiangCard1.isOn) {
                    param.cardType = "1";
                }
            if (gongjiangCard2 != null)
                if (gongjiangCard2.isOn) {
                    param.cardType = "2";
                }
            if (gongjiangCard3 != null)
                if (gongjiangCard3.isOn) {
                    param.cardType = "3";
                }
            if (gongjiangCard1 != null && gongjiangCard2 != null && gongjiangCard3 != null)
                if (!gongjiangCard1.isOn && !gongjiangCard2.isOn && !gongjiangCard3.isOn) {
                    UIManager.instance.ShowLoggerMsg("必须选择一种工匠卡类型!");

                    return;
                }

            if (heGe.IsOn) {
                // Debug.Log("aaa---------");
                UIManager.instance.ShowCommitMsg("是否确认此订单为合格?", "提示", delegate {

                    param.orderStatus = 3;
                    GameObject.Find("Camera").GetComponent<InterfaceManager>().TakeTestPressure(param);
                });


            }
            else {
                // Debug.Log("bbbbbbb---------");
                UIManager.instance.ShowCommitMsg("是否确认此订单为不合格?", "提示", delegate {

                    param.orderStatus = 4;
                    GameObject.Find("Camera").GetComponent<InterfaceManager>().TakeTestPressure(param);
                });

            }




        }
        /// <summary>
        /// 当合格和不合格界面
        /// </summary>
        /// <param name="value"></param>
        void OnChooseHeGe(bool value) {

            buHeGe.IsOn = !value;
            if (value != true)
                UIManager.instance.ShowLoggerMsg("请编辑试压不合格内容！", "提示", delegate {

                    OpenBuHeGeArray();
                    ChangeDetailPanel(0);
                });
            else
                HideBuHeGeArray();
        }

        void OnChooseBuHeGe(bool value) {
            heGe.IsOn = !value;
            if (value != true)
                HideBuHeGeArray();
            else
                UIManager.instance.ShowLoggerMsg("请编辑试压不合格内容！", "提示", delegate {

                    OpenBuHeGeArray();
                    ChangeDetailPanel(0);
                });
        }

        void OpenBuHeGeArray() {
            if (buhege)
                buhege.transform.parent.gameObject.SetActive(true);
            if (buhegeInput)
                buhegeInput.gameObject.SetActive(true);
            if (UtilManager.getInstance.buhegeParent)
                UtilManager.getInstance.buhegeParent.gameObject.SetActive(true);
        }

        void HideBuHeGeArray() {
            if (buhege)
                buhege.transform.parent.gameObject.SetActive(false);
            if (buhegeInput)
                buhegeInput.gameObject.SetActive(false);
            if (UtilManager.getInstance.buhegeParent)
                UtilManager.getInstance.buhegeParent.gameObject.SetActive(false);
        }

        /// <summary>
        ///选择屋顶
        /// </summary>
        /// <param name="value"></param>
        void OnChooseWuDing(bool value) {
            if (value) {
                qiangMian.isOn = false;
                diMian.isOn = false;
            }
        }
        /// <summary>
        ///选择地面
        /// </summary>
        /// <param name="value"></param>
        void OnChooseDiMian(bool value) {
            if (value) {
                wuDing.isOn = false;
                qiangMian.isOn = false;
            }
        }
        /// <summary>
        ///选择墙面
        /// </summary>
        /// <param name="value"></param>
        void OnChooseQiangMian(bool value) {
            if (value) {
                wuDing.isOn = false;
                diMian.isOn = false;
                // qiangMian.isOn = false;
            }
        }
        /// <summary>
        /// 检测安装工人的电话号码
        /// </summary>
        /// <param name="text"></param>
        void CheckPhoneNum(string text) {
            if (text.Length == 11) {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().CheckBuilderByPhone(text);


            }
            else {
                UIManager.instance.ShowLoggerMsg("请输入11位手机号!");
                anZhuangPhone.text = "";
            }

        }

        private int buildId = 0;
        /// <summary>
        /// -1 试压员 不能提交
        /// 0 非系统用户 
        /// 1 系统用户
        /// </summary>
        private int buildType = 0;
        /// <summary>
        /// 检测当前的手机号是否存在
        /// </summary>
        /// <param name="obj"></param>
        private void OnCheckBuilder(object[] obj) {
            Builder m_builder = (Builder)obj[1];
            buildId = m_builder.id;
            if (buildId == 0) {
                UIManager.instance.ShowLoggerMsg("该安装者非系统用户，试压成功将无法获得积分，工匠卡");
            }
            else {
                if (m_builder.area != UserData.instance.area) {
                    GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAreaDetail(m_builder.area, (string area) => {
                        UIManager.instance.ShowLoggerMsg(string.Format("该安装者属于{0}，试压成功将只能获得积分，无法获得工匠卡", area));
                    });
                }
            }
        }
        /// <summary>
        /// 选择安装和预约人是否一致
        /// </summary>
        /// <param name="value"></param>
        void OnAnZhuangYuYueChange(bool value) {
            if (value) {
                anZhuangPhone.text = currentShowOrder.userPhone;
                anZhuangPhone.interactable = false;
                anZhuangName.text = currentShowOrder.userName;
                anZhuangName.interactable = false;
                CheckPhoneNum(currentShowOrder.userPhone);
            }
            else {
                anZhuangPhone.interactable = true;
                anZhuangName.interactable = true;
                anZhuangPhone.text = "";
                anZhuangName.text = "";
            }
        }


        int currentPanel = 0;
        /// <summary>
        /// 切换细节界面
        /// </summary>
        /// <param name="index"></param>
        void ChangeDetailPanel(int index) {

            if (currentPanel == index) return;


            if (index == 0) {
                detail_1_panel.transform.Find("content").GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                detail_1_panel.gameObject.SetActive(true);
                detail_2_panel.gameObject.SetActive(false);
                detail_1.transform.Find("state").GetComponent<Image>().color = new Color32(38, 149, 159, 255);
                detail_1.transform.Find("label").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                detail_2.transform.Find("state").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                detail_2.transform.Find("label").GetComponent<Text>().color = new Color32(50, 50, 50, 255);
                currentPanel = index;
            }
            else {
                UIManager.instance.ShowCommitMsg("是否确认不连接智能试压仪？", "提示", delegate {
                    detail_2_panel.transform.Find("content").GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                    detail_1_panel.gameObject.SetActive(false);
                    detail_2_panel.gameObject.SetActive(true);
                    detail_2.transform.Find("state").GetComponent<Image>().color = new Color32(38, 149, 159, 255);
                    detail_2.transform.Find("label").GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                    detail_1.transform.Find("state").GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    detail_1.transform.Find("label").GetComponent<Text>().color = new Color32(50, 50, 50, 255);
                    currentPanel = index;
                });

            }


        }



        void PosImage(string _typeName, string _path) {

            //   StartCoroutine(GameObject.Find("Camera").GetComponent<InterfaceManager>().PostImage("头像.png", "测试图片", typeName, "image/jpg", OnLocalPhotoJson));
            StartCoroutine(GameObject.Find("Camera").GetComponent<InterfaceManager>().PostImage(_path, "测试图片", _typeName, "image/jpg", OnLocalPhotoJson));
        }
        /// <summary>
        /// photo返回结果
        /// </summary>
        /// <param name="result"></param>
        private void OnLocalPhotoJson(JsonData result) {
            int status = int.Parse(result["status"].ToString());
            if (status != 1) {
                if (UtilManager.getInstance.loadingPanel.activeInHierarchy) {
                    Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "图片上传失败，请稍候再试!", delegate {
                        //  Debug.Log(NetWorkError);
                        UtilManager.getInstance.messageAlert.SetActive(false);
                        UtilManager.getInstance.loadingPanel.SetActive(false);
                    });
                }
                EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, false);
#if UNITY_EDITOR
                Util.ShowErrorMessage(status);
                Debug.LogError("OnLocalPhotoJson >>>>error status:" + status);
#endif
                return;
            }
            result = result["data"];
            if (result == null)
                return;
#if UNITY_EDITOR
            Debug.Log(result["param"].ToString());
            Debug.Log(result["url"].ToString());
#endif
            string key = result["param"].ToString();
            switch (key) {
                case "menpai":
                    SetImage(UtilManager.getInstance.menpaiParent, result, UtilManager.getInstance.menpaiDic, "menpai");
                    break;
                case "state":
                    SetImage(UtilManager.getInstance.stateParent, result, UtilManager.getInstance.stateDic, "state");
                    break;
                case "chufang":
                    SetImage(UtilManager.getInstance.chufangParent, result, UtilManager.getInstance.chufangDic, "chufang");
                    break;
                case "weishengjian":
                    SetImage(UtilManager.getInstance.weishengjianParent, result, UtilManager.getInstance.weishengjianDic, "weishengjian");
                    break;
                case "keting":
                    SetImage(UtilManager.getInstance.ketingParent, result, UtilManager.getInstance.ketingDic, "keting");
                    break;
                case "guodao":
                    SetImage(UtilManager.getInstance.guodaoParent, result, UtilManager.getInstance.guodaoDic, "guodao");
                    break;
                case "yangtai":
                    SetImage(UtilManager.getInstance.yangtaiParent, result, UtilManager.getInstance.yangtaiDic, "yangtai");
                    break;
                case "other":
                    SetImage(UtilManager.getInstance.otherParent, result, UtilManager.getInstance.otherDic, "other");
                    break;
                case "pingzheng":
                    SetImage(UtilManager.getInstance.pingzhengParent, result, UtilManager.getInstance.pingzhengDic, "pingzheng");
                    break;
                case "buhege":
                    SetImage(UtilManager.getInstance.buhegeParent, result, UtilManager.getInstance.buhegeDic, "buhege");
                    break;
            }




            //UIManager.instance.LoadImageFromUrl(result["url"].ToString(), 300, 500, delegate {

            //    switch (key)
            //    {
            //        case "jinchang":
            //            currentImage = jinChang.transform.GetComponentInChildren<RawImage>();
            //            break;
            //        case "shiya":
            //            currentImage = shiYa.transform.GetComponentInChildren<RawImage>();
            //            break;
            //        case "keting":
            //            currentImage = keTing.transform.GetComponentInChildren<RawImage>();
            //            break;
            //        case "woshi":
            //            currentImage = woShi.transform.GetComponentInChildren<RawImage>();
            //            break;
            //        case "weishengjian":
            //            currentImage = weiShengJian.transform.GetComponentInChildren<RawImage>();
            //            break;
            //        case "chufang":
            //            currentImage = chuFang.transform.GetComponentInChildren<RawImage>();
            //            break;
            //        case "yangtai":
            //            currentImage = yangTai.transform.GetComponentInChildren<RawImage>();
            //            break;
            //        case "zoulang":
            //            currentImage = zouLang.transform.GetComponentInChildren<RawImage>();
            //            break;

            //    }

            //    return currentImage;
            //    // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            //});
            ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
            //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(result["url"].ToString(), 300, 500, delegate {

            //    return currentImage;
            //    // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            //}));

        }

        private void SetImage(Transform _parent, JsonData result, Dictionary<string, string> _dic, string _typeStr) {

            Transform itemTrans = Instantiate(imageArrayItem).transform;
            itemTrans.SetParent(_parent);
            RectTransform rect = itemTrans.GetComponent<RectTransform>();
            rect.localScale = Vector3.one;
            rect.anchoredPosition3D = Vector3.zero;
            RawImage view = itemTrans.GetComponentInChildren<RawImage>();
            ToBigImageHelper toBig = itemTrans.GetComponentInChildren<ToBigImageHelper>();
            toBig.typeStr = _typeStr;

            UIManager.instance.LoadImageFromUrl(result["url"].ToString(), 700, 0, delegate {
                string timeName = DateTime.Now.ToString();
                _dic.Add(timeName, result["url"].ToString());
                itemTrans.name = timeName;
                if (UtilManager.getInstance.photoKey.ContainsKey(result["param"].ToString())) {
                    UtilManager.getInstance.photoKey[result["param"].ToString()] = _dic;
                }
                else {
                    UtilManager.getInstance.photoKey.Add(result["param"].ToString(), _dic);
                }
                return view;
                // EventManager.instance.NotifyEvent(XMWorkspace.Event.PostImageJson, "已完成图片加载功能!");
            }, delegate {
                UtilManager.getInstance.loadingPanel.SetActive(false);
                seleftPanel.SetActive(false);
                return view.transform.parent.GetComponent<LayoutElement>();
            });
        }
    }

}
