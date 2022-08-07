using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GD
{
    /// <summary>
    /// 搜索组件
    /// </summary>
    public class MySearch : MonoBehaviour
    {

        /// <summary>
        /// 确认搜索按钮
        /// </summary>
        private UIEventListener searchBtn;
        /// <summary>
        /// 搜索内容
        /// </summary>
        public InputField input;

        public System.Action<string> OnClick;

        private int currentType;

        private void Start()
        {
            searchBtn = transform.Find("btn_search").GetComponent<UIEventListener>();
            input= transform.Find("input_search").GetComponent<InputField>();
            searchBtn.onClick = OnSearch;

        }
        /// <summary>
        /// 开始搜索
        /// </summary>
        /// <param name="go"></param>
        /// <param name="data"></param>
        private void OnSearch(GameObject go, PointerEventData data)
        {
            // Debug.Log(input.text);
            if (OnClick!=null)
            {
                OnClick(input.text);
            }
            input.text = "";
           // UIManager.instance.GoToForumJieGuo(currentType, new string[] { input.text });
        }
    }

}
