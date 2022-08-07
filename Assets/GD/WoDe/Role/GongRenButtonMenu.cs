using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GD
{

    /// <summary>
    /// 工人的跳转按钮
    /// </summary>
    public class GongRenButtonMenu : ShiYaYuanButtonMenu
    {
        protected UIEventListener jiFen;
        protected UIEventListener duiHuan;
        protected UIEventListener dengji;
        protected UIEventListener anzhuang;
        protected UIEventListener yuYue;
        protected UIEventListener yaoQing;
        protected UIEventListener btn_MyCraftsmanCard;
        private void Start()
        {

            GetCompent();
        }
        new void GetCompent()
        {
            base.GetCompent();
            dengji = transform.Find("btn_dengji").GetComponent<UIEventListener>();
            anzhuang = transform.Find("btn_anzhuang").GetComponent<UIEventListener>();
            jiFen = transform.Find("btn_jifen").GetComponent<UIEventListener>();
            duiHuan = transform.Find("btn_duihuan").GetComponent<UIEventListener>();
            yuYue = transform.Find("btn_yuyue").GetComponent<UIEventListener>();
            yaoQing = transform.Find("btn_yaoqing").GetComponent<UIEventListener>();

            btn_MyCraftsmanCard = GameObject.Find("btn_MyCraftsmanCard").GetComponent<UIEventListener>();
            btn_MyCraftsmanCard.onClick = GoToCraftsman;

            dengji.onClick = GoToDengJiPanel;
            anzhuang.onClick = GoToAnZhuangPanel;

            jiFen.onClick = GoToJiFenPanel;
            duiHuan.onClick = GoToDuiHuanPanel;

            yuYue.onClick= GoToYuYuePanel;

            yaoQing.onClick = GoToYaoQing;
        }
        /// <summary>
        /// 前往邀请界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToYaoQing(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToYaoQing(transform.parent.GetComponent<IRefresh>());
        }
        /// <summary>
        /// 前往预约订单的界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToYuYuePanel(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToOrder_ShuiGong(1);
        }

        /// <summary>
        /// 前往安装订单的界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToAnZhuangPanel(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToOrder_ShuiGong(2);
        }

        /// <summary>
        /// 前往等级界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToDengJiPanel(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToLevelPanel(transform.parent.GetComponent<IRefresh>());
        }

        /// <summary>
        /// 前往兑换界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToDuiHuanPanel(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToDuiHuan(transform.parent.GetComponent<IRefresh>());

        }
        /// <summary>
        /// 前往积分界面
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToJiFenPanel(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToJiFen(transform.parent.GetComponent<IRefresh>());
        }
        /// <summary>
        /// 前往我的工匠卡
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void GoToCraftsman(GameObject go, PointerEventData data)
        {

            UIManager.instance.GoToCraftsmanPanel(transform.parent.GetComponent<IRefresh>());
        }

    }
}
