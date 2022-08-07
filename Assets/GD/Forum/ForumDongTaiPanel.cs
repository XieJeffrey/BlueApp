using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 论坛动态界面
    /// </summary>
    public class ForumDongTaiPanel :Panel,IRefresh
    {


        /// <summary>
        /// 返回界面
        /// </summary>
        private UIEventListener back;
        /// <summary>
        /// 动态分享
        /// </summary>
        private UIEventListener dongTai;
        /// <summary>
        /// 工艺分享
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
        /// 发帖
        /// </summary>
        private UIEventListener faTie;
        /// <summary>
        /// 搜索组件
        /// </summary>
        private MySearch searchBtn;
        /// <summary>
        /// 当前显示的帖子类型
        /// 
        /// 1 动态分享
        /// 2 工艺展示
        /// 3 寻找工友
        /// 4 问题咨询
        /// </summary>
        private int currentType=0;
        protected ScrollItem st;
        protected int page = 1;
        protected int pageCount = 5;
        protected int currentShowCount;

        /// <summary>
        /// 帖子预制体
        /// </summary>
        private forumItem prefab;
        /// <summary>
        /// 帖子预制体
        /// </summary>
        private forumItem top;
        /// <summary>
        /// 缓存池
        /// </summary>
        private List<forumItem> forumPool = new List<forumItem>();
        /// <summary>
        /// 当前显示的帖子
        /// </summary>
        private List<forumItem> currentForums = new List<forumItem>();
       

        /// <summary>
        /// 设置回调事件
        /// </summary>
        void SetEvent()
        {
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetForumList, OnGetForumList);

        }
        /// <summary>
        /// 给属性赋值
        /// </summary>
        void GetCompent()
        {
            if (prefab == null)
            {
                prefab = transform.Find("dragPanel").Find("offset").Find("content").Find("forumItem").GetComponent<forumItem>();
                top = transform.Find("dragPanel").Find("offset").Find("content").Find("topItem").GetComponent<forumItem>();
                back = transform.Find("searchbar").Find("btn_back").GetComponent<UIEventListener>();
                dongTai= transform.Find("topbar").Find("btn_dongtai").GetComponent<UIEventListener>();
                gongYi = transform.Find("topbar").Find("btn_gongyi").GetComponent<UIEventListener>();
                gongYou = transform.Find("topbar").Find("btn_xunzhao").GetComponent<UIEventListener>();
                ziXun = transform.Find("topbar").Find("btn_zixun").GetComponent<UIEventListener>();
                faTie= transform.Find("btn_editor").GetComponent<UIEventListener>();
                searchBtn = transform.Find("searchbar").GetComponent<MySearch>();
                st = transform.Find("dragPanel").Find("offset").GetComponent<ScrollItem>();
                st.onEndDrag = OnEndDrag;
                searchBtn.OnClick = OnSearch;
                back.onClick = delegate { Hide(); };
                dongTai.onClick = delegate { OnTypeChange(1); };
                gongYi.onClick = delegate { OnTypeChange(2); };
                gongYou.onClick = delegate { OnTypeChange(3); };
                ziXun.onClick = delegate { OnTypeChange(4); };
                faTie.onClick = GoToEditor;
                SetEvent();
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
            UIManager.instance.GoToForumJieGuo(currentType,new string[] { obj},this);
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
                // StopAllCoroutines();
                //  StartCoroutine(CreatForumList(orders));
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
                    //creatItem.gameObject.SetActive(true);
                }
                //  UIManager.instance.StartLoad();
                currentShowCount = total;
                page = (int)obj[3];

            }
            //if ((bool)obj[0])
            //{

            //    List<Forum> forumList = (List<Forum>)obj[1];
            //    int currentCount = currentForums.Count;
            //    int creatCount = forumList.Count + pageCount * (page - 1)- currentCount;
            //    for(int i=0;i< creatCount; i++)
            //    {
            //        forumItem temp = Instantiate<forumItem>(prefab, prefab.transform.parent);
            //        currentForums.Add(temp);
            //    }
            //    for(int i=0;i< forumList.Count; i++)
            //    {
            //        currentForums[pageCount * (page - 1) + i].gameObject.SetActive(true);
            //        currentForums[pageCount * (page - 1) + i].SetItemData(forumList[i]);
            //    }
            //}
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
            yield return   creatItem.SetItemDataIe(orders[i - currentShowCount]);
                //creatItem.gameObject.SetActive(true);
            }
            //  UIManager.instance.StartLoad();
            currentShowCount = total;
        }


        public override void Hide()
        {
            //currentType = 0;
            ClearCache();
            UIManager.instance.BackToLastPanel();
        }

        public override void Show()
        {
           

        }

        public void Show(int index)
        {
#if UNITY_EDITOR
            Debug.Log("帖子类型:"+index);
#endif
            GetCompent();
            gameObject.SetActive(true);
          //  UIManager.instance.Clear();
            currentType = 0;
            OnTypeChange(index);
            top.SetItemData(0);
        }
        /// <summary>
        /// 获取论坛帖子列表
        /// </summary>
        /// <param name="index"></param>
        void GetForumList()
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetForumList(currentType, page, pageCount,new string[] { });
        }

        /// <summary>
        /// 切换帖子类型
        /// </summary>
        /// <param name="index"></param>
        void OnTypeChange(int index)
        {
            if (index == currentType) return;
            currentType = index;
            page = 1;
            UpdateMyButtonState();
            HideAllCurrentForum();
            GetForumList();
           
          
        }

        /// <summary>
        /// 刷新按钮选择状态
        /// </summary>
        /// <param name="index"></param>
        void UpdateMyButtonState()
        {
            switch (currentType)
            {
                case 1:
                    dongTai.GetComponent<MyButtonState>().IsSelect = true;
                    gongYi.GetComponent<MyButtonState>().IsSelect = false;
                    gongYou.GetComponent<MyButtonState>().IsSelect = false;
                    ziXun.GetComponent<MyButtonState>().IsSelect = false;

                    break;
                case 2:
                    dongTai.GetComponent<MyButtonState>().IsSelect = false;
                    gongYi.GetComponent<MyButtonState>().IsSelect = true;
                    gongYou.GetComponent<MyButtonState>().IsSelect = false;
                    ziXun.GetComponent<MyButtonState>().IsSelect = false;
                    break;
                case 3:
                    dongTai.GetComponent<MyButtonState>().IsSelect = false;
                    gongYi.GetComponent<MyButtonState>().IsSelect = false;
                    gongYou.GetComponent<MyButtonState>().IsSelect = true;
                    ziXun.GetComponent<MyButtonState>().IsSelect = false;
                    break;
                case 4:
                    dongTai.GetComponent<MyButtonState>().IsSelect = false;
                    gongYi.GetComponent<MyButtonState>().IsSelect = false;
                    gongYou.GetComponent<MyButtonState>().IsSelect = false;
                    ziXun.GetComponent<MyButtonState>().IsSelect = true;
                    break;

            }

        }

        /// <summary>
        /// 关闭现有的帖子
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
            Show(currentType);
        }

        public void ClearCache()
        {
            gameObject.SetActive(false);
            for (int i = 0; i < currentForums.Count; i++)
            {
              Destroy( currentForums[i].gameObject);
            }
            currentForums.Clear();
            currentShowCount = 0;
            searchBtn.input.text = "";
            UIManager.instance.RefrehImageCache();
        }
    }

}