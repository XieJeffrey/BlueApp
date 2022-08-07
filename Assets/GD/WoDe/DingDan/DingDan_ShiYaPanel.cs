using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 试压员的试压订单列表
    /// </summary>
    public class DingDan_ShiYaPanel : Panel,IRefresh
    {

        private DingDanItem prefab;
        private UIEventListener back;
        private ScrollItem st;

        private List<DingDanItem> currentItems = new List<DingDanItem>();

        private int page=1;
        int Page
        {
            get
            {
                return page;
            }
            set
            {
                if (value < 1)
                {
                    page = 1;
                }
                else
                {
                    page = value;
                }
            }
        }
        private int pageCount=10;
        private int currentShowCount = 0;

        private void Start()
        {
           // back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
          //  back.onClick = delegate { Hide(); };
            st = transform.Find("list").Find("scroll").GetComponent<ScrollItem>();
            st.onEndDrag = OnEndDrag;
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetAppointmentList, OnGetOrderList);

        }

        /// <summary>
        /// 刷新操作
        /// </summary>
        /// <param name="obj"></param>
        private void OnEndDrag(Vector2 obj)
        {

            //刷新
            if (obj.y < -100)
            {
                page = 1;
                HideAllCurrentShow();
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
            gameObject.SetActive(true);
            HideAllCurrentShow();
            //获取我试压的订单
            GetOrderList();
        }
        public override void Hide()
        {
            ClearCache();
            UIManager.instance.BackToLastPanel();
            gameObject.SetActive(false);
        }

        void GetOrderList()
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAppointmentOrderList(3, 2, page, pageCount, "");
        }

        /// <summary>
        /// 获取试压列表成功后
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetOrderList(object[] obj)
        {
            if (!gameObject.activeSelf) return;
            if ((bool)obj[0])
            {
                List<Order> orders = (List<Order>)obj[1];
                ///当前已经存在的个数
                int currentCount = currentItems.Count;

                //当前需要显示的个数
                int total = currentShowCount + orders.Count;

                for(int i= currentShowCount;i< total; i++)
                {
                    DingDanItem creatItem;
                    //历史存在的无需创建
                    if (i < currentCount)
                    {
                        creatItem = currentItems[i];
                    }
                    else
                    {
                        if (prefab == null)
                        {
                            prefab = transform.Find("list").Find("scroll").Find("content").Find("dingdan_item").GetComponent<DingDanItem>();
                        }
                        creatItem = Instantiate<DingDanItem>(prefab, prefab.transform.parent);
                        currentItems.Add(creatItem);
                    }
                    creatItem.SetItemData(orders[i- currentShowCount]);
                    creatItem.gameObject.SetActive(true);
                }
                currentShowCount = total;
                page = (int)obj[3];
            }
        }
       

        /// <summary>
        /// 关闭当前显示的订单
        /// </summary>
        private void HideAllCurrentShow()
        {

            for(int i = 0; i < currentShowCount; i++)
            {
                currentItems[i].gameObject.SetActive(false);
            }
            currentShowCount = 0;
        }

        public void Reload()
        {
            Show();
        }

        public void ClearCache()
        {
            for (int i = 0; i < currentShowCount; i++)
            {
               Destroy( currentItems[i].gameObject);
            }
            currentItems.Clear();
            currentShowCount = 0;
        }
    }


}