using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GD
{
    /// <summary>
    /// 底脚按钮
    /// </summary>
    public class Footer : MonoBehaviour
    {
        private UIEventListener lanQuan;
        private UIEventListener paiHang;

        private UIEventListener yuYue;
        private UIEventListener chaoShi;
        private UIEventListener woDe;

        private void Start()
        {
            yuYue = transform.Find("btn_editor").GetComponent<UIEventListener>();
            lanQuan=transform.Find("btnlayout").Find("btn_lanquan").GetComponent<UIEventListener>();
            paiHang = transform.Find("btnlayout").Find("btn_paihang").GetComponent<UIEventListener>();

            chaoShi = transform.Find("btnlayout").Find("btn_chaoshi").GetComponent<UIEventListener>();
            woDe = transform.Find("btnlayout").Find("btn_wode").GetComponent<UIEventListener>();
        }

        /// <summary>
        /// 跳转到蓝圈
        /// </summary>
        void GoToLanQuan()
        {

        }
        /// <summary>
        /// 跳转到排行
        /// </summary>
        void GoToPaiHang()
        {

        }
        /// <summary>
        /// 跳转到预约
        /// </summary>
        void GoToYuYue()
        {

        }
        /// <summary>
        /// 跳转到超市
        /// </summary>
        void GoToChaoShi()
        {

        }
        /// <summary>
        /// 跳转到我的
        /// </summary>
        void GoToWoDe()
        {

        }
    }
}
