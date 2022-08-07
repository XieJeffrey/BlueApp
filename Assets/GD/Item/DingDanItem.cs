using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GD
{
    /// <summary>
    /// 订单显示单位元素
    /// </summary>
    public class DingDanItem : Item
    {
        private Order itemData;
        /// <summary>
        /// 订单号
        /// </summary>
        private Text dingDanHao;
        /// <summary>
        /// 订单类型
        /// </summary>
        private Text dingDanLeiXing;
        /// <summary>
        /// 订单的信息
        /// </summary>
        private Text dingDanXinXi;

        /// <summary>
        /// 合格的状态图标
        /// </summary>
        private GameObject heGeStateIcon;
        /// <summary>
        /// 不合格的状态图标
        /// </summary>
        private GameObject buHeGeStateIcon;

        private UIEventListener button;
        
        // Use this for initialization
        void Start()
        {
            //Debug.Log(name);
            //dingDanHao = transform.Find("bianhao").Find("Text").GetComponent<Text>();
            //dingDanLeiXing= transform.Find("icon").GetComponentInChildren<Text>();
            //dingDanXinXi = transform.Find("infoLabel").GetComponent<Text>();
            //heGeStateIcon = transform.Find("stateIconHeGe").gameObject;
            //buHeGeStateIcon = transform.Find("stateIconBuHeGe").gameObject;
            //button = GetComponent<UIEventListener>();
            //button.onClick = OnClick;
        }
        /// <summary>
        /// 找到组件并赋值
        /// </summary>
        void FindCompent()
        {
            if(dingDanHao==null)
                dingDanHao = transform.Find("bianhao").Find("Text").GetComponent<Text>();
            if (dingDanLeiXing == null)
                dingDanLeiXing = transform.Find("icon").GetComponentInChildren<Text>();
            if (dingDanXinXi == null)
                dingDanXinXi = transform.Find("infoLabel").GetComponent<Text>();
            if (heGeStateIcon == null)
                heGeStateIcon = transform.Find("stateIconHeGe").gameObject;
            if (buHeGeStateIcon == null)
                buHeGeStateIcon = transform.Find("stateIconBuHeGe").gameObject;
            if (button == null)
            {
                button = GetComponent<UIEventListener>();
                button.onClick = OnClick;
            }
          
        }
        /// <summary>
        /// 点击订单后 显示订单详情
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void OnClick(GameObject go, PointerEventData data)
        {
            // UIManager.instance.GoToDingDan();
            if (UserData.instance.role == 3)
            {
             
                ///已完成订单
                if(itemData.orderStatus=="3"|| itemData.orderStatus == "4")
                {
                    UIManager.instance.ShowOrderInfo_ShuiGong(itemData);
                }
                else
                {
                    UIManager.instance.ShowOrderInfo_ShiYa(itemData);

                }


            }else
            {

                UIManager.instance.ShowOrderInfo_ShuiGong(itemData);
            }
          
        }

       

        /// <summary>
        /// 设置订单显示信息
        /// </summary>
        private void SetOrderItemData(Order data)
        {
          //  gameObject.SetActive(true);
            FindCompent();
            itemData = data;
            dingDanHao.text = /*"订单号："+*/data.orderId;
           // dingDanLeiXing.text = data.type;
            dingDanXinXi.text ="订单地址："+ data.community + "\n"+"预约时间："+ data.createTime;


            if (data.orderStatus == "3")
            {
                heGeStateIcon.SetActive(true);

            }else
            {
                heGeStateIcon.SetActive(false);
            }
            if (data.orderStatus == "4")
            {
                buHeGeStateIcon.SetActive(true);

            }
            else
            {
                buHeGeStateIcon.SetActive(false);
            }
        }
        /// <summary>
        /// 设置订单显示信息
        /// </summary>
        public override void SetItemData(object data)
        {
            SetOrderItemData((Order)data);
        }
    }

}

