using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;

namespace GD {

    /// <summary>
    /// 搜索帖子的结果界面
    /// </summary>
    public class ForumSearchPanel : Panel,IRefresh
    {

        /// <summary>
        /// 当前显示的帖子类型
        /// 0 全部帖子
        /// 1 动态分享
        /// 2 工艺展示
        /// 3 寻找工友
        /// 4 问题咨询
        /// </summary>
        private int currentType = -1;

        protected ScrollItem st;
        protected int page = 1;
        protected int pageCount = 5;
        protected int currentShowCount;
        private string[] condition;
        /// <summary>
        /// 帖子的预制体
        /// </summary>
        private forumItem prefab;
        private forumItem top;
        /// <summary>
        /// 返回键
        /// </summary>
        private UIEventListener back;

        private MySearch searchButton;
        /// <summary>
        /// 当前查看的帖子
        /// </summary>
        private List<forumItem> currentForums = new List<forumItem>();

        public override void Hide()
        {
           // gameObject.SetActive(false);
            ClearCache();
            UIManager.instance.BackToLastPanel();
        }

       
        public override void Show()
        {


        }

        void GetCompent()
        {
            if (prefab == null)
            {
                prefab = transform.Find("dragPanel").Find("offset").Find("content").Find("forumItem").GetComponent<forumItem>();
                top = transform.Find("dragPanel").Find("offset").Find("content").Find("topItem").GetComponent<forumItem>();
                back = transform.Find("searchbar").Find("btn_back").GetComponent<UIEventListener>();
                searchButton = transform.Find("searchbar").GetComponent<MySearch>();
                searchButton.OnClick = OnSearch;
                st = transform.Find("dragPanel").Find("offset").GetComponent<ScrollItem>();
                st.onEndDrag = OnEndDrag;
                back.onClick = delegate { Hide(); };
                EventManager.instance.RegisterEvent(XMWorkspace.Event.GetForumList, OnGetForumList);
            }

        }
        void OnEndDrag(Vector2 obj)
        {
            //刷新
            if (obj.y < -100)
            {
                page = 1;
                HideAllCurrentForum();
                top.SetItemData(0);
                GetForumList();
            }
            //翻页
            else if (obj.y > 100)
            {
                page++;
                GetForumList();
            }
        }
        private void OnSearch(string obj)
        {
            Show(currentType, new string[] { obj });

        }

        /// <summary>
        /// 论坛帖子列表
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetForumList(object[] obj)
        {
            if (!gameObject.activeSelf) return;
            if ((bool)obj[0])
            {

                List<Forum> orders = (List<Forum>)obj[1];
                ///当前已经存在的个数
                int currentCount = currentForums.Count;

                //当前需要显示的个数
                int total = currentShowCount + orders.Count;

                for (int i = currentShowCount; i < total; i++)
                {
                    forumItem creatItem;
                    //历史存在的无需创建
                    if (i < currentCount)
                    {
                        creatItem = currentForums[i];
                    }
                    else
                    {
                        creatItem = Instantiate<forumItem>(prefab, prefab.transform.parent);
                        currentForums.Add(creatItem);
                    }
                    creatItem.SetItemData(orders[i - currentShowCount]);
                   // creatItem.gameObject.SetActive(true);
                }
              //  UIManager.instance.StartLoad();
                currentShowCount = total;
                page = (int)obj[3];

            }
            //if ((bool)obj[0])
            //{
            //    List<Forum> forumList = (List<Forum>)obj[1];
            //    int currentCount = currentForums.Count;
            //    int creatCount = forumList.Count + pageCount * (page - 1) - currentCount;
            //    for (int i = 0; i < creatCount; i++)
            //    {
            //        forumItem temp = Instantiate<forumItem>(prefab, prefab.transform.parent);
            //        currentForums.Add(temp);
            //    }
            //    for (int i = 0; i < forumList.Count; i++)
            //    {
            //        currentForums[pageCount * (page - 1) + i].gameObject.SetActive(true);
            //        currentForums[pageCount * (page - 1) + i].SetItemData(forumList[i]);
            //    }
            //}
        }

        /// <summary>
        /// 获取论坛帖子列表
        /// </summary>
        /// <param name="index"></param>
        void GetForumList()
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetForumList(currentType, page, pageCount, condition);
        }
        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        public  void Show(int index, string[] _condition)
        {
           
            gameObject.SetActive(true);
          //  UIManager.instance.Clear();
            currentType = index;
            condition = _condition;
            page = 1;
            HideAllCurrentForum();
            GetCompent();
            top.SetItemData(0);
            GetForumList();
        }
        /// <summary>
        /// 隐藏所有的元素
        /// </summary>
        void HideAllCurrentForum()
        {
            for(int i = 0; i < currentShowCount; i++)
            {
                currentForums[i].gameObject.SetActive(false);
            }
            currentShowCount = 0;
        }

        public void Reload()
        {
            Show(currentType, condition);
        }

        public void ClearCache()
        {
            gameObject.SetActive(false);
            for (int i = 0; i < currentForums.Count; i++)
            {
                Destroy(currentForums[i].gameObject);
            }
            currentForums.Clear();
            currentShowCount = 0;
            UIManager.instance.RefrehImageCache();
        }
    }


}


