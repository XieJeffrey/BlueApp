using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 收藏界面
    /// </summary>
    public class ShouCangPanel : Panel,IRefresh
    {

        protected UIEventListener back;

        protected Item prefab;
        protected Item top;
        protected List<Item> forumsPool = new List<Item>();
        protected List<Item> currentForums = new List<Item>();

        protected ScrollItem st;
        protected int page=1;
        protected int pageCount=10;
        protected int currentShowCount;

         void Start()
        {
            GetCompent();
             prefab = transform.Find("dragPanel").Find("offset").Find("content").Find("forumItem").GetComponent<Item>();
          
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetCollectionList, OnGetCollectionList);

        }

        public override void Show()
        {
            gameObject.SetActive(true);
            if (top == null)
                top = transform.Find("dragPanel").Find("offset").Find("content").Find("topItem").GetComponent<Item>();
            HideAllCurrentForum();
            top.SetItemData(0);
            page = 1;
            GetOrderList();

        }
        /// <summary>
        /// 给属性赋值
        /// </summary>
        protected virtual void GetCompent()
        {
            back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = delegate {  Hide(); };
            st = transform.Find("dragPanel").Find("offset").GetComponent<ScrollItem>();
            if(st)
            st.onEndDrag = OnEndDrag;
        }
          void OnEndDrag(Vector2 obj)
        {
            //刷新
            if (obj.y < -100)
            {
                page = 1;
                HideAllCurrentForum();
                top.SetItemData(0);
                GetOrderList();
            }
            //翻页
            else if (obj.y > 100)
            {
                page++;
                GetOrderList();
            }
        }

        protected virtual void GetOrderList()
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetCollectionList(page, pageCount);

        }
        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="obj"></param>
        protected void OnGetCollectionList(object[] obj)
        {
            if ((bool)obj[0])
            {

                List<Forum> orders = (List<Forum>)obj[1];
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
                    //creatItem.gameObject.SetActive(true);
                }
                currentShowCount = total;
                page = (int)obj[3];

            }
            //HideAllCurrentForum();
            //currentForums.Clear();

                //if ((bool)obj[0])
                //{

                //    List<Forum> datas = (List<Forum>)obj[1];

                //    //需要创建的预制体数量
                //    int creatNum = datas.Count - forumsPool.Count;
                //    ///创建帖子的预制体
                //    for(int i=0;i< creatNum;i++)
                //    {
                //        Item temp = Instantiate<Item>(prefab, prefab.transform.parent);
                //        forumsPool.Add(temp);
                //    }
                //    //按照获取的列表赋值
                //    for (int i=0;i< datas.Count; i++)
                //    {

                //        Item temp = forumsPool[i];
                //        temp.SetItemData(datas[i]);
                //        temp.gameObject.SetActive(true);
                //        currentForums.Add(temp);
                //    }

                //}else
                //{


                //}

        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            ClearCache();
            UIManager.instance.BackToLastPanel();

        }


       
        /// <summary>
        /// 隐藏所有的当前帖子
        /// </summary>
       protected   void HideAllCurrentForum()
        {

            for(int i=0;i< currentShowCount; i++)
            {
                currentForums[i].gameObject.SetActive(false);
            }
            currentShowCount = 0;
        }

        public void Reload()
        {
            Show();

        }

        public void ClearCache()
        {
#if UNITY_EDITOR
            Debug.Log("清空收藏列表");
#endif
            currentShowCount = 0;
            for (int i = 0; i < currentForums.Count; i++)
            {
               Destroy( currentForums[i].gameObject);
            }
            currentForums.Clear();
        }
    }

}
