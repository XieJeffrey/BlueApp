using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GD
{
    /// <summary>
    /// 搜索工具
    /// </summary>
    public class SearchPanel : Panel {

        /// <summary>
        /// 查找按钮
        /// </summary>
        private UIEventListener search;

        private InputField input;

        public override void Hide()
        {

            gameObject.SetActive(false);


        }

        public override void Show()
        {
            gameObject.SetActive(true);

        }
       
        void Start()
        {
            search = transform.Find("btn_search").GetComponent<UIEventListener>();
            input = transform.Find("input_search").GetComponent<InputField>();
            search.onClick = OnSearch;

        }

        /// <summary>
        /// 搜索订单
        /// </summary>
        private void OnSearch(GameObject go, PointerEventData data)
        {
          

        }
    }

}