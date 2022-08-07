using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GD
{
    /// <summary>
    /// 试压员界面按钮
    /// </summary>
    public class ShiYaYuanButtonMenu : MonoBehaviour
    {

        /// <summary>
        /// 收藏按钮
        /// </summary>
        protected UIEventListener shouCang;
        /// <summary>
        /// 蓝圈按钮
        /// </summary>
        protected UIEventListener lanQuan;
        /// <summary>
        /// 订单按钮
        /// </summary>
        protected UIEventListener dingDan;
        /// <summary>
        /// 邀请按钮
        /// </summary>
        protected UIEventListener sheZhi;

      
        // Use this for initialization
        void Start()
        {
            dingDan = transform.Find("btn_shiya").GetComponent<UIEventListener>();
            if (dingDan)
                dingDan.onClick = GoToShiYa;
            GetCompent();
        }

        protected void GetCompent()
        {
            shouCang = transform.Find("btn_shoucang").GetComponent<UIEventListener>();
            lanQuan = transform.Find("btn_lanquan").GetComponent<UIEventListener>();
          
            sheZhi = transform.Find("btn_shezhi").GetComponent<UIEventListener>();
           
            shouCang.onClick = GoToShouCangPanel;
           
            sheZhi.onClick = GoToSheZhi;
            lanQuan.onClick = GoToLanQuan;
        }

        /// <summary>
        /// 前往收藏界面
        /// </summary>
        protected void GoToShouCangPanel(GameObject go, PointerEventData data)
        {
#if UNITY_EDITOR
            Debug.Log("转到收藏界面");
#endif
            UIManager.instance.GoToShouCang(transform.parent.GetComponent<IRefresh>());
        }
        /// <summary>
        /// 跳转到蓝圈
        /// </summary>
        protected void GoToLanQuan(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToLanQuan(transform.parent.GetComponent<IRefresh>());
        }
        /// <summary>
        /// 跳转到我的订单
        /// </summary>
        protected void GoToShiYa(GameObject go, PointerEventData data)
        {
            UIManager.instance.GoToOrder_ShuiGong(3);
           // UIManager.instance.GoToOrder_ShiYa(transform.parent.GetComponent<IRefresh>());
        }
        /// <summary>
        /// 跳转到设置界面
        /// </summary>
        protected void GoToSheZhi(GameObject go, PointerEventData data)
        {
           
            UIManager.instance.GoToSheZhi(transform.parent.GetComponent<IRefresh>());

        }
    }

}