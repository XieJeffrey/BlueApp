using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;
using UnityEngine.EventSystems;
namespace GD
{
    /// <summary>
    /// 轮播图片
    /// </summary>
    public class BannerItem : Item
    {

        RawImage[] bannerImage;

        ScrollItem sr;
        Transform pointPanel;
        int currentIndex=0;
        int CurrentIndex
        {
            get { return currentIndex; }
            set
            {

                if (value >= 3)
                {
                    currentIndex = 0;

                }else if (value < 0)
                {
                    currentIndex = 2;
                }else
                {
                    currentIndex = value;

                }
            }

        }
        private float lastTime=0;
        private void Start()
        {
          
            SetItemData("测试");

        }
        public override void SetItemData(object data)
        {
            FindCompent();
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetBannerList(1, 3);

        }
        void FindCompent()
        {
            if (bannerImage == null)
            {
                bannerImage = new RawImage[3];
                bannerImage[0] = transform.Find("dragPanel").Find("content").Find("RawImage1").GetComponent<RawImage>();
                bannerImage[1] = transform.Find("dragPanel").Find("content").Find("RawImage2").GetComponent<RawImage>();
                bannerImage[2] = transform.Find("dragPanel").Find("content").Find("RawImage3").GetComponent<RawImage>();
            }
            if (pointPanel == null)
            {
                pointPanel = transform.Find("pointPanel").transform;
                EventManager.instance.RegisterEvent(XMWorkspace.Event.GetBannerList, OnGetBannerList);
            }
            if (sr == null)
            {
                sr = transform.Find("dragPanel").GetComponent<ScrollItem>();
                sr.onEndDrag = OnEndDrag;
              
               
            }
        }

        private void Update()
        {
            if (Time.time - lastTime > 10)
            {
                ChangeState(1);
            }

        }
        private void OnGetBannerList(object[] obj)
        {
            
            if ((bool)obj[0])
            {
                List<Banner> bannerList = (List<Banner>)obj[1];
                for(int i = 0; i < bannerList.Count; i++)
                {
                    int index = i;
                    UIManager.instance.LoadImageFromUrl(bannerList[index].thumb,750,230,delegate { return bannerImage[index]; });
                }
                currentIndex = 0;
                UpdateState();
            }
            else
            {


            }

        }

        private void OnEndDrag(Vector2 arg0)
        {
           // Debug.Log(arg0);
            if (arg0.x <= -100)
            {
                ChangeState(1);

            }else if(arg0.x>=100)
            {
                ChangeState(-1);
            }

        }
        void ChangeState( int step)
        {
            CurrentIndex += step;
            lastTime = Time.time;
            UpdateState();

        }

        void UpdateState()
        {
            if (bannerImage == null)
            {
                bannerImage = new RawImage[3];
                bannerImage[0] = transform.Find("dragPanel").Find("content").Find("RawImage1").GetComponent<RawImage>();
                bannerImage[1] = transform.Find("dragPanel").Find("content").Find("RawImage2").GetComponent<RawImage>();
                bannerImage[2] = transform.Find("dragPanel").Find("content").Find("RawImage3").GetComponent<RawImage>();
            }
            if (pointPanel == null)
                    {
                        pointPanel = transform.Find("pointPanel").transform;
                        EventManager.instance.RegisterEvent(XMWorkspace.Event.GetBannerList, OnGetBannerList);
                    }
            for (int i = 0; i < 3; i++)
            {
                if (currentIndex == i)
                {
                    bannerImage[i].gameObject.SetActive(true);
                    pointPanel.GetChild(i).GetComponent<SVGImporter.SVGImage>().color = new Color32(38, 149, 159, 255);
                }
                else
                {
                    bannerImage[i].gameObject.SetActive(false);
                    
                    pointPanel.GetChild(i).GetComponent<SVGImporter.SVGImage>().color = new Color32(255, 255, 255, 255);
                }
            }
        }


    }


}
