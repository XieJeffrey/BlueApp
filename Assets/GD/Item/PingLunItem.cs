using LM_Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GD
{

    public class PingLunItem : Item
    {
        Comment commentData;

        protected Text userName;

        protected Text mainTitle;

        protected Text dateTime;
      
        protected RawImage touxiang;

      
        /// <summary>
        /// 找到组件并赋值
        /// </summary>
        protected void FindCompent()
        {
            if (userName == null)
                userName = transform.Find("top").Find("users").Find("userName").GetComponent<Text>();
            if (mainTitle == null)
                mainTitle = transform.Find("top").Find("mainTitle").GetComponent<Text>();
            if (dateTime == null)
                dateTime = transform.Find("top").Find("users").Find("dateTimeLabel").GetComponent<Text>();
            if (touxiang == null)
                touxiang = transform.Find("top").Find("mask").Find("icon").GetComponent<RawImage>();
          

        }
        //设置评论信息
        public override void SetItemData(object data)
        {
            gameObject.SetActive(true);
            commentData = (Comment)data;
            FindCompent();
            userName.text = commentData.userName;
            mainTitle.text = commentData.content;
            dateTime.text = commentData.createTime;

           // UIManager.instance.LoadImageFromUrl(commentData.userAvatar, 94, 94, delegate {
               
            //    return touxiang;
            //});

			bool checkType;
			UIManager.instance.LoadImageFromUrl(commentData.userAvatar, 94, 94,out checkType, delegate
				{
					touxiang.gameObject.SetActive(true);
					return touxiang;
				});
			if (!checkType)
			{

				touxiang.gameObject.SetActive(false);
			}
            ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
            //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(commentData.userAvatar, 300, 500, delegate {
            //    return touxiang;
            //}));

        }

       
    }


}