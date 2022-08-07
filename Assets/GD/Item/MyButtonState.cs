using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GD
{
    /// <summary>
    /// 按钮的状态管理
    /// </summary>
    public class MyButtonState : MonoBehaviour
    {
        /// <summary>
        /// 默认的颜色
        /// </summary>
        private Color32 normalColor = new Color32(50, 50, 50, 255);
        /// <summary>
        /// 选中的颜色
        /// </summary>
        private Color32 selectColor = new Color32(38, 149, 159, 255);

        /// <summary>
        /// 显示的字体
        /// </summary>
        private Text textColor;

        /// <summary>
        /// 状态显示
        /// </summary>
        private GameObject selectState;

        private bool isSelect;
        public bool IsSelect
        {
            set
            {
                isSelect = value;
                SetSelectState(isSelect);
            }

        }
        void GetCompent()
        {
            if (selectState == null)
                selectState = transform.Find("stater").gameObject;
            if (textColor == null)
                textColor = transform.Find("label").GetComponent<Text>();
        }

        /// <summary>
        /// 设置按钮的状态
        /// </summary>
        /// <param name="select"></param>
        void SetSelectState(bool select)
        {
            GetCompent();
            if (select)
            {
                selectState.SetActive(true);
                textColor.color = selectColor;

            }
            else
            {

                selectState.SetActive(false);
                textColor.color = normalColor;
            }


        }

    }

}
