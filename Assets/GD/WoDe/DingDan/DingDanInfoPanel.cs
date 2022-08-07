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
    /// 订单信息显示面板
    /// </summary>
    public class DingDanInfoPanel : MonoBehaviour {

        /// <summary>
        /// 返回
        /// </summary>
        private UIEventListener back;
        /// <summary>
        /// 施工照片及其情况
        /// </summary>
        private UIEventListener detail;

        private UIEventListener xiugaiBtn;

        protected Order currentShowOrder;
        #region 订单显示信息
        private Text dingDanBianHao;
        private Text infoLabel;
        private Text orderType;
        private InputField xiaoQu;
        private InputField louHao;
        private InputField yeZhu;
        private InputField yeZhuPhone;
        private Text yuYueRen;
        private Text yuYueRenPhone;
        private Text shangMenShiJian;
        private Text beiZhu;

        private InterfaceManager interfaceManager;

        [HideInInspector]
        public int currentOrderID;
        #endregion 订单显示信息

      

        protected void GetCompent()
        {
           
            back = transform.Find("content").Find("header").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = GoBack;
            detail = transform.Find("content").Find("btn_more").GetComponent<UIEventListener>();
            if(detail)
            detail.onClick = GoDetail;

            xiugaiBtn = transform.Find("content").Find("buttonXiuGai").GetComponent<UIEventListener>();
            if (xiugaiBtn)
                xiugaiBtn.onClick = ChangeOrderMessage;

            dingDanBianHao = transform.Find("content").Find("dingdan_item").Find("bianhao").GetComponentInChildren<Text>();
            infoLabel = transform.Find("content").Find("dingdan_item").Find("infoLabel").GetComponent<Text>();
            orderType = transform.Find("content").Find("dingdan_item").Find("icon").GetComponentInChildren<Text>();
            xiaoQu= transform.Find("content").Find("doc").Find("layout").Find("item").GetComponentInChildren<InputField>();
            louHao = transform.Find("content").Find("doc").Find("layout").Find("item (1)").GetComponentInChildren<InputField>();
            yeZhu = transform.Find("content").Find("doc").Find("layout").Find("item (2)").Find("leftlabel").GetChild(0).GetComponent<InputField>();
            yeZhuPhone = transform.Find("content").Find("doc").Find("layout").Find("item (2)").Find("rightLabel").GetChild(0).GetComponent<InputField>();
            yuYueRen = transform.Find("content").Find("doc").Find("layout").Find("item (3)").Find("leftlabel").GetComponent<Text>();
            yuYueRenPhone = transform.Find("content").Find("doc").Find("layout").Find("item (3)").Find("rightLabel").GetComponent<Text>();
            shangMenShiJian = transform.Find("content").Find("doc").Find("layout").Find("item (4)").GetComponentInChildren<Text>();
            beiZhu = transform.Find("content").Find("doc").Find("layout").Find("item (5)").GetComponentInChildren<Text>();
        }
        /// <summary>
        /// 前往施工详情界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        protected void GoDetail(GameObject go, PointerEventData data)
        {
            UIManager.instance.ShowOrderDetailInfo_ShuiGong(currentShowOrder);
        }

        /// <summary>
        /// 修改订单信息
        /// </summary>
        protected void ChangeOrderMessage(GameObject go, PointerEventData data)
        {
            interfaceManager.ChangeOrderMessage(currentOrderID, yeZhu.text, yeZhuPhone.text, xiaoQu.text, louHao.text);
        }
        /// <summary>
        /// 前往上一页
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoBack(GameObject go, PointerEventData data)
        {
            Hide();
        }

        protected void Start()
        {
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetAppointmentGetList, OnGetOrderInfo);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.ChangeOrderMessage, OnChangeOrderMessage);
            interfaceManager = GameObject.Find("Camera").GetComponent<InterfaceManager>();

        }

        private void OnChangeOrderMessage(object[] obj)
        {
            if ((bool)obj[0])
            {

                GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAppointmentDetail(currentOrderID);
            }

        }

        private void OnGetOrderInfo(object[] obj)
        {
            if ((bool)obj[0])
            {
                currentShowOrder = (Order)obj[1];
                GetCompent();
                UpdateOrderInfo();

            }
           
        }

        /// <summary>
        /// 显示订单面板
        /// </summary>
        /// <param name="data"></param>
        public void Show(Order data)
        {
            gameObject.SetActive(true);
            currentOrderID = int.Parse(data.id);
            GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAppointmentDetail(currentOrderID);



        }


        private string DateTime(string _frame)
        {
            string dateStr = "";
            if(_frame == "1")
            {
                dateStr = "09:00-12:00";
            }
            else
            {
                dateStr = "12:00-5:30";
            }
            return dateStr;
        }


        void UpdateOrderInfo()
        {
           

            dingDanBianHao.text = /*"订单编号："+*/currentShowOrder.orderId;
            infoLabel.text ="订单地址: "+ currentShowOrder.community + "\n" +"预约时间: "+ currentShowOrder.createTime;
            //orderType.text = currentShowOrder.type;
            xiaoQu.text = currentShowOrder.community;
            louHao.text = currentShowOrder.address;
            yeZhu.text = currentShowOrder.name;
            yeZhuPhone.text = currentShowOrder.phone;
            yuYueRen.text = "预约人: " + currentShowOrder.userName;
            yuYueRenPhone.text = "预约人电话: "+ currentShowOrder.userPhone;
            if(currentShowOrder.a_time_frame == "0")
                shangMenShiJian.text = "上门时间: "+currentShowOrder.time;
            else
                shangMenShiJian.text = "上门时间: " + currentShowOrder.a_date + "   " + DateTime(currentShowOrder.a_time_frame);
            beiZhu.text = "备注: "+currentShowOrder.remark;

            if(currentShowOrder.orderStatus=="1"|| currentShowOrder.orderStatus == "2")
            {
                if (detail)
                {
                    detail.gameObject.SetActive(false);
                    if(UserData.instance.role == 3)
                        xiugaiBtn.gameObject.SetActive(true);
                }
            }
            else
            {
                if (detail)
                {
                    detail.gameObject.SetActive(true);
                    xiugaiBtn.gameObject.SetActive(false);
                }
            }
        } 


        /// <summary>
        /// 关闭当前的面板
        /// </summary>
        private void Hide()
        {
            gameObject.SetActive(false);
            FindObjectOfType<DingDanPanel>().HideAllCurrentItemState();
            FindObjectOfType<DingDanPanel>().GetOrderList();

        }
    }
}
