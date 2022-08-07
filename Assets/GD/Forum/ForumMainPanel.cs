using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XMWorkspace;

namespace GD {

    /// <summary>
    /// 论坛首页
    /// </summary>
    public class ForumMainPanel : Panel,IRefresh
    {
        /// <summary>
        /// 搜索组件
        /// </summary>
        private MySearch search;

        /// <summary>
        /// 帖子
        /// </summary>
        private forumItem prefab;
        /// <summary>
        /// 帖子
        /// </summary>
        private forumItem top;
        /// <summary>
        /// 动态分享
        /// </summary>
        private UIEventListener dongTai;
        /// <summary>
        /// 工艺展示
        /// </summary>
        private UIEventListener gongYi;
        /// <summary>
        /// 寻找工友
        /// </summary>
        private UIEventListener gongYou;
        /// <summary>
        /// 问题咨询
        /// </summary>
        private UIEventListener ziXun;
        /// <summary>
        /// 中财官网
        /// </summary>
        private UIEventListener guanWang;
        /// <summary>
        /// 发帖
        /// </summary>
        private UIEventListener faTie;
        /// <summary>
        /// 当前显示的帖子
        /// </summary>
        List<forumItem> currentForums = new List<forumItem>();


        protected ScrollItem st;
        protected int page = 1;
        protected int pageCount = 5;
        protected int currentShowCount=0;

        private void Start()
        {
            //UserData.instance.token = "mwpVJ5K0tjojPkaL";
           // Show();
            //  guanWang = transform.Find("pageDrag").Find("pageViewport").Find("menubar").Find("btn_guanwang").GetComponent<UIEventListener>();
           // guanWang.onClick = delegate { Debug.Log("中财"); Application.OpenURL("http://zcgd.zhongcai.com/"); };
        }
        /// <summary>
        /// 给属性赋值
        /// </summary>
        void GetCompent()
        {
            if (search == null)
            {
                search = transform.Find("searchbar").GetComponent<MySearch>();
                st = transform.Find("pageDrag").GetComponent<ScrollItem>();
                st.onEndDrag = OnEndDrag;
                prefab = transform.Find("pageDrag").Find("pageViewport").Find("forumItem").GetComponent<forumItem>();
                top = transform.Find("pageDrag").Find("pageViewport").Find("topItem").GetComponent<forumItem>();
                dongTai = transform.Find("pageDrag").Find("pageViewport").Find("menubar").Find("btn_dongtai").GetComponent<UIEventListener>();
                gongYi = transform.Find("pageDrag").Find("pageViewport").Find("menubar").Find("btn_gongyi").GetComponent<UIEventListener>();
                gongYou = transform.Find("pageDrag").Find("pageViewport").Find("menubar").Find("btn_xunzhao").GetComponent<UIEventListener>();
                ziXun = transform.Find("pageDrag").Find("pageViewport").Find("menubar").Find("btn_wenti").GetComponent<UIEventListener>();
                guanWang = transform.Find("pageDrag").Find("pageViewport").Find("menubar").Find("btn_guanwang").GetComponent<UIEventListener>();
                faTie = transform.Find("btn_editor").GetComponent<UIEventListener>();
                faTie.onClick = GoToEditor;
                search.OnClick = OnSearch;
                dongTai.onClick = delegate { GoToDongTaiPanel(1); };
                gongYi.onClick = delegate { GoToDongTaiPanel(2); };
                gongYou.onClick = delegate { GoToDongTaiPanel(3); };
                ziXun.onClick = delegate { GoToDongTaiPanel(4); };
                guanWang.onClick = delegate {  Application.OpenURL("http://zcgd.zhongcai.com/"); };
                EventManager.instance.RegisterEvent(XMWorkspace.Event.GetForumList, OnGetForumList);

            }
        }
        /// <summary>
        /// 进入帖子编辑界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToEditor(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToForumEditor(this);
        }

        void OnEndDrag(Vector2 obj)
        {
            //刷新
            if (obj.y < -100)
            {
                page = 1;
                HideAllCurrentForum();
                top.SetItemData(0);
                GetAllForumList();
            }
            //翻页
            else if (obj.y > 100)
            {
                page++;
                GetAllForumList();
            }
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="obj"></param>
        private void OnSearch(string obj)
        {

            if (obj == "")
            {
                obj = " ";
            }
            UIManager.instance.GoToForumJieGuo(0, new string[] { obj }, this);
        }

        /// <summary>
        /// 跳转到动态界面
        /// </summary>
        /// <param name="index"></param>
        void GoToDongTaiPanel(int index)
        {
            UIManager.instance.GoToDongTai(index,this);
        }


        private void OnGetForumList(object[] obj)
        {
            if (!gameObject.activeSelf) return;
            if ((bool)obj[0])
            {
                List<Forum> orders = (List<Forum>)obj[1];
                //StopAllCoroutines();
                //StartCoroutine(CreatForumList(orders));
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
                // UIManager.instance.StartLoad();
                currentShowCount = total;
                page = (int)obj[3];

            }

            #region
            //if ((bool)obj[0])
            //{
            //    List<Forum> datas = (List<Forum>)obj[1];

            //    //需要创建的预制体数量
            //    int creatNum = datas.Count; 
            //    ///创建帖子的预制体
            //    for (int i = 0; i < creatNum; i++)
            //    {
            //        forumItem temp = Instantiate<forumItem>(prefab, prefab.transform.parent);
            //        temp.SetItemData(datas[i]);
            //        temp.gameObject.SetActive(true);
            //        currentForum.Add(temp);
            //    }


            //}
            //else
            //{


            //}
            #endregion
        }

        private IEnumerator CreatForumList(List<Forum> orders)
        {
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
             yield return  creatItem.SetItemDataIe(orders[i - currentShowCount]);
                // creatItem.gameObject.SetActive(true);
            }
            // UIManager.instance.StartLoad();
            currentShowCount = total;
         

        }
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Show()
        {
            HideAllCurrentForum();
            gameObject.SetActive(true);
          //  UIManager.instance.Clear();
            GetCompent();
            GetAllForumList();
            top.SetItemData(0);
        }
        /// <summary>
        /// 获取所有的帖子列表
        /// </summary>
        void GetAllForumList()
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetForumList(0, page, pageCount, new string[] { });
          
        }
       
        /// <summary>
        /// 隐藏所有的当前帖子
        /// </summary>
        protected void HideAllCurrentForum()
        {

            for (int i = 0; i < currentShowCount; i++)
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
            gameObject.SetActive(false);

        }
        private void OnDisable()
        {
            for (int i = 0; i < currentForums.Count; i++)
            {
              Destroy(  currentForums[i].gameObject);
            }
            currentShowCount = 0;
            currentForums.Clear();
            search.input.text = "";
            UIManager.instance.RefrehImageCache();
        }
    }

}

