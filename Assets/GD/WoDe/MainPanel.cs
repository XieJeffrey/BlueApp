using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;

namespace GD {

    /// <summary>
    /// 主操作菜单
    /// </summary>
    public class MainPanel : MonoBehaviour {


        /// <summary>
        /// 试压员主界面
        /// </summary>
        private Panel shiYaYuanPanel;

        /// <summary>
        /// 水工主界面
        /// </summary>
        private Panel shuiGongPanel;


        void Start() {

            shiYaYuanPanel = transform.Find("p_shiyayuan").GetComponent<Panel>();
            shuiGongPanel = transform.Find("p_gongren").GetComponent<Panel>();

            CheckVersion();


        }



        public void OpenMainPanel() {
            //保存用于记录登录历史TOKEN
            PlayerPrefs.SetString("TOKEN", UserData.instance.token);

            // 1为“经销商”，2为“水工”，3为“试压员”
            switch (UserData.instance.role) {
                case 1:
                    if (shuiGongPanel == null)
                        shuiGongPanel = transform.Find("p_gongren").GetComponent<Panel>();
                    shuiGongPanel.Show();
                    break;
                case 2:

                    if (shuiGongPanel == null)
                        shuiGongPanel = transform.Find("p_gongren").GetComponent<Panel>();
                    shuiGongPanel.Show();
                    break;
                case 3:
                    if (shiYaYuanPanel == null)
                        shiYaYuanPanel = transform.Find("p_shiyayuan").GetComponent<Panel>();
                    shiYaYuanPanel.Show();
                    break;
                default:

                    break;
            }
        }

        public void CheckVersion() {
            InterfaceManager instance = GameObject.Find("Camera").GetComponent<InterfaceManager>();
            string url = "http://" + LM_Workspace.UtilManager.getInstance.serverIP + "/System/checkAppVersion";
            StartCoroutine(instance.Get(url, "?version=1.1", (LitJson.JsonData data) => {
                if (((bool)data["data"]["result"]) == false) {
                    LM_Workspace.UtilManager.getInstance.CallOtherAppError("App版本过低\r\n请升级后重新运行", () => {
                        Application.Quit();
                    });
                }
            }));
        }
    }
}
