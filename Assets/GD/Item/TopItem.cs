using LM_Workspace;
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
    /// 官方帖子
    /// </summary>
    public class TopItem : forumItem
    {
        /// <summary>
        /// 当前置顶的帖子
        /// </summary>
        private OfficalForum forumData;
        /// <summary>
        /// 官方显示界面
        /// </summary>
        private RawImage viewImage;
        private void Start()
        {
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetOfficialList, OnOfficialList);


        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="data"></param>
        public override void SetItemData(object data)
        {
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetOfficialList(1,10);

        }

        new void FindCompent()
        {
            base.FindCompent();
            if (viewImage==null)
            {
                viewImage = transform.Find("centerItem").Find("RawImage").GetComponent<RawImage>();
            }
        }
        /// <summary>
        /// 注册回调事件
        /// </summary>
        void SetEvent()
        {
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetOfficialList, OnOfficialList);
        }

        private void OnOfficialList(object[] obj)
        {
            if (!gameObject.activeInHierarchy) return;
            if ((bool)obj[0])
            {
                Debug.Log("获取官方发布信息");
                List<OfficalForum> Officallist = (List<OfficalForum>)obj[1];

                for(int i = 0; i < Officallist.Count; i++)
                {
                    if (Officallist[i].isTop)
                    {
                        SetTopItemData(Officallist[i]);
                        break;
                    }

                }
               
            }
           


        }

        /// <summary>
        /// 跳往 帖子界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        protected override void OnClick(GameObject go, PointerEventData data)
        {

            UIManager.instance.GoToForumDetail(forumData);


        }

        /// <summary>
        /// 设置顶部帖子信息
        /// </summary>
        /// <param name="data"></param>
        private void SetTopItemData(OfficalForum data)
        {
           this. FindCompent();
            forumData = data;
            userName.text = data.title;
            mainTitle.text = data.content;
            Type.text = "小蓝讯息";
            dateTime.text = data.createTime;
            pingLun.text ="评论"+ data.comment.ToString();
            liuLan.text = "浏览量"+data.view.ToString();
            
            //UIManager.instance.LoadImageFromUrl(data.thumb, 690, 230, delegate {
            //    return viewImage;
            //});
            ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
            //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(data.thumb, 690, 230, delegate {
            //    return viewImage;
            //}));




        }


    }


}
