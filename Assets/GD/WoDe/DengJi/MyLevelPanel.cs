using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;
using SVGImporter;
using UnityEngine.EventSystems;

namespace GD
{
    /// <summary>
    /// 我的等级界面
    /// </summary>
    public class MyLevelPanel : Panel
    {
        /// <summary>
        /// 返回上一个界面
        /// </summary>
        private UIEventListener back;

        /// <summary>
        /// 等级图标
        /// </summary>
        private Transform levelIcon;
        /// <summary>
        /// 等级文本
        /// </summary>
        private Text levelTitle;

        /// <summary>
        /// 等级提示信息
        /// </summary>
        private Text tips;

        private Scrollbar[] sliders;

        private Transform icon;

        /// <summary>
        /// 说明信息按钮
        /// </summary>
        private UIEventListener infoOpenBtn;

        /// <summary>
        /// 说明信息关闭按钮
        /// </summary>
        private UIEventListener infoCloseBtn;

        /// <summary>
        /// 说明信息界面
        /// </summary>
        private GameObject infoObj;


        private void Start()
        {
          
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetLevel, OnGetLevel);
            back = transform.Find("header").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = delegate { Hide(); };

            infoObj = Util.FindChildByName(gameObject, "infoBG");
            infoOpenBtn = Util.FindChildByName(gameObject, "header/infoOpenBtn").GetComponent<UIEventListener>();
            infoCloseBtn = Util.FindChildByName(infoObj, "infoImage/Btn").GetComponent<UIEventListener>();

            infoOpenBtn.onClick = InfoOpenBtnClick;
            infoCloseBtn.onClick = InfoCloseBtnClick;
        }
        private Color32 normalColor = new Color32(191, 191, 191, 255);
        private Color32 selectColor = new Color32(38, 141, 151, 255);
        /// <summary>
        /// 获取等级信息
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetLevel(object[] obj)
        {

            if ((bool)obj[0])
            {
                if (levelIcon == null)
                {
                    levelIcon = transform.Find("内容区").Find("等级容器").Find("等级图标");
                    levelTitle = transform.Find("内容区").Find("等级容器").Find("等级文本").GetComponent<Text>();
                    tips = transform.Find("内容区").Find("提示框").Find("文本").GetComponent<Text>();
                    icon = transform.Find("内容区").Find("进度条").Find("图标");
                    sliders = transform.Find("内容区").Find("进度条").Find("进度").GetComponentsInChildren<Scrollbar>();
                }
#if UNITY_EDITOR
                Debug.Log(UserData.instance.level + "::::::" + (int)obj[1]);
#endif
                int count = (int)obj[2];
                tips.text = "已安装" + count + "户合格纯净蓝产品";
                for (int i = 0; i < levelIcon.childCount; i++)
                {
                    if (i == UserData.instance.level)
                    {
                        levelIcon.GetChild(i).gameObject.SetActive(true);

                    }else
                    {
                        levelIcon.GetChild(i).gameObject.SetActive(false);
                    }
                }
                for (int i = 0; i < sliders.Length; i++)
                {
                   if (i+1 < UserData.instance.level)
                    sliders[i].size = 1;
                   else
                     sliders[i].size = 0;
                }
                for (int i = 0; i < icon.childCount; i++)
                {

                    if (i < UserData.instance.level)
                    {
                        icon.GetChild(i).Find("圆圈").GetComponent<SVGImage>().color = normalColor;
                    }
                    else
                    {

                        icon.GetChild(i).Find("圆圈").GetComponent<SVGImage>().color = selectColor;
                    }
                }
                switch (UserData.instance.level)
                {
                    case 0:
                        levelTitle.text = "新手";

                        break;
                    case 1:
                        levelTitle.text = "工匠";
                        sliders[0].size =  count%10/(float)10;
                        break;

                    case 2:
                        levelTitle.text = "巧匠";
                        sliders[1].size = count % 10 / (float)10;
                        break;
                    case 3:
                        levelTitle.text = "大师";
                        sliders[2].size = count % 10 / (float)10;
                        break;
                    case 4:
                        levelTitle.text = "鲁班";
                        sliders[3].size = count % 10 / (float)10;
                        break;
                    case 5:
                        levelTitle.text = "中财权威认证";
                        break;
                }



            }
           
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            UIManager.instance.BackToLastPanel();
        }

        public override void Show()
        {
            Debug.Log("等级界面");
            gameObject.SetActive(true);
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetLevel();
        }

        void InfoOpenBtnClick(GameObject go, PointerEventData eventdata)
        {
            infoObj.SetActive(true);

        }
        void InfoCloseBtnClick(GameObject go, PointerEventData eventdata)
        {
            infoObj.SetActive(false);

        }
    }

}
  
