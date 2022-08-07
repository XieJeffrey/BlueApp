using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 我的蓝圈界面
    /// </summary>
    public class LanQuanPanel : ShouCangPanel,IRefresh
    {

       

        /// <summary>
        /// 我的帖子
        /// </summary>
        private UIEventListener woDeTieZi;
        /// <summary>
        /// 我的回帖
        /// </summary>
        private UIEventListener woDeHuiTie;



         void  Start()
        {
            GetCompent();
          
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetCircleList, OnGetCollectionList);
          //  Debug.Log("************");
        }

        new void GetCompent()
        {
            base.GetCompent();
            prefab = transform.Find("dragPanel").Find("offset").Find("content").Find("forumItem").GetComponent<forumItem>();
            if (woDeTieZi == null)
            {
                woDeTieZi = transform.Find("topbar").Find("btn_tiezi").GetComponent<UIEventListener>();
                woDeTieZi.onClick = delegate { GetWoDeTiZi(); };
#if UNITY_EDITOR
                Debug.Log(woDeTieZi);
#endif
            }
            if (woDeHuiTie == null) {
                woDeHuiTie = transform.Find("topbar").Find("btn_huitie").GetComponent<UIEventListener>();
                woDeHuiTie.onClick = delegate { GetWoDeHuiTi(); };
            }
            
        }
        /// <summary>
        /// 获取我的回帖
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GetWoDeHuiTi()
        {
            SetButtonState(2);
            HideAllCurrentForum();
            page = 1;
            GetOrderList();
            // GameObject.Find("Camera").GetComponent<InterfaceManager>().GetCircleList(buttonState, page, pageCount);
        }
        /// <summary>
        /// 获取我的帖子
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GetWoDeTiZi()
        {
            SetButtonState(1);

            HideAllCurrentForum();
            page = 1;
            GetOrderList();
            // GameObject.Find("Camera").GetComponent<InterfaceManager>().GetCircleList(buttonState, page, pageCount);
        }

      
        /// <summary>
        /// 1 我的发帖 
        /// 2 我的回帖
        /// </summary>
        private int buttonState=1;
       protected override void GetOrderList()
        {
#if UNITY_EDITOR
            Debug.Log("获取列表");
#endif
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetCircleList(buttonState, page, pageCount);
        }
        /// <summary>
        /// 设置选中的状态
        /// </summary>
        /// <param name="index"></param>


        void SetButtonState(int index)
        {
            buttonState = index;
            //Debug.Log(woDeTieZi);
            switch (index)
            {
                ///我的发帖
                case 1:
                    if (woDeTieZi)
                    {
                        woDeTieZi.GetComponent<MyButtonState>().IsSelect = true;
                        woDeHuiTie.GetComponent<MyButtonState>().IsSelect = false;
                    }
                  
                    break;
                    //我回帖
                case 2:
                    if (woDeTieZi)
                    {
                        woDeTieZi.GetComponent<MyButtonState>().IsSelect = false;
                        woDeHuiTie.GetComponent<MyButtonState>().IsSelect = true;
                    }
                   
                    break;
            }

        }


        public override void Show()
        {
            gameObject.SetActive(true);
            GetWoDeTiZi();

            if (top == null)
                top = transform.Find("dragPanel").Find("offset").Find("content").Find("topItem").GetComponent<Item>();
            top.SetItemData(0);
            //HideAllCurrentForum();
            //page = 1;
            //buttonState = 1;
            //GetOrderList();
            // GameObject.Find("Camera").GetComponent<InterfaceManager>().GetCircleList(1,0, 10);

        }

        //public void Reload()
        //{
           
        //}

        //public void ClearCache()
        //{
          
        //}
    }

}