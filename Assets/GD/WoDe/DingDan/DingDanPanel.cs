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
    /// 我的订单界面 水工
    /// </summary>
    public class DingDanPanel : Panel
    {
        /// <summary>
        /// 返回按钮
        /// </summary>
        private UIEventListener back;
        /// <summary>
        /// 搜索按钮
        /// </summary>
        private UIEventListener search;

        /// <summary>
        /// 全部订单
        /// </summary>
        private UIEventListener all;
        /// <summary>
        /// 合格订单
        /// </summary>
        private UIEventListener heGe;
        /// <summary>
        /// 不合格订单
        /// </summary>
        private UIEventListener buHeGe;

        /// <summary>
        /// 子查找按钮
        /// </summary>
        private UIEventListener subSearch;
        /// <summary>
        /// 输入菜单
        /// </summary>
        private InputField input;
        /// <summary>
        /// 查找界面
        /// </summary>
        private GameObject searchPanel;
        /// <summary>
        /// 订单信息预制
        /// </summary>
        private DingDanItem prefab;

        private Text title;
        private ScrollItem st;
        /// <summary>
        /// 订单信息缓存
        /// </summary>
        private List<DingDanItem> itemPools=new List<DingDanItem>();
        /// <summary>
        /// 订单信息当前显示的
        /// </summary>
        private List<DingDanItem> currentIems=new List<DingDanItem>();
        /// <summary>
        /// 默认颜色
        /// </summary>
        private Color32 normalColor=new Color32(50,50,50,255);
        /// <summary>
        /// 选中颜色
        /// </summary>
        private Color32 SelectColor = new Color32(38, 149, 159, 255);

        /// <summary>
        /// 订单类型
        /// 1 预约
        /// 2 安装
        /// 3 试压
        /// </summary>
        private int dingDanType = 1;
        private int page = 1;
        private int pageCount = 10;
        private int currentShowCount = 0;
     
        /// <summary>
        /// 设置按钮状态信息
        /// 
        /// 0 全部
        /// 3 合格
        /// 4 不合格
        /// </summary>
        int currentState = 0;
        List<Order> orderData;

        private void Start()
        {          
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetAppointmentList, OnGetOrderList);
        }

        void GetCompent()
        {
            if (back == null)
            {
                back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
                search = transform.Find("header").Find("normalState").Find("btn_search").GetComponent<UIEventListener>();
                all = transform.Find("topbar").Find("btn_all").GetComponent<UIEventListener>();
                heGe = transform.Find("topbar").Find("btn_hege").GetComponent<UIEventListener>();
                buHeGe = transform.Find("topbar").Find("btn_buhege").GetComponent<UIEventListener>();

                searchPanel = transform.Find("header").Find("searchState").gameObject;
                subSearch = searchPanel.transform.Find("btn_search").GetComponent<UIEventListener>();
                input = searchPanel.transform.Find("input_search").GetComponent<InputField>();
                title= transform.Find("header").Find("normalState").Find("label").GetComponent<Text>();
                st = transform.Find("dragPanel").Find("scroll").GetComponent<ScrollItem>();
                st.onEndDrag = OnEndDrag;
                // Show();
                back.onClick = delegate { Hide(); };
                search.onClick = delegate { SetSearchState(true); };
                subSearch.onClick = SearchOrderList;
                all.onClick = AllDingDan;
                heGe.onClick = HeGeDingDan;
                buHeGe.onClick = BuHeGeDingDan;

            }
            if (prefab == null)
                prefab = transform.Find("dragPanel").Find("scroll").Find("content").Find("dingdan_item_wode").GetComponent<DingDanItem>();
        }

        private void OnEndDrag(Vector2 obj)
        {
            //刷新
            if (obj.y < -100)
            {
                page = 1;
                HideAllCurrentItemState();
                GetOrderList();
            }
            //翻页
            else if (obj.y > 100)
            {
                page++;
                GetOrderList();
            }
        }

        public override void Show()
        {
           // gameObject.SetActive(true);

           // GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAppointmentOrderList(1, 0, 1, 50, "");

        }
        public  void Show(int type)
        {
            dingDanType = type;
            gameObject.SetActive(true);
            GetCompent();
           
            SetButtonState(0);
            SetSearchState(false);
            HideAllCurrentItemState();
            if (input)
                input.text = "";
            if (dingDanType == 1)
            {
                title.text = "我的预约";
            }else if(dingDanType == 2)
            {
                title.text = "我的安装";
            }else
            {
                title.text = "我的试压";
            }
           
            GetOrderList();
        }
        /// <summary>
        /// 获取订单列表
        /// </summary>
        public void GetOrderList()
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAppointmentOrderList(dingDanType, currentState, page, pageCount, input.text);
           
        }
        
        /// <summary>
        /// 查找订单信息
        /// </summary>
        void SearchOrderList(GameObject go, PointerEventData data)
        {
            HideAllCurrentItemState();
           
            page = 1;
            string searchMsg = input.text;
            if (searchMsg == "")
            {
                searchMsg = " ";
            }
            Debug.Log(searchMsg);
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAppointmentOrderList(dingDanType, currentState, page, pageCount, searchMsg);
            SetSearchState(false);
        }
        /// <summary>
        /// 查看不合格订单
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void BuHeGeDingDan(GameObject go, PointerEventData data)
        {
            if (currentState == 4)
            {
                return;
            }
            SetButtonState(4);
            HideAllCurrentItemState();
            page = 1;
            GetOrderList();
        

        }
        /// <summary>
        /// 查看合格订单
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void HeGeDingDan(GameObject go, PointerEventData data)
        {
            if (currentState == 3)
            {
                return;
            }
            SetButtonState(3);

            HideAllCurrentItemState();
            page = 1;
            GetOrderList();
           
        }
        /// <summary>
        /// 查看所有订单
        /// </summary>
        /// <param name="go"></param>
        private void AllDingDan(GameObject go, PointerEventData data)
        {
            if (currentState == 0)
            {
                return;
            }
            SetButtonState(0);
            HideAllCurrentItemState();
            page = 1;
            GetOrderList();
        
        }
        /// <summary>
        /// 隐藏全部订单
        /// </summary>
        public void HideAllCurrentItemState()
        {
            for (int i = 0; i < currentShowCount; i++)
            {
                currentIems[i].gameObject.SetActive(false);
            }
            currentShowCount = 0;
          
        }
        #region  备份
        /// <summary>
        /// 显示全部订单
        /// </summary>
        void ShowAllCurrentItemState()
        {
            //for (int i = 0; i < orderData.Count; i++)
            //{
            //    currentIems[i].gameObject.SetActive(true);
            //}
        }
        /// <summary>
        /// 显示合格订单
        /// </summary>
        void ShowHeGeCurrentItemState()
        {
            //for (int i = 0; i < orderData.Count; i++)
            //{
            //    if (orderData[i].orderStatus == "3")
            //    {
            //        currentIems[i].gameObject.SetActive(true);
            //    }
            //    else
            //    {
            //        currentIems[i].gameObject.SetActive(false);
            //    }
            //}
        }
        /// <summary>
        /// 显示不合格订单
        /// </summary>
        void ShowBuHeGeCurrentItemState()
        {
            //for (int i = 0; i < orderData.Count; i++)
            //{
            //    if (orderData[i].orderStatus == "4")
            //    {
            //        currentIems[i].gameObject.SetActive(true);
            //    }
            //    else
            //    {
            //        currentIems[i].gameObject.SetActive(false);
            //    }
            //}
        }
        #endregion

        /// <summary>
        /// 获取清单成功后
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetOrderList(object[] obj)
        {
            //  Debug.Log(DataManager.instance.orderList_lastTime);
            // HideAllCurrentItemState();
            // currentIems.Clear();
            if (!gameObject.activeSelf) return;
            //获取成功
            if ((bool)obj[0])
            {
#if UNITY_EDITOR
                Debug.Log("获取成功");
#endif
                List<Order> orders = (List<Order>)obj[1];
                ///当前已经存在的个数
                int currentCount = currentIems.Count;

                //当前需要显示的个数
                int total = currentShowCount + orders.Count;

                for (int i = currentShowCount; i < total; i++)
                {
                    DingDanItem creatItem;
                    //历史存在的无需创建
                    if (i < currentCount)
                    {
                        creatItem = currentIems[i];
                    }
                    else
                    {
                        creatItem = Instantiate<DingDanItem>(prefab, prefab.transform.parent);
                        currentIems.Add(creatItem);
                    }
                    creatItem.SetItemData(orders[i - currentShowCount]);
                    creatItem.gameObject.SetActive(true);
                }
                currentShowCount = total;
                page = (int)obj[3];
                #region 老版本

                ///创建订单
                // List<Order> orders = (List<Order>)obj[1];
                //  orderData = orders;
                //    if (itemPools.Count< orders.Count)
                //    {
                //        int creatCount = orders.Count - itemPools.Count;
                //        for(int i = 0; i < creatCount; i++)
                //        {
                //            DingDanItem creatItem = Instantiate<DingDanItem>(prefab, prefab.transform.parent);
                //            itemPools.Add(creatItem);
                //        }
                //    }
                //    ///设置订单信息
                //    for(int i=0;i< orders.Count; i++)
                //    {
                //        itemPools[i].SetItemData(orders[i]);
                //        currentIems.Add(itemPools[i]);
                //    }
                //    //显示订单信息
                //    switch (currentState)
                //    {
                //        case 0:
                //            ShowAllCurrentItemState();
                //            break;
                //        case 1:
                //            ShowHeGeCurrentItemState();
                //            break;
                //        case 2:
                //            ShowBuHeGeCurrentItemState();
                //            break;
                //    }

                //}
                //else
                //{

                #endregion

            }

        }
        
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

      

       
        /// <summary>
        /// 重置数据
        /// </summary>
        void ResetState()
        {
            SetButtonState(0);
            HideAllCurrentItemState();
            SetSearchState(false);
        }

        #region 状态显示
        void SetButtonState(int state)
        {
            currentState = state;
            switch (state)
            {
                case 0:
                    all.GetComponent<MyButtonState>().IsSelect = true;
                    heGe.GetComponent<MyButtonState>().IsSelect = false;
                    buHeGe.GetComponent<MyButtonState>().IsSelect = false;

                    break;
                case 3:
                    all.GetComponent<MyButtonState>().IsSelect = false;
                    heGe.GetComponent<MyButtonState>().IsSelect = true;
                    buHeGe.GetComponent<MyButtonState>().IsSelect = false;

                    break;
                case 4:
                    all.GetComponent<MyButtonState>().IsSelect = false;
                    heGe.GetComponent<MyButtonState>().IsSelect = false;
                    buHeGe.GetComponent<MyButtonState>().IsSelect = true;
                    break;
            }

        }
        /// <summary>
        /// 设置查找状态信息
        /// </summary>
        private bool searchState = false;
        void SetSearchState(bool _searchState)
        {
            searchState = _searchState;
            if (_searchState)
            {
                searchPanel.SetActive(true);
                search.gameObject.SetActive(false);
            }
            else
            {
                searchPanel.SetActive(false);
                search.gameObject.SetActive(true);
                input.text = "";
            }
        }

        #endregion



    }
}
