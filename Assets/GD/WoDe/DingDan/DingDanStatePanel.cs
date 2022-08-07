using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GD
{
    public class DingDanStatePanel : MonoBehaviour
    {
        private Text msg;

        private UIEventListener back;

        private void Start()
        {
            back = transform.Find("button").GetComponent<UIEventListener>();

            back.onClick = GoToWoDe;
        }
        /// <summary>
        /// 回到首页
        /// </summary>
        private void GoToWoDe(GameObject go, PointerEventData data)
        {

            gameObject.SetActive(false);
            UIManager.instance.OpenMainPanel(gameObject);

        }

        public   void Show(bool state )
        {
            gameObject.SetActive(true);
            if (msg == null)
                msg = transform.Find("message").Find("Text").GetComponent<Text>();
            if (state)
            {

                msg.text = "订单合格";

            }
            else
            {
                msg.text = "订单不合格";

            }
        }
    }

}

