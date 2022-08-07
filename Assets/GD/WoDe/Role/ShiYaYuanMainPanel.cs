using LM_Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 试压员 主界面
    /// </summary>
    public class ShiYaYuanMainPanel : Panel,IRefresh
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        private Text userName;
        /// <summary>
        /// 用户位置信息
        /// </summary>
        private Text location;

        private RawImage touXiang;
       
        
        /// <summary>
        /// 显示试压员界面
        /// </summary>
        public override void Show()
        {
            gameObject.SetActive(true);
            UpdateUserData();

        }
       

        private void Start()
        {
            // EventManager.instance.RegisterEvent(XMWorkspace.Event.PostImageJson, OnHeaderChange);

        }
        /// <summary>
        /// 加载头像
        /// </summary>
        void LoadHeaderImage()
        {
            bool checkType;
            UIManager.instance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, out checkType, delegate
            {
                touXiang.gameObject.SetActive(true);
                return touXiang;
            });
            if (!checkType)
            {
                touXiang.gameObject.SetActive(false);
            }
        }

        private void OnHeaderChange(object[] obj)
        {
            if (!gameObject.activeSelf) return;
            LoadHeaderImage();

            //if (UserData.instance.avatar == "")
            //{
            //    touXiang.gameObject.SetActive(false);
            //}
            //else
            //{
            //    UIManager.instance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, delegate
            //    {
            //        touXiang.gameObject.SetActive(true);
            //        return touXiang;
            //    });
            //}
        }  

        private void UpdateUserData()
        {
            if (userName == null)
            {
                userName = transform.Find("userName").GetComponent<Text>();
            }
            userName.text = UserData.instance.name;
            if (location == null)
            {
                location = transform.Find("location").Find("label").GetComponent<Text>();
            }
            location.text = LM_Workspace.UtilManager.getInstance.areaName;           

            if (touXiang == null)
            {
                touXiang = transform.Find("userIcon").Find("RawImage").GetComponent<RawImage>();
            }
            LoadHeaderImage();
            //if (UserData.instance.avatar == "")
            //{
            //    touXiang.gameObject.SetActive(false);
            //}else
            //{

            //    UIManager.instance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, delegate {

            //        touXiang.gameObject.SetActive(true);
            //        return touXiang;
            //    });
            //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(UserData.instance.avatar, 230, 230, delegate {

            //    touXiang.gameObject.SetActive(true);
            //    return touXiang;
            //}));
       // }

        }
        public override void Hide()
        {

        }

        public void Reload()
        {
            this.Show();
        }

        public void ClearCache()
        {
            gameObject.SetActive(false);
            touXiang.texture = null;
        }
    }

}
