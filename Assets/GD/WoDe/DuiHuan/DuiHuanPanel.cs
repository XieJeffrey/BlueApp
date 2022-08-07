using LM_Workspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;

namespace GD
{

    public class DuiHuanPanel : ShouCangPanel, IRefresh
    {
        protected UIEventListener jiXuDuiHuan;
        // Use this for initialization
        void Start()
        {
            GetCompent();
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetMyExchangeList, OnGetCollectionList);

        }

        protected override void GetCompent()
        {
            base.GetCompent();
            prefab = transform.Find("dragPanel").Find("offset").Find("content").Find("shangpin_item").GetComponent<Item>();
            jiXuDuiHuan= transform.Find("bottom").Find("btn_duihuan").GetComponent<UIEventListener>();
            jiXuDuiHuan.onClick = delegate { UtilManager.getInstance.marketGo.SetActive(true); };

        }


        public override void Show()
        {
            gameObject.SetActive(true);

            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetMyExchangeList(1, 50);

        }
        protected override void GetOrderList()
        {
          
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetCollectionList(page, pageCount);

        }

        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="obj"></param>
        protected new void OnGetCollectionList(object[] obj)
        {
            if ((bool)obj[0])
            {
                List<HistoryOrder> orders = (List<HistoryOrder>)obj[1];
                ///当前已经存在的个数
                int currentCount = currentForums.Count;

                //当前需要显示的个数
                int total = currentShowCount + orders.Count;

                for (int i = currentShowCount; i < total; i++)
                {
                    Item creatItem;
                    //历史存在的无需创建
                    if (i < currentCount)
                    {
                        creatItem = currentForums[i];
                    }
                    else
                    {
                        creatItem = Instantiate<Item>(prefab, prefab.transform.parent);
                        currentForums.Add(creatItem);
                    }
                    creatItem.SetItemData(orders[i - currentShowCount]);
                    creatItem.gameObject.SetActive(true);
                }
                currentShowCount = total;
                page = (int)obj[3];

            }
           


            //HideAllCurrentForum();
            //currentForums.Clear();
            //if ((bool)obj[0])
            //{
            //    List<HistoryOrder> datas = (List<HistoryOrder>)obj[1];

            //    //需要创建的预制体数量
            //    int creatNum = datas.Count - forumsPool.Count;
            //    ///创建帖子的预制体
            //    for (int i = 0; i < creatNum; i++)
            //    {
            //        Item temp = Instantiate<Item>(prefab, prefab.transform.parent);
            //        forumsPool.Add(temp);
            //    }
            //    //按照获取的列表赋值
            //    for (int i = 0; i < datas.Count; i++)
            //    {

            //        Item temp = forumsPool[i];
            //        temp.SetItemData(datas[i]);
            //        temp.gameObject.SetActive(true);
            //        currentForums.Add(temp);
            //    }

            //}
            //else
            //{


            //}

        }
    }
}