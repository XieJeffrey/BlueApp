using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;
namespace GD {

    /// <summary>
    /// 用户类型选择界面
    /// </summary>
    public class UserTypeSelectPanel : MonoBehaviour
    {

        /// <summary>
        /// 水工按钮
        /// </summary>
        private UIEventListener shuiGong;

        /// <summary>
        /// 试压员按钮
        /// </summary>
        private UIEventListener shiYaYuan;
        /// <summary>
        /// 经销商
        /// </summary>
        private UIEventListener jingXiaoShang;
        int type;
        private void Start()
        {
            jingXiaoShang = transform.Find("内容区").Find("btn_jingxiaoshang").GetComponent<UIEventListener>();
            shuiGong = transform.Find("内容区").Find("btn_shuigong").GetComponent<UIEventListener>();
            shiYaYuan = transform.Find("内容区").Find("btn_shiyayuan").GetComponent<UIEventListener>();


            jingXiaoShang.onClick = delegate { SetRole(1); };
            shuiGong.onClick = delegate { SetRole(2); };
            shiYaYuan.onClick = delegate { SetRole(3); };
            EventManager.instance.RegisterEvent(XMWorkspace.Event.SetRole, OnSetRole);
        }


        private void OnSetRole(object[] data)
        {
            if ((bool)data[0])
            {
                // UIManager.instance.OpenMainPanel();
                UIManager.instance.GoToAreaSelectPanel(gameObject);
            }else
            {
                UIManager.instance.ShowLoggerMsg("选择角色出错");

            }

        }

        void SetRole(int index)
        {
            type = index;
            GameObject.Find("Camera").GetComponent<InterfaceManager>().SetRole(index);

        }
    }
}
