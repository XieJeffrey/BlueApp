using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GD
{

    public class ShangPinItem : Item
    {
        /// <summary>
        /// 货物名称
        /// </summary>
        private Text nameTitle;

        /// <summary>
        /// 货物价格
        /// </summary>
        private Text jiaGe;

        private RawImage icon;
        HistoryOrder good;
      
        public override void SetItemData(object data)
        {
            SetShangPinItemData((HistoryOrder)data);


        }
        /// <summary>
        /// 设置商品信息
        /// </summary>
        /// <param name="data"></param>
        void SetShangPinItemData(HistoryOrder data)
        {
            FindCompent();
            good = data;
            nameTitle.text = good.goodsTitle;
            jiaGe.text = good.price+"积分";
            UIManager.instance.LoadImageFromUrl(good.thumb,300,300,delegate { return icon;  });
        }
       
        void FindCompent()
        {
            if (nameTitle == null)
                nameTitle = transform.Find("title").GetComponent<Text>();
            if (jiaGe == null)
                jiaGe = transform.Find("Text").GetComponent<Text>();

            if (icon == null)
            {
                icon = transform.Find("mask").Find("RawImage").GetComponent<RawImage>();
            }
        }

       
    }

}
