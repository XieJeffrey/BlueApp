using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GD
{

    public class DingDanInfo_ShiYaPanel : DingDanInfoPanel
    {

        /// <summary>
        /// 显示订单面板
        /// </summary>
        /// <param name="data"></param>
        public new void Show(Order data)
        {
            base.Show(data);

        }
        UIEventListener shiYa;
        new void Start()
        {
            base.Start();
            shiYa = transform.Find("content").Find("buttonPrefab").GetComponent<UIEventListener>();
            shiYa.onClick = GoToShiYaPanel;

        }
        /// <summary>
        /// 跳转到试压界面
        /// </summary>
        private void GoToShiYaPanel(GameObject go, PointerEventData data)
        {

            UIManager.instance.ShowOrderDetailInfo_ShiYa(currentShowOrder);

        }
    }
}
