using LM_Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GD
{
    /// <summary>
    /// 收藏条目信息
    /// </summary>
    public class forumItem : Item
    {
        /// <summary>
        /// 收藏数据信息
        /// </summary>
        Forum forumData;

        protected Text userName;

        protected Text mainTitle;

        protected Text Type;

        protected Text dateTime;

        protected Text pingLun;

        protected Text liuLan;
        protected RawImage touxiang;
        protected UIEventListener button;
        /// <summary>
        /// 找到组件并赋值
        /// </summary>
        protected void FindCompent()
        {
            if (userName == null)
                userName = transform.Find("topItem").Find("userName").GetComponent<Text>();
            if (mainTitle == null)
                mainTitle = transform.Find("topItem").Find("mainTitle").GetComponent<Text>();
            if (Type == null)
                Type = transform.Find("bottomItem").Find("userNameItem").Find("label").GetComponent<Text>();
            if (dateTime == null)
                dateTime = transform.Find("bottomItem").Find("dateTimeLabel").GetComponent<Text>();
            if (pingLun == null)
                pingLun = transform.Find("bottomItem").Find("forumLabel").Find("Text").GetComponent<Text>();
            if (liuLan == null)
                liuLan = transform.Find("bottomItem").Find("viewLabel").Find("Text").GetComponent<Text>();
            if (touxiang == null)
                touxiang = transform.Find("topItem").Find("mask").Find("icon").GetComponent<RawImage>();
            if (button == null)
            {
                button = GetComponent<UIEventListener>();
                button.onClick = OnClick;
            }

        }
        /// <summary>
        /// 跳往 帖子界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        protected virtual void OnClick(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToForumDetail(forumData);
        }
        /// <summary>
        /// 设置帖子信息
        /// </summary>
        /// <param name="data"></param>
         protected void SetForumData(Forum data)
        {
            gameObject.SetActive(true);
            FindCompent();
            forumData = data;
            userName.text = data.userName;
            // Debug.Log(data.content);
            if (data.content.Length > 18)
                mainTitle.text = data.content.Substring(0, 18) + " ...";
            else
                mainTitle.text = data.content;
            switch (data.catId)
            {
                case 0:
                    Type.text = "全部";
                    break;
                case 1:
                    Type.text = "动态分享";
                    break;
                case 2:
                    Type.text = "工艺展示";
                    break;
                case 3:
                    Type.text = "寻找工友";
                    break;
                case 4:
                    Type.text = "问题咨询";
                    break;
            }
           
            dateTime.text = data.create_time;
            pingLun.text ="评论" + data.comment.ToString();
            liuLan.text = "浏览量" +data.view.ToString();
            bool checkType;
            UIManager.instance.LoadImageFromUrl(data.userAvatar, 94, 94,out checkType, delegate
            {
                touxiang.gameObject.SetActive(true);
                return touxiang;
            });
            if (!checkType)
            {
               
                touxiang.gameObject.SetActive(false);
            }
           // UIManager.instance.Push(data.userAvatar, 300, 500, touxiang);
                ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
                //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(data.userAvatar, 300, 500, delegate {
                //    return touxiang;
                //}));

            }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="data"></param>
        public override void SetItemData(object data)
        {
            SetForumData((Forum)data);
          
        }

        public IEnumerator SetItemDataIe(Forum data)
        {
            FindCompent();
            forumData = data;
            userName.text = data.userName;
            mainTitle.text = data.title;
            switch (data.catId)
            {
                case 0:
                    Type.text = "全部";
                    break;
                case 1:
                    Type.text = "动态分享";
                    break;
                case 2:
                    Type.text = "工艺展示";
                    break;
                case 3:
                    Type.text = "寻找工友";
                    break;
                case 4:
                    Type.text = "问题咨询";
                    break;
            }

            dateTime.text = data.create_time;
            pingLun.text = "评论" + data.comment.ToString();
            liuLan.text = "浏览量" + data.view.ToString();

            yield return PostImageManager.getInstance.LoadImageFromUrl(data.userAvatar, 94, 94, delegate
            {
                gameObject.SetActive(true);
                return touxiang;
            });

            }
    }

}
